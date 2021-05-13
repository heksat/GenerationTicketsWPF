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
using System.Text.RegularExpressions;
using  Trd = System.Threading.Tasks;
using System.Threading;

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
        private DbInteraction dbInteraction = new DbInteraction();
        private List<Ticket> listTickets = null;
        private List<Ticket> localTickets = null;
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
                if (Config.User.RoleId != 1)
                {
                    DiscipDescList.ItemsSource = (from p in db.Disciplines
                                                  join c in db.Teachings on p.DisciplineId equals c.DisciplineId
                                                  where c.WorkerId == Config.User.WorkerId
                                                  select p.DisciplineName).ToList();
                }
                else
                {
                    DiscipDescList.ItemsSource = db.Disciplines.Select(x => x.DisciplineName).ToList();
                }
                Lvl.ItemsSource = db.Levels.Select(x => x.LeverDecryption).ToList();
            }

        }

        private async void Run_Click(object sender, RoutedEventArgs e)
        {

            
            if (DoLocalGen.IsChecked == true)
            {
                listTickets = dbInteraction.GetTickets();
            }
            else
            {
                listTickets = localTickets;
            }

            int semestr, course;

            if (Chairmen.SelectedIndex == -1 || !(Int32.TryParse(Semestr.Text, out semestr)) || !(Int32.TryParse(Course.Text, out course)) || course < 1 || course > 6 || semestr < 1 || semestr > 6)
            {
                MessageBox.Show(@"Проверьте правильность выбранных\введенных значений");
            }
            else
            {
                if (listTickets == null)
                {
                    MessageBox.Show("Сгенерируйте сначала билеты или выберите генерацию по БД ");
                }
                else
                {
                    ProgressCheck.Value = 1;
                    try
                    {
                        ProgressCheck.Maximum = listTickets.Count / 3 + 1;
                        if (listTickets.Any())
                        {
                            var pathChoice = new WinForms.FolderBrowserDialog();
                            if (pathChoice.ShowDialog() == WinForms.DialogResult.OK)
                            {
                                var path = pathChoice.SelectedPath;
                                var wordhelper = new WordHelper("Shablon.docx");
                                var spec = dbInteraction.GetSpectoDisp(dbInteraction.GetDispfromTickets(listTickets));
                                List<Task> listTasks = dbInteraction.GetTasks();
                                var discipchoised = dbInteraction.GetDispfromTickets(listTickets);
                                var coursechoised = Course.Text;
                                var Chairmenchoised = Chairmen.SelectedItem.ToString();
                                var Semestrchoised = Semestr.Text;
                                var item = new Dictionary<string, string>
                    {
                        {"<SPEC>", spec.SpecialtyDecryption},
                        {"<SPECI>", spec.SpecialtyId + " " + spec.SpecialtyDecryption},
                        {"<DISP>", discipchoised},
                        {"<CMAN>", Chairmenchoised},
                        {"<COURSE>",coursechoised},
                        {"<SMSTR>", Semestrchoised},
                        {"<TEACHER>", $"{Config.User.Lname} {Config.User.Fname[0]}. {Config.User.Sname[0]}." }, //$"{Config.User.Lname} {Config.User.Fname[0]}. {Config.User.Sname[0]}."
                        {"<TASK1>", ""},
                        {"<TASK2>", ""},
                        {"<TASK3>", ""},
                        {"<NUMB>",""},
                    };
                                var app = new Microsoft.Office.Interop.Word.Application();
                                var file = (wordhelper.getFileInfo).FullName;
                                var missing = Type.Missing;
                                var fil = app.Documents.Open(file);
                                for (int i = 0; i < listTickets.Count() / 3; i++)
                                {
                                    object times = 1;
                                    while (app.ActiveDocument.Undo(ref times))
                                    { }
                                    var currentTicket = listTickets.Where(x => x.TicketId == (i + 1)).Select(x => x).ToList();
                                    item["<TASK1>"] = listTasks.Where(x => x.TaskId == currentTicket[0].TaskId).Select(x => x.TaskDecryption).FirstOrDefault();
                                    item["<TASK2>"] = listTasks.Where(x => x.TaskId == currentTicket[1].TaskId).Select(x => x.TaskDecryption).FirstOrDefault();
                                    item["<TASK3>"] = listTasks.Where(x => x.TaskId == currentTicket[2].TaskId).Select(x => x.TaskDecryption).FirstOrDefault();
                                    item["<NUMB>"] = (i + 1).ToString();
                                    await Trd.Task.Run(() => { wordhelper.Process(item, path, app, file, missing); });

                                    //app.ActiveDocument.UndoClear();
                                    ProgressCheck.Value += 1;
                                    //  ProgressCheck.Value += 1;
                                }

                                app.ActiveDocument.Close();
                                app.Quit();
                                ProgressCheck.Value = ProgressCheck.Maximum;
                            }
                            MessageBox.Show("Готово!");
                            ProgressCheck.Value = 0;
                        }
                        else
                        {
                            MessageBox.Show("База данных и локальный кеш пуст, сгенерируйте билеты");
                        }
                    }
                    catch (System.Runtime.InteropServices.COMException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                //}
            }
        }

        private void DiscipList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var allowchairman = dbInteraction.GetChairmanList(((ComboBox)sender).SelectedItem.ToString());
            //using (var db = new GenerationTicketsContext(Config.Options))
            //{
                Chairmen.ItemsSource = allowchairman.Select(p => p.Lname + ' ' + p.Fname + ' ' + p.Sname);
                                        //(from p in db.Chairmans
                                        //join c in db.Specialties on p.ChairmanId equals c.ChairmanId
                                        //join x in db.Disciplines on c.SpecialtyId equals x.SpecialtyId
                                        //join t in db.Teachings on x.DisciplineId equals t.DisciplineId
                                        //where (t.WorkerId == Config.User.WorkerId) && (((ComboBox)sender).SelectedItem.ToString()==x.DisciplineName)//Config.User.WorkerId
                                        //select p.Lname + ' ' + p.Fname + ' ' + p.Sname).Distinct().ToList();
            
           // }
            Chairmen.IsEnabled = true;
            Chairmen.SelectedIndex = -1;
            CountTickets_TextChanged(sender, e);
        }

        private void CountTickets_TextChanged(object sender, SelectionChangedEventArgs e)
        {

            using (var db = new GenerationTicketsContext(Config.Options))
            {
                if (DiscipDescList.SelectedIndex != -1)
                {
                    currentDiscipID = db.Disciplines.Where(x => x.DisciplineName == (DiscipDescList.SelectedItem.ToString())).Select(x => x.DisciplineId).FirstOrDefault();
                    var AllTasks = db.Tasks.Where(y =>
                       (y.DisciplineId == currentDiscipID)
                       && (y.Level.LeverDecryption == Lvl.SelectedItem.ToString())
                       ).Select(x => new { IDTask = x.TaskId, TypeTask = x.TypesTaskId }); // получение всех вопросов локально, их id и тип
                    if (Lvl.SelectedIndex != -1 && DiscipDescList.SelectedIndex != -1)
                    {
                        int CountTeorTask = db.Tasks.Where(y =>
                        (y.DisciplineId == currentDiscipID)
                        && (y.Level.LeverDecryption == Lvl.SelectedItem.ToString())
                        && (y.TypesTaskId == 2)
                        ).Select(x => x).Count();
                        int CountPractTask = db.Tasks.Where(y =>
                        (y.DisciplineId == currentDiscipID)
                        && (y.Level.LeverDecryption == Lvl.SelectedItem.ToString())
                        && (y.TypesTaskId == 1)
                        ).Select(x => x).Count();
                        MaxTickets.Text = ((CountTeorTask / 2 < CountPractTask) ? CountTeorTask / 2 : CountPractTask).ToString(); //реф
                                                                                                                                  //MaxTickets.Text = db.Tasks.Where(y =>
                                                                                                                                  //(y.DisciplineId == (int)(db.Disciplines.Where(x => x.DisciplineName == (DiscipList.SelectedItem.ToString())).Select(x => x.DisciplineId).FirstOrDefault()))
                                                                                                                                  //&& (y.Level.LeverDecryption == Lvl.SelectedItem.ToString())
                                                                                                                                  //).Select(x => x).Count().ToString();

                    }
                    else
                        MaxTickets.Text = "Unknows";
                }
            }
        }

        public async void GenerationintoDB(DbInteraction dbinteration,bool inDB)
        {
            if ((DoLocalGen.IsChecked == true) || (Lvl.SelectedIndex != -1 && DiscipDescList.SelectedIndex != -1 && _numValue > 0))
            {
                    
                    var AllTasks = dbinteration.GetTasks(DiscipDescList.SelectedItem.ToString(),Lvl.SelectedItem.ToString());
                    var teorTask = AllTasks.Where(x => x.TypesTaskId == 2).Select(x => x.TaskId).ToList();
                    var practTask = AllTasks.Where(x => x.TypesTaskId == 1).Select(x => x.TaskId).ToList();
                    Random random = new Random();
                    if (MaxTickets.Text != "Unknows" && _numValue <= int.Parse(MaxTickets.Text))
                    {
                    ProgressCheck.Value = 1;
                    ProgressCheck.Maximum = _numValue + 1;
                    localTickets = new List<Ticket>(_numValue);
                        for (int i = 1; i <= _numValue; i++)
                        {
                        await Trd.Task.Run(() =>
                        {
                            var item = random.Next(0, teorTask.Count - 1);
                            localTickets.Add(new Ticket() { TicketId = i, TaskNumber = 1, TaskId = (teorTask[item]), DisciplineId = currentDiscipID });
                            teorTask.RemoveAt(item);
                            item = random.Next(0, teorTask.Count - 1);
                            localTickets.Add(new Ticket() { TicketId = i, TaskNumber = 2, TaskId = (teorTask[item]), DisciplineId = currentDiscipID });
                            teorTask.RemoveAt(item);
                            item = random.Next(0, practTask.Count - 1);
                            localTickets.Add(new Ticket() { TicketId = i, TaskNumber = 3, TaskId = (practTask[item]), DisciplineId = currentDiscipID }); //Вопрос в инкременте бд
                            practTask.RemoveAt(item);
                        });
                        ProgressCheck.Value += 1;
                        };
                        if (inDB)
                        {
                            dbinteration.PullTickets(localTickets);
                        }
                        MessageBox.Show("Готово!");
                    ProgressCheck.Value = 0;
                    }
                    else
                    {
                        MessageBox.Show("Проверьте выбранное кол-во билетов");

                    }
            }
            
            //return true;
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
            if (NumValue > 0)
            {
                NumValue--;
            }
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
        private void Gen_Click(object sender, RoutedEventArgs e)
        {
            if (DoAddDB.IsChecked == true)
            {
                GenerationintoDB(dbInteraction, true);
            }
            else
            {
                GenerationintoDB(dbInteraction, false);
            }
        }

        private void DoAddDB_Checked(object sender, RoutedEventArgs e)
        {
            //_numValue = dbInteraction
            Lvl.IsEnabled = false;
            DiscipDescList.IsEnabled = false;
           // Course.IsEnabled = false;
           // Semestr.IsEnabled = false;
            counttick.IsEnabled = false;
            DoAddDB.IsEnabled = false;
            GenButton.IsEnabled = false;
            Chairmen.IsEnabled = true;
            Chairmen.ItemsSource = (dbInteraction.GetChairmanList(dbInteraction.GetDispfromTickets(listTickets))).Select(p => p.Lname + ' ' + p.Fname + ' ' + p.Sname);
            Chairmen.SelectedIndex = -1;
            DoAddDB.IsChecked = false;
        }
        private void DoAddDB_UnChecked(object sender, RoutedEventArgs e)
        {
            Lvl.IsEnabled = true;
            DiscipDescList.IsEnabled = true;
          //  Course.IsEnabled = true;
          //  Semestr.IsEnabled = true;
            counttick.IsEnabled = true;
            DoAddDB.IsEnabled = true;
            GenButton.IsEnabled = true;
            if (DiscipDescList.SelectedIndex == -1)
            {
                Chairmen.IsEnabled = false;
            }
            Chairmen.SelectedIndex = -1;

        }
        private static readonly Regex onlyNumbers = new Regex("[^0-9]");

        private static bool IsTextAllowed(string text)
        {
            return !onlyNumbers.IsMatch(text);
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void text_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = ((TextBox)sender).Text.Replace(" ", "");
        }
    }
}
