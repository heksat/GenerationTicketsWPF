using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;

namespace GenerationTicketsWPF
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        //private int? RoleId = null;
        //private int? DisciplineId = null;
        public Registration()
        {
            InitializeComponent();
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                //ListDiscipline.ItemsSource = db.Disciplines.Select(x => x.DisciplineName).ToList();
                ListRoles.ItemsSource = db.Roles.Select(x => x.RoleDecryption).ToList();
            }
        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int? roleid = null;          
            int countcheck = 0;
            if (!(LName.Text != "" && LName.Text.Length <= 50))
            {
                LName.BorderBrush = Brushes.Red;
            }
            else
                countcheck++;
            if (!(FName.Text != "" && FName.Text.Length <= 50))
            {
                FName.BorderBrush = Brushes.Red;
            }
            else
                countcheck++;
            if (!(SName.Text != "" && SName.Text.Length <= 50))
            {
                SName.BorderBrush = Brushes.Red;
            }
            else
                countcheck++;
            //if (ListDiscipline.SelectedIndex == -1)
            //{
            //    ListDiscipline.Background = new SolidColorBrush(Colors.Red); //не меняет цвет, условие рабочее. 
            //}
            //else {
            //    using (var db = new GenerationTicketsContext(Config.Options))
            //    {
            //        disciplineid = db.Disciplines.Where(x => x.DisciplineName == ListDiscipline.SelectedItem.ToString()).Select(x => x.DisciplineId).FirstOrDefault();
            //    }
            //    if (disciplineid==null)
            //        MessageBox.Show("Такой роли нет");
            //    else
            //        countcheck++;
            //}
            string gender = "";
            if (Woman.IsChecked == true)
            {
                countcheck++;
                gender = "ж";
            }
            else
             if (Man.IsChecked == true)
            {
                countcheck++;
                gender = "м";
            }
            if (ListRoles.SelectedIndex == -1)
            {
                ListRoles.BorderBrush = new SolidColorBrush(Colors.Red); //не меняет цвет, условие рабочее. 
            }
            else
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    roleid = db.Roles.Where(x => x.RoleDecryption == ListRoles.SelectedItem.ToString()).Select(x => x.RoleId).FirstOrDefault();
                }
                if (roleid == null)
                    MessageBox.Show("Такой роли нет");
                else
                    countcheck++;
            }
                if (Login.Text != "" && Login.Text.Length>5)
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    if ((db.Workers.Select(x => x.WorkerLogin).Contains(Login.Text)))
                    {
                        Login.BorderBrush = Brushes.Red;
                    }
                    else
                        countcheck++;
                }
            }
            else
            {
                Login.BorderBrush = Brushes.Red;
            }
            if (!(Password.Password != "" && Password.Password.Length <= 50))
            {
                Password.BorderBrush = TryPassword.BorderBrush = Brushes.Red;
                if (Password.Password == "")
                {
                    MessageBox.Show("Заполните поле пароля");
                }
                else
                {
                    MessageBox.Show("Пароль должен быть короче 50 символов");
                }
            }
            else
                if (Password.Password.Equals(TryPassword.Password))
            {
                countcheck += 2;
            }
            else
            {
                Password.BorderBrush = Brushes.Red;
                MessageBox.Show("Пароли не равны");
            }
            if (countcheck == 8)
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
#warning: Нужно реализовать добавление в teaching учителей и добавить возможность выбора дисциплин
                    db.Workers.Add(new Worker() { Lname = LName.Text, Fname = FName.Text, Sname = SName.Text, Gender = gender, WorkerLogin = Login.Text, RoleId = (int)roleid, WorkerPassword = Password.Password });
                   db.SaveChanges();
                    //var i = db.Database.ExecuteSqlRaw($"insert into Workers (Lname,Fname,Sname,Discipline_ID,Worker_Login,Worker_password,Role_id) values('{LName.Text}'," +
                    //    $"'{FName.Text}'," +
                    //    $"'{SName.Text}'," +
                    //    $"{db.Disciplines.Where(x => x.DisciplineName == ListDiscipline.SelectedItem.ToString()).Select(x => x.DisciplineId).FirstOrDefault()}," +
                    //    $"'{Login.Text}'," +
                    //    $"'{Password.Password}'," +
                    //    $"{db.Roles.Where(x => x.RoleDecryption == ListRoles.SelectedItem.ToString()).Select(x => x.RoleId).FirstOrDefault()})");
                    //db.Workers.Add(new Worker() { Lname = LName.Text, 
                    //    Fname = FName.Text, 
                    //    Sname = SName.Text, 
                    //    DisciplineId = db.Disciplines.Where(e => e.DisciplineName == ListDiscipline.DataContext.ToString()).Select(e=>e.DisciplineId).First(),
                    //    WorkerLogin = Login.Text,
                    //    WorkerPassword = Password.Password,
                    //    RoleId=2});
                }
            }
            
        }

        private void MouseClick(object sender, RoutedEventArgs e)
        {
            var el = (Control)sender;
            el.BorderBrush = Brushes.DarkGray;//Brushes.Gray;//new SolidColorBrush(Color.FromRgb(FFABADB3);
        }

        //private void ListRoles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    using (var db = new GenerationTicketsContext(Config.Options)) {
        //        RoleId = db.Roles.Where(x => x.RoleDecryption == ((ComboBox)sender).SelectedItem.ToString()).Select(x => x.RoleId).FirstOrDefault();
        //        MessageBox.Show($"{RoleId}");
        //    }
        //}
    }
}
