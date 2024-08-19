using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    internal class UkrainianWordsWPF : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public string ukrWordLesson;

        public string UkrWordLesson
        {
            get => ukrWordLesson;
            set
            {
                if(value != ukrWordLesson)
                {
                    ukrWordLesson = value;
                    NotifyPropertyChanged();
                }
            }
        }


        public override string ToString()
        {
            return UkrWordLesson;
        }
        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
