using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Client.Infrastructure;
using Client.Model;
using RqRsModel;
namespace Client.ViewModels
{
    internal class RegPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ICommand regUser;
        public RegPageViewModel()
        {
            NewUser = new Users();
            ConfirmPass = "";
        }

        public ICommand RegUser => regUser ??= new RelayCommand(RegsUser);


        public void RegsUser(object? param)
        {
            if (newUser.Password != null && newUser.Login != null && confirmPass!= "")
            {
                if (confirmPass == newUser.Password)
                {
                    var bf = new BinaryFormatter();
                    using (var tcpClient = new TcpClient())
                    {
                        tcpClient.Connect("127.0.0.1", 9001);
                        using (var networkStream = tcpClient.GetStream())
                        {
                            var request = new Request
                            {
                                Title = "CHEK_DETAILS",
                                RqLogin = newUser.Login,
                                RqPassword = newUser.Password
                            };
                            bf.Serialize(networkStream, request);
                            var response = (Response)bf.Deserialize(networkStream);
                            if (response.Chek)
                            {
                                MessageBox.Show("All done! Thank you!");
                            }
                            else
                            {
                                MessageBox.Show("This login already exist");
                            }
                            networkStream.Flush();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Your password should be similar");
                }
            }
            else
            {
                MessageBox.Show("Fill all fields");
            }
        }





        private Users newUser;
        private string confirmPass;

        public string ConfirmPass
        {
            get => confirmPass;
            set
            {
                if(value != confirmPass)
                {
                    confirmPass = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Users NewUser
        {
            get => newUser;
            set
            {
                if(value != newUser)
                {
                    newUser = value;
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
