using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    internal class EnglishWordsWPF : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string engWordLesson;

        public string EngWordLesson
        {
            get => engWordLesson;
            set
            {
                if(value != engWordLesson)
                {
                    engWordLesson = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public override string ToString()
        {
            return EngWordLesson;
        }
        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
