﻿using System;
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
using GenerationTicketsWPF.Models;
using GenerationTicketsWPF.Pages;
namespace GenerationTicketsWPF.Pages
{
    /// <summary>
    /// Interaction logic for AddTickets.xaml
    /// </summary>
    public partial class AddTickets : Page
    {
        public AddTickets()
        {
            InitializeComponent();
            CounterChar.Text = $"0 / {TicketBox.MaxLength}";
            var DBhelper = new DbInteraction();
            DiscipAllow.ItemsSource = DBhelper.GetAllowDisciplines();
            LvlList.ItemsSource = DBhelper.GetLevels().Select(x=>x.LeverDecryption);
       
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CounterChar.Text = $"{((TextBox)sender).Text.Length} / {((TextBox)sender).MaxLength}";
        }
#warning:требуется рефакторинг    
        private void AddTick_Click(object sender, RoutedEventArgs e)
        {
            var DBhelper = new DbInteraction();
            string rbcontext = "";
            foreach (var i in TypeTick.Children.OfType<RadioButton>()) { 
                if (i.IsChecked == true)
                {
                    rbcontext = i.Content.ToString();
                    break;
                }
            }
            if (rbcontext == "" || LvlList.SelectedIndex==-1 || DiscipAllow.SelectedIndex==-1)
            {
                MessageBox.Show("Выберите тип задания!");
            }
            else
            {
                using (var db = new GenerationTicketsContext(Config.Options))
                {
                    var iddisp = db.Disciplines.Where(x => x.DisciplineName == DiscipAllow.SelectedItem.ToString()).Select(x => x.DisciplineId).FirstOrDefault();
                    var idtype = db.TypesTasks.Where(x => x.TypesTaskDecryption == rbcontext).Select(x => x.TypesTaskId).FirstOrDefault();
                    var idlvl = db.Levels.Where(x => x.LeverDecryption == LvlList.SelectedItem.ToString()).Select(x => x.LevelId).FirstOrDefault();
                    TicketBox.Text.Trim();
                    if (TicketBox.Text == string.Empty)
                    {
                        MessageBox.Show("Вы пытаетесь добавить пустой билет, не хорошо так делать");
                    }
                    else
                    {
                        if (iddisp != 0 && idtype != 0 && idlvl != 0)
                        {
                            DBhelper.TaskAdd(new Task() { TaskDecryption = TicketBox.Text, DisciplineId = iddisp, WorkerId = Config.User.WorkerId, TypesTaskId = idtype, LevelId = idlvl });
                            MessageBox.Show("Билет добавлен");
                        }
                        else
                            MessageBox.Show("Беда!");
                    }
                }
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ChanTick_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Changetasks());
        }
    }
}
