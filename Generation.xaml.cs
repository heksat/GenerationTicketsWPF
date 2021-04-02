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
using System.Linq;
namespace GenerationTicketsWPF
{
    /// <summary>
    /// Interaction logic for Generation.xaml
    /// </summary>

    public partial class Generation : Page
    {
        private List<Task> teorqesta;
        private List<Task> practqesta;
        private int currentDiscipID;
        public Generation()
        {
            InitializeComponent();
            MaxTickets.Text = "Unknows";
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                DiscipList.ItemsSource = (from p in db.Disciplines
                                          join c in db.Teachings on p.DisciplineId equals c.DisciplineId
                                          where c.WorkerId == 1//Config.User.WorkerId
                                          select p.DisciplineName).ToList();
                Lvl.ItemsSource= db.Levels.Select(x => x.LeverDecryption).ToList();
            }

        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            if (Lvl.SelectedIndex != -1 && DiscipList.SelectedIndex != -1)
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    var AllTasks = db.Tasks.Where(y =>
                        (y.DisciplineId == (int)(db.Disciplines.Where(x => x.DisciplineName == (DiscipList.SelectedItem.ToString())).Select(x => x.DisciplineId).FirstOrDefault()))
                        && (y.Level.LeverDecryption == Lvl.SelectedItem.ToString())
                        ).Select(x => new { IDTask = x.TaskId, TypeTask = x.TypesTaskId });
                    var teorTask = AllTasks.Where(x => x.TypeTask == 2).Select(x => x.IDTask).ToList();
                    var practTask = AllTasks.Where(x => x.TypeTask == 1).Select(x => x.IDTask).ToList();
                    Random random = new Random();
                    int neededTickets = 0;
                    if (int.TryParse(CountTickets.Text, out neededTickets))
                    {
                        if (MaxTickets.Text!="Unknows" && neededTickets>0 && neededTickets<=int.Parse(MaxTickets.Text)) {
                            db.Tickets.RemoveRange(db.Tickets);
                            for (int i = 1; i <= neededTickets; i++)
                            {
                                var item = random.Next(0, teorTask.Count-1);
                                db.Tickets.Add(new Ticket() { TicketId=i ,TaskNumber = 1, TaskId = (teorTask[item]), DisciplineId = currentDiscipID, ChairmanId = 1 });
                                teorTask.RemoveAt(item);
                                item = random.Next(0, teorTask.Count-1);
                                db.Tickets.Add(new Ticket() { TicketId = i,TaskNumber = 2, TaskId = (teorTask[item]), DisciplineId = currentDiscipID, ChairmanId = 1 });
                                teorTask.RemoveAt(item);
                                item = random.Next(0, practTask.Count-1);
                                db.Tickets.Add(new Ticket() { TicketId = i,TaskNumber = 3, TaskId = (practTask[item]), DisciplineId = currentDiscipID, ChairmanId = 1 }); //Вопрос в инкременте бд
                                practTask.RemoveAt(item);
                            };
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось конвертировать кол-во билетов");
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
                                        where (t.WorkerId == 1) && (((ComboBox)sender).SelectedItem.ToString()==x.DisciplineName)//Config.User.WorkerId
                                        select p.Lname + ' ' + p.Fname + ' ' + p.Sname).Distinct().ToList();
            
            }
            CountTickets_TextChanged(sender, e);
        }

        private void CountTickets_TextChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                currentDiscipID = db.Disciplines.Where(x => x.DisciplineName == (DiscipList.SelectedItem.ToString())).Select(x => x.DisciplineId).FirstOrDefault();
                var AllTasks = db.Tasks.Where(y =>
                   (y.DisciplineId == currentDiscipID)
                   && (y.Level.LeverDecryption == Lvl.SelectedItem.ToString())
                   ).Select(x => new {IDTask = x.TaskId, TypeTask = x.TypesTaskId }); // получение всех вопросов локально, их id и тип


                if (Lvl.SelectedIndex != -1 && DiscipList.SelectedIndex != -1)
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
                    MaxTickets.Text = ((CountTeorTask/2 < CountPractTask) ? CountTeorTask/2 : CountPractTask).ToString();
#warning:Двойное деление в одном ифе, чет придумать
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
}
