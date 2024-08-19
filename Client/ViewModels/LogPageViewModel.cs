using Client.Model;
using RqRsModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Client.Views;
using Client.ViewModels;
using System.Windows.Input;
using Client.Infrastructure;
using Client.Repository;

namespace Client.ViewModels
{
    internal class LogPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ICommand logInUser;
        public LogPageViewModel()
        {
            LogUser = new Users();
        }
        public ICommand LogInUser => logInUser ??= new RelayCommand(Log_In_User);
        public void Log_In_User(object? param)
        {
            if(logUser.Login != null && logUser.Password != null)
            {
                var bf = new BinaryFormatter();
                using (var tcpClient = new TcpClient())
                {
                    tcpClient.Connect("127.0.0.1", 9001);
                    using (var networkStream = tcpClient.GetStream())
                    {
                        var request = new Request
                        {
                            Title = "LOGIN",
                            RqLogin = logUser.Login,
                            RqPassword = logUser.Password
                        };
                        bf.Serialize(networkStream, request);
                        var response = (Response)bf.Deserialize(networkStream);
                        if(response.CheckLogOrPass == '+')
                        {
                            LoginRepository.Login = logUser.Login;
                            MainAppWindow appWindow = new MainAppWindow();
                            appWindow.ShowDialog();
                        }
                        else if(response.CheckLogOrPass == 'p')
                        {
                            MessageBox.Show("Password is not valid");
                        }
                        else if (response.CheckLogOrPass == 'n')
                        {
                            MessageBox.Show("This user does not exist");
                        }
                        networkStream.Flush();
                    }
                }
            }
            else
            {
                MessageBox.Show("Fill all fields");
            }
        }

        private Users logUser;

        public Users LogUser
        {
            get => logUser;
            set
            {
                if(value != logUser)
                {
                    logUser = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
