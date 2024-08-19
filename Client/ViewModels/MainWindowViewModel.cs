using Client.Infrastructure;
using Client.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.ViewModels
{
    internal class MainWindowViewModel
    {
        private ICommand openRegPage;
        private ICommand openLogPage;
        public ICommand OpenRegPage => openRegPage ??= new RelayCommand(OpenRegisterPage);
        public ICommand OpenLogPage => openLogPage ??= new RelayCommand(OpenLoginPage);
        private void OpenRegisterPage(object? param)
        {
            if (param is Frame fr)
            {
                var regPage = new Register();
                fr.Navigate(regPage);
            }
        }
        private void OpenLoginPage(object? param)
        {
            if(param is Frame fr)
            {
                var logPage = new Login();
                fr.Navigate(logPage);
            }
            
        }
    }
}
