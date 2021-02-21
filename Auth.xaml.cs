using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace GenerationTicketsWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());// установка пути к текущему каталогу
            builder.AddJsonFile("appconfig.json");// получаем конфигурацию из файла appsettings.json
            var config = builder.Build(); // создаем конфигурацию
            string connectionString = config.GetConnectionString("DefaultConnection");// получаем строку подключения

            var optionsBuilder = new DbContextOptionsBuilder<GenerationTicketsContext>();
            var options = optionsBuilder
                .UseSqlServer(connectionString)
                .Options;
            using (GenerationTicketsContext db = new GenerationTicketsContext(options))
            {
                //Chairman user1 = new Chairman { Lname = "чек", Fname = "Tom", Sname = "kek" };
                // Добавление
                // db.Chairmans.Add(user1);
                // db.SaveChanges();
                var users = db.Chairmans.ToList();
                Console.WriteLine("Данные после добавления:");
                //txb_Selected.Text = "";
                foreach (Chairman u in users)
                {
                    // txb_Selected.Text += ($"{u.Lname} - {u.Fname} - {u.Sname}\n");
                    //Console.WriteLine($"{u.Lname} - {u.Fname} - {u.Sname}");
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            main.Show();
            this.Hide();
        }
    }
}
