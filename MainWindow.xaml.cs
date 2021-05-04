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
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Security.Permissions;

namespace GenerationTicketsWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        public MainWindow()
        {
            InitializeComponent();
        }
        
       // public static Click Goback()
       // public void GoBack_Click(object sender, RoutedEventArgs e)
       // {
       //     NavigationService.GoBack();
       // }
    }
}
