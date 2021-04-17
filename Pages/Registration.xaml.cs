﻿using System;
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
using GenerationTicketsWPF.Models;
namespace GenerationTicketsWPF
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        /// <summary>
        /// Новое окно выбора дисциплин
        /// </summary>
        private Window window;
        /// <summary>
        /// Список id дисциплин
        /// </summary>
        List<string> choicelistdisname;
        public Registration()
        {
            InitializeComponent();
            using (var db = new GenerationTicketsContext(Config.Options))
            {
                //ListDiscipline.ItemsSource = db.Disciplines.Select(x => x.DisciplineName).ToList();
                ListRoles.ItemsSource = db.Roles.Select(x => x).ToList();
            }
        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {        
            int countcheck = 0;
            var Dbhelper = new DbInteraction();
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
                MessageBox.Show("Такой роли нет");
            }
            else
            {
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
                //using (var db = new GenerationTicketsContext(Config.Options))
                //{
                    Dbhelper.AddUser(new Worker() { Lname = LName.Text, Fname = FName.Text, Sname = SName.Text, Gender = gender, WorkerLogin = Login.Text, RoleId = (int)ListRoles.SelectedValue, WorkerPassword = Password.Password },choicelistdisname);
                //    var newuser = db.Workers.Add(new Worker() { Lname = LName.Text, Fname = FName.Text, Sname = SName.Text, Gender = gender, WorkerLogin = Login.Text, RoleId = (int)ListRoles.SelectedValue, WorkerPassword = Password.Password });
                //    db.SaveChanges();
                //    if (((Role)ListRoles.SelectedItem).RoleDecryption == "Teacher")
                //    {
                //        var id = db.Workers.Where(x => x.WorkerLogin == Login.Text).Select(x => x.WorkerId).FirstOrDefault();
                //        if (id != 0) {
                //            foreach (var i in choicelistdisname) {
                //                var tempid = (int)db.Disciplines.Where(x => x.DisciplineName == i).Select(x => x.DisciplineId).FirstOrDefault();
                //                if (tempid != 0)
                //                    db.Teachings.Add(new Teaching() { WorkerId = id, DisciplineId = tempid });
                //                else
                //                    MessageBox.Show("Какая-то беда");
                //            }
                //            db.SaveChanges();
                //        }
                //        else
                //            MessageBox.Show("Какая-то беда");
                //    }
                    
                //}
            }
            
        }

        private void MouseClick(object sender, RoutedEventArgs e)
        {
            var el = (Control)sender;
            el.BorderBrush = Brushes.DarkGray;//Brushes.Gray;//new SolidColorBrush(Color.FromRgb(FFABADB3);
        }

        private void choise_subject(object sender, RoutedEventArgs e)
        {
            window = new Window() { Width = 300 };
            var DbHelper = new DbInteraction();
            var disciplineName = DbHelper.GetDisciplinesDecryption();
            StackPanel panel = new StackPanel { Orientation = Orientation.Vertical };
            foreach (var name in disciplineName)
            {
                panel.Children.Add(new CheckBox() { Content = name, Height = 20 });
            }
            window.Content = panel;
            var but = new Button() {Content = "Готово!" };
            but.Click += Confirmed_Choice;
            panel.Children.Add(but);
            window.Show();
        }
        private void Confirmed_Choice(object sender, RoutedEventArgs e)
        {
            var data = (StackPanel)window.Content;
            choicelistdisname = new List<string>(data.Children.Count - 1);
                foreach (var i in data.Children.OfType<CheckBox>())
                {
                    if (i.IsChecked == true)
                    {
                        choicelistdisname.Add(i.Content.ToString());
                    }

                }
            window.Close();
        }

        private void ListRoles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((Role)((ComboBox)sender).SelectedItem).RoleDecryption == "Teacher")
            {
                Predmets.IsEnabled = true;
            }
            else
                Predmets.IsEnabled = false;
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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