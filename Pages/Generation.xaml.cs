using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinForms = System.Windows.Forms;
using System.Linq;
using GenerationTicketsWPF.Models;
namespace GenerationTicketsWPF
{
    /// <summary>
    /// Interaction logic for Generation.xaml
    /// </summary>

    public partial class Generation : Page
    {
        private List<Discipline> DiscipList = null;
        private List<Level> LvlList = null;
        private int currentDiscipID;
        private int _numValue = 0;
        public Generation()
        {
            InitializeComponent();
            MaxTickets.Text = "Unknows";
            txtNum.Text = _numValue.ToString();
            var test = new DbInteraction();
            DiscipList = test.GetDiscipList();
            LvlList = (List<Level>)test.GetLevels();
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                DiscipDescList.ItemsSource = (from p in db.Disciplines
                                              join c in db.Teachings on p.DisciplineId equals c.DisciplineId
                                              where c.WorkerId == Config.User.WorkerId
                                              select p.DisciplineName).ToList();
                Lvl.ItemsSource = db.Levels.Select(x => x.LeverDecryption).ToList();
            }

        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            var pathChoice = new WinForms.FolderBrowserDialog();
            if (pathChoice.ShowDialog() == WinForms.DialogResult.OK)
            {
                var path = pathChoice.SelectedPath;
                List<Ticket> listTickets = null;
                if (Lvl.SelectedIndex != -1 && DiscipDescList.SelectedIndex != -1 && _numValue > 0)
                {
                    using (var db = new GenerationTicketsContext(Config.Options))
                    {
                        var AllTasks = db.Tasks.Where(y =>
                            (y.DisciplineId == (db.Disciplines.Where(x => x.DisciplineName == (DiscipDescList.SelectedItem.ToString())).Select(x => x.DisciplineId).FirstOrDefault()))
                            && (y.Level.LeverDecryption == Lvl.SelectedItem.ToString())
                            ).Select(x => new { IDTask = x.TaskId, TypeTask = x.TypesTaskId });
                        var teorTask = AllTasks.Where(x => x.TypeTask == 2).Select(x => x.IDTask).ToList();
                        var practTask = AllTasks.Where(x => x.TypeTask == 1).Select(x => x.IDTask).ToList();
                        Random random = new Random();
                        if (MaxTickets.Text != "Unknows" && _numValue <= int.Parse(MaxTickets.Text))
                        {
                            db.Tickets.RemoveRange(db.Tickets);
                            for (int i = 1; i <= _numValue; i++)
                            {
                                var item = random.Next(0, teorTask.Count - 1);
                                db.Tickets.Add(new Ticket() { TicketId = i, TaskNumber = 1, TaskId = (teorTask[item]), DisciplineId = currentDiscipID });
                                teorTask.RemoveAt(item);
                                item = random.Next(0, teorTask.Count - 1);
                                db.Tickets.Add(new Ticket() { TicketId = i, TaskNumber = 2, TaskId = (teorTask[item]), DisciplineId = currentDiscipID });
                                teorTask.RemoveAt(item);
                                item = random.Next(0, practTask.Count - 1);
                                db.Tickets.Add(new Ticket() { TicketId = i, TaskNumber = 3, TaskId = (practTask[item]), DisciplineId = currentDiscipID }); //Вопрос в инкременте бд
                                practTask.RemoveAt(item);
                            };
                            db.SaveChanges();
                            listTickets = db.Tickets.Select(x => x).ToList();
                        }
                        else
                        {
                            MessageBox.Show("Проверьте выбранное кол-во билетов");
                        }


                    }

                    if (listTickets != null)
                    {
                        using (var db = new GenerationTicketsContext(Config.Options))
                        {
                            var wordhelper = new WordHelper("Shablon.docx");
                            var item = new Dictionary<string, string>
                    {
                        {"<SPEC>", (from c in db.Disciplines
                                   join p in db.Specialties on c.SpecialtyId equals p.SpecialtyId
                                   where c.DisciplineName==DiscipDescList.SelectedItem.ToString()
                                   select p.SpecialtyDecryption).FirstOrDefault()},
                        {"<DISP>", DiscipDescList.SelectedItem.ToString()},
                        {"<CMAN>", Chairmen.SelectedItem.ToString() },
                        {"<COURSE>",Course.Text},
                        {"<SMSTR>", Semestr.Text },
                        {"<TEACHER>", $"{Config.User.Lname} {Config.User.Fname[0]}. {Config.User.Sname[0]}." }, //$"{Config.User.Lname} {Config.User.Fname[0]}. {Config.User.Sname[0]}."
                        {"<TASK1>", ""},
                        {"<TASK2>", ""},
                        {"<TASK3>", ""},
                        {"<NUMB>",""},
                    };
                            var app = new Microsoft.Office.Interop.Word.Application();
                            var file = (wordhelper.getFileInfo).FullName;
                            var missing = Type.Missing;
                            app.Documents.Open(file);
                            for (int i = 0; i < _numValue; i++)
                            {
                                object times = 1;
                                while (app.ActiveDocument.Undo(ref times))
                                { }
                                var currentTicket = listTickets.Where(x => x.TicketId == (i + 1)).Select(x => x).ToList();
                                item["<TASK1>"] = db.Tasks.Where(x => x.TaskId == currentTicket[0].TaskId).Select(x => x.TaskDecryption).FirstOrDefault();
                                item["<TASK2>"] = db.Tasks.Where(x => x.TaskId == currentTicket[1].TaskId).Select(x => x.TaskDecryption).FirstOrDefault();
                                item["<TASK3>"] = db.Tasks.Where(x => x.TaskId == currentTicket[2].TaskId).Select(x => x.TaskDecryption).FirstOrDefault();
                                item["<NUMB>"] = (i+1).ToString();
                                wordhelper.Process(item,path,app,file,missing);
                            }

                            app.ActiveDocument.Close();
                            app.Quit();
                        }
                    }
                }
            }
        }

        private void DiscipList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                Chairmen.ItemsSource = (from p in db.Chairmans
                                        join c in db.Specialties on p.ChairmanId equals c.ChairmanId
                                        join x in db.Disciplines on c.SpecialtyId equals x.SpecialtyId
                                        join t in db.Teachings on x.DisciplineId equals t.DisciplineId
                                        where (t.WorkerId == Config.User.WorkerId) && (((ComboBox)sender).SelectedItem.ToString()==x.DisciplineName)//Config.User.WorkerId
                                        select p.Lname + ' ' + p.Fname + ' ' + p.Sname).Distinct().ToList();
            
            }
            CountTickets_TextChanged(sender, e);
        }

        private void CountTickets_TextChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                currentDiscipID = db.Disciplines.Where(x => x.DisciplineName == (DiscipDescList.SelectedItem.ToString())).Select(x => x.DisciplineId).FirstOrDefault();
                var AllTasks = db.Tasks.Where(y =>
                   (y.DisciplineId == currentDiscipID)
                   && (y.Level.LeverDecryption == Lvl.SelectedItem.ToString())
                   ).Select(x => new {IDTask = x.TaskId, TypeTask = x.TypesTaskId }); // получение всех вопросов локально, их id и тип


                if (Lvl.SelectedIndex != -1 && DiscipDescList.SelectedIndex != -1)
                {
                    int CountTeorTask = db.Tasks.Where(y =>
                    (y.DisciplineId == currentDiscipID)
                    && (y.Level.LeverDecryption == Lvl.SelectedItem.ToString())
                    && (y.TypesTaskId==2)
                    ).Select(x => x).Count();
                    int CountPractTask = db.Tasks.Where(y =>
                    (y.DisciplineId == currentDiscipID)
                    && (y.Level.LeverDecryption == Lvl.SelectedItem.ToString())
                    && (y.TypesTaskId == 1)
                    ).Select(x => x).Count();
                    MaxTickets.Text = ((CountTeorTask/2 < CountPractTask) ? CountTeorTask/2 : CountPractTask).ToString(); //реф
                    //MaxTickets.Text = db.Tasks.Where(y =>
                    //(y.DisciplineId == (int)(db.Disciplines.Where(x => x.DisciplineName == (DiscipList.SelectedItem.ToString())).Select(x => x.DisciplineId).FirstOrDefault()))
                    //&& (y.Level.LeverDecryption == Lvl.SelectedItem.ToString())
                    //).Select(x => x).Count().ToString();

                }
                else
                    MaxTickets.Text = "Unknows";
            }
        }


        public int NumValue
        {
            get { return _numValue; }
            set
            {
                _numValue = value;
                txtNum.Text = value.ToString();
            }
        }


        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            NumValue++;
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            NumValue--;
        }

        private void txtNum_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtNum == null)
            {
                return;
            }

            if (!int.TryParse(txtNum.Text, out _numValue))
            {
                txtNum.Text = _numValue.ToString();   
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
