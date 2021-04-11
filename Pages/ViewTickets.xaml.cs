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
using GenerationTicketsWPF.Models;

namespace GenerationTicketsWPF.Pages
{
    /// <summary>
    /// Interaction logic for ViewTickets.xaml
    /// </summary>
    public partial class ViewTickets : Page
    {
        public ViewTickets()
        {
            InitializeComponent();
            TicketsGrid.ItemsSource = (System.Collections.IEnumerable)(new DbInteraction()).ListTickets();
        }
    }
}
