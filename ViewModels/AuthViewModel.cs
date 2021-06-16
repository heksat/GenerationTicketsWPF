using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GenerationTicketsWPF.Infrastructure.Commands;
using GenerationTicketsWPF.Models;
using GenerationTicketsWPF.ViewModels.Base;
using System.Windows.Navigation;

namespace GenerationTicketsWPF.ViewModels
{
    internal class AuthViewModel : ViewModel
    {
        #region Commands

        public ICommand LogInUserCommand { get; }

        private void OnLogInUserCommandExecuted (object p)
        {
            var db = new DbInteraction();
            Config.User = db.Auth(login, password);
            if (Config.User != null)
            {
                if (Config.User.RoleId == 1)
                {
                    db.checkactualdiscip();
                }
                Application.Current.MainWindow.Content = new MenuPage();
                    //NavigationService.Navigate(new MenuPage());
            }
            else
            {
                MessageBox.Show("Не получилося!(");
            }
        }

        private bool CanLogInUserCommandExecute(object p) => true;
        #endregion
        private string login;
        private SecureString password;

        public string Login
        {
            get => login;
            set => Set(ref login, value);
        }
        public SecureString Password
        {
            get => password;
            set => Set(ref password, value);
        }
        public AuthViewModel()
        {
            LogInUserCommand = new ActionCommand(OnLogInUserCommandExecuted, CanLogInUserCommandExecute);
        }

    }
}
