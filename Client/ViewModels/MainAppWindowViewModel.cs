using Client.Model;
using Client.Repository;
using RqRsModel;
using Client.Views;
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
using System.Windows.Automation;
using System.Windows;
using System.Windows.Input;
using Client.Infrastructure;

namespace Client.ViewModels
{
    internal class MainAppWindowViewModel : INotifyPropertyChanged
    {
        //Позакоментовував, тому що на цьому етапі, мені потрібно пико що створити правильно і правильні навгаійні властивості
        //які будуть взаємодіяти з вікном. До нас прийшли слова з бази, тепер їх потрібно розподілити по властивостям
        //та обсервабле колекшинам. Також мені здалось, що не потрібно створювати окрему змінну для логіну, тому що
        //можна створити обєкт юзер і використати змінну обєкту юзер, у яку присвоється значення з репозиторію
        public event PropertyChangedEventHandler? PropertyChanged;
        private bool selected_1;
        private bool selected_2;
        private bool selected_3;
        private Users login;
        private EnglishWordsWPF engWord;
        private UkrainianWordsWPF ukrWord;
        private UkrainianWordsWPF answear_var1;
        private UkrainianWordsWPF answear_var2;
        private UkrainianWordsWPF answear_var3;
        private bool testContent;
        private bool grammaContent;
        private Visibility testVisibility;
        private Visibility grammaVisibility;
        private ICommand gramAnswearCommand;
        private ICommand answearCommand;
        public ICommand AnswearCommand => answearCommand ??= new RelayCommand(btnAnswear);
        public ICommand GramAnswearCommand => gramAnswearCommand ??= new RelayCommand(btnGrammAnswear);
        public MainAppWindowViewModel()
        {
            Login = new Users();
            Login.login = LoginRepository.Login;
            
            //EngWord = new EnglishWordsWPF();
            Answear_var1 = new UkrainianWordsWPF();
            Answear_var2 = new UkrainianWordsWPF();
            Answear_var3 = new UkrainianWordsWPF();
            radioButtons = [];
            radioButtons.Add(Answear_var1);
            radioButtons.Add(Answear_var2);
            radioButtons.Add(Answear_var3);
            Start_And_RestartLesson();
            //Start_Lesson();
            //Form_Question();
            //TestVisibility = Visibility.Visible;
            //GrammaVisibility = Visibility.Hidden;
            //TestContent = true;
            //GrammaContent = false;
        }
        public void Start_And_RestartLesson()
        {
            EngWord = new EnglishWordsWPF();
            //Answear_var1 = new UkrainianWordsWPF();
            //Answear_var2 = new UkrainianWordsWPF();
            //Answear_var3 = new UkrainianWordsWPF();
            //radioButtons = [];
            //radioButtons.Add(Answear_var1);
            //radioButtons.Add(Answear_var2);
            //radioButtons.Add(Answear_var3);
            Start_Lesson();
            Form_Question();
            TestVisibility = Visibility.Visible;
            GrammaVisibility = Visibility.Hidden;
            TestContent = true;
            GrammaContent = false;
        }
        //public ObservableCollection<EnglishWordsWPF> EnglishWords { get; set; }
        public ObservableCollection<UkrainianWordsWPF> UkrainianWords { get; set; }
        private Dictionary<EnglishWordsWPF, bool> progress;
        private List<(EnglishWordsWPF, bool)> progress2;
        private List<UkrainianWordsWPF> radioButtons;
        public Visibility TestVisibility
        {
            get => testVisibility;
            set
            {
                if(value != testVisibility)
                {
                    testVisibility = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Visibility GrammaVisibility
        {
            get => grammaVisibility;
            set
            {
                if(value != grammaVisibility)
                {
                    grammaVisibility = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool GrammaContent
        {
            get => grammaContent;
            set
            {
                if(value!= grammaContent)
                {
                    grammaContent = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool TestContent
        {
            get=> testContent;
            set
            {
                if(value != testContent)
                {
                    testContent = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public UkrainianWordsWPF Answear_var1
        {
            get => answear_var1;
            set
            {
                if (value != answear_var1)
                {
                    answear_var1 = value;
                }
            }
        }
        public UkrainianWordsWPF Answear_var2
        {
            get => answear_var2;
            set
            {
                if (value != answear_var2)
                {
                    answear_var2 = value;
                }
            }
        }
        public UkrainianWordsWPF Answear_var3
        {
            get => answear_var3;
            set
            {
                if (value != answear_var3)
                {
                    answear_var3 = value;
                }
            }
        }
        public UkrainianWordsWPF UkrWord
        {
            get => ukrWord;
            set
            {
                if(value != ukrWord)
                {
                    ukrWord = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public EnglishWordsWPF EngWord
        {
            get => engWord;
            set
            {
                if (value != engWord)
                {
                    engWord = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public Users Login
        {
            get => login;
            set
            {
                if (value != login)
                {
                    login = value;
                    NotifyPropertyChanged();
                }
            }
        }
        /*
          Отримуємо з радіо батона варінт, потім це слово разом з англійським словом відправляємо на сервер
          і там дивимось через запит у базу даних чи правильний переклад, якщо так, відправляємо бульку тру якщо ні фолс
          Тест далі продовжеє тривати, у методі кнопки викликаємо метод Form_Question. Уявімо що користувач відповів правильно
        і сервер повернув тру. У методі кнопки міняємо бульку до слова яке було задано до перекладу на тру, при наступному
        виклику метада Form_Question це слово не буде задаватись користувачу. Метод потрібно викликати у самому кінці.
        */
        public bool Selected_1 
        {
            get => selected_1; 
            set
            {
                if(value != selected_1)
                {
                    selected_1 = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool Selected_2
        {
            get => selected_2;
            set
            {
                if (value != selected_2)
                {
                    selected_2 = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public bool Selected_3
        {
            get => selected_3;
            set
            {
                if (value != selected_3)
                {
                    selected_3 = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public void CheckAnswear(string engWord, string ukrWord)
        {
            var bf = new BinaryFormatter();
            using (var tcpClient = new TcpClient())
            {
                tcpClient.Connect("127.0.0.1", 9001);
                using(var networkStream = tcpClient.GetStream())
                {
                    var request = new Request
                    {
                        Title = "CHECK_ANSWEAR",
                        RqEWord = engWord,
                        RqUWord = ukrWord
                    };
                    bf.Serialize(networkStream, request);
                    var response = (Response)bf.Deserialize(networkStream);

                    //for (int i = 0; i < progress2.Count; i++)
                    //{
                    //    if (progress2[i].Item1.EngWordLesson == Question.EngWordLesson)
                    //    {
                    //        progress2[i].Item2 = true;
                    //    }
                    //}
                    var index = progress2.FindIndex(i => i.Item1.EngWordLesson == EngWord.EngWordLesson);
                    
                        progress2[index] =  (new EnglishWordsWPF() {EngWordLesson = EngWord.EngWordLesson }, response.Chek );
                    //var wordDone = progress2.Where(e => e.Item1.EngWordLesson == Question.EngWordLesson).SingleOrDefault();
                    //wordDone.Item2 = response.Chek;

                    networkStream.Flush();
                }
            }
        }
        public void btnGrammAnswear(object? param)
        {
            Check_Gram_Answear();
            if(CheckProgress())
            {
                //string messageBoxText = "Would you like to continue?";
                //MessageBoxButton button = MessageBoxButton.YesNo;
                //MessageBoxImage icon = MessageBoxImage.Question;
                //MessageBoxResult result;
                if(progress2.Count() > 1)
                {
                    MessageBox.Show($"Well done!\n You studied {progress2.Count} new words!");
                   if(MessageBox.Show("Would you like to continue?", "Please select", MessageBoxButton.YesNo, 
                       MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Start_And_RestartLesson();
                    }
                    else
                    {
                        MessageBox.Show("Thank you for visit)\nBye!)");
                        if(param is Window wm)
                        {
                            wm.Close();
                        }
                        //цей варіант використовується там, де немає можливості передати параметром вікно
                        //Application.Current.Windows[2].Close();
                    }
                }
                else
                {
                    MessageBox.Show($"Well done!\n You studied {progress2.Count} new word!");
                    if (MessageBox.Show("Would you like to continue?", "Please select", MessageBoxButton.YesNo,
                       MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Start_And_RestartLesson();
                    }
                    else
                    {
                        MessageBox.Show("Thank you for visit)\nBye!)");
                        
                    }
                }
                SetProgress();
            }
            else
            {
                Form_Gram_Question();
            }
        }
        public void SetProgress()
        {
            var bf = new BinaryFormatter();
            using (var tcpClient = new TcpClient())
            {
                tcpClient.Connect("127.0.0.1", 9001);
                using (var networkStream = tcpClient.GetStream())
                {
                    var request = new Request
                    {
                        Title = "SET_PROGRESS",
                        RqLogin = Login.login
                    };
                    
                    for(int i =0; i< progress2.Count;i++)
                    {
                        request.RqEWords.Add(progress2[i].Item1.EngWordLesson);
                    }
                    bf.Serialize(networkStream, request);
                    var response = (Response)bf.Deserialize(networkStream);
                    networkStream.Flush();
                }

            }
            
        }
        public void btnAnswear(object? param)
        {
            if(Selected_1)
            {
                CheckAnswear(EngWord.EngWordLesson, Answear_var1.UkrWordLesson);
                
            }
            else if(Selected_2)
            {
                CheckAnswear(EngWord.EngWordLesson, Answear_var2.UkrWordLesson);
                
            }
            else if(Selected_3)
            {
                CheckAnswear(EngWord.EngWordLesson, Answear_var3.UkrWordLesson);
                
            }
            if(CheckProgress())
            {
                MessageBox.Show("Well done!\nNow, let's start second part of study");
                Reset_Prog_Eng_Words();
                Form_Gram_Question();
                TestContent = false;
                TestVisibility = Visibility.Hidden;
                GrammaContent = true;
                GrammaVisibility = Visibility.Visible;
            }
            else
            {
                Form_Question();
            }
            
        }
        public bool CheckProgress()
        {
            for(int i = 0; i< progress2.Count;i++)
            {
                if (progress2[i].Item2 == false)
                {
                    return false;
                }
            }
            return true;
        }
        public void Form_Question()
        {
            Selected_1 = false;
            Selected_2 = false;
            Selected_3 = false;
            bool job = true;
            Random rndE = new Random();
            int i = 0;
            int iA = 0;
            while (job)
            {
                i = rndE.Next(0, progress2.Count);
                iA = rndE.Next(0, 3);
                if (!progress2[i].Item2)
                {
                    EngWord.EngWordLesson = progress2[i].Item1.EngWordLesson;
                    radioButtons[iA].UkrWordLesson = UkrainianWords[i].UkrWordLesson;
                    while (job)
                    {
                        int iU1 = rndE.Next(0, UkrainianWords.Count);
                        int iU2 = rndE.Next(0, UkrainianWords.Count);
                        if (iU1 != iU2 && iU1 != i && iU2 != i)
                        {
                            bool c = false;
                            for (int j = 0; j < radioButtons.Count; j++)
                            {
                                if (j != iA)
                                {
                                    if (!c)
                                    {
                                        radioButtons[j].UkrWordLesson = UkrainianWords[iU1].UkrWordLesson;
                                        c = true;
                                    }
                                    else
                                    {
                                        radioButtons[j].UkrWordLesson = UkrainianWords[iU2].UkrWordLesson;
                                        job = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            /*
            bool check = true;
            List<EnglishWordsWPF> eWords = new List<EnglishWordsWPF>();
            foreach (var kvp in progress)
            {
                if (kvp.Value == false)
                {
                    eWords.Add(kvp.Key);
                    check = false;
                    break;
                }
            }
            */
        }



        public void Form_Gram_Question()
        {
            EngWord.EngWordLesson = "";
            Random rnd = new Random();
            bool job = true;
            while (job)
            {
                //було              UkrainianWords.Count()
                int i = rnd.Next(0, progress2.Count());
                if (!progress2[i].Item2)
                {
                    UkrWord = UkrainianWords[i];
                    job = false;
                }
            }
            
        }
        public void Check_Gram_Answear()
        {
            bool messBoxActive = true;
            for (int i = 0; i < progress2.Count; i++)
            {
                //перевірити що приходить з текст боксу у 486 рядку після натиску enter
                if (progress2[i].Item1.EngWordLesson.ToLower() == EngWord.EngWordLesson.ToLower())
                {
                    messBoxActive = false;
                    //var index = progress2.FindIndex(i => i.Item1.EngWordLesson == EngWord.EngWordLesson);

                    //progress2[index] = (new EnglishWordsWPF() { EngWordLesson = EngWord.EngWordLesson }, response.Chek);
                    progress2[i] = (new EnglishWordsWPF() { EngWordLesson = progress2[i].Item1.EngWordLesson }, true);
                    break;
                }
            }
            if(messBoxActive)
            {
                MessageBox.Show("Your answear is not correct. You can try a little later");
            }
            
        }
        public void Reset_Prog_Eng_Words()
        {
            for (int i = 0; i< progress2.Count;i++)
            {
                string temp = null;
                temp = progress2[i].Item1.EngWordLesson;
                progress2[i] = (new EnglishWordsWPF() { EngWordLesson = temp }, false);
            }

        }






        public void Start_Lesson()
        {
            var bf = new BinaryFormatter();
            using (var tcpClient = new TcpClient())
            {
                tcpClient.Connect("127.0.0.1", 9001);
                using (var networkStream = tcpClient.GetStream())
                {
                    var request = new Request
                    {
                        Title = "START_LESSON",
                        RqLogin = Login.login
                    };
                    bf.Serialize(networkStream, request);
                    var response = (Response)bf.Deserialize(networkStream);
                    //EnglishWords = new ObservableCollection<EnglishWordsWPF>();
                    if (response.RqEWords.Count == 0)
                    {
                        MessageBox.Show("Congratulation!!! You studiet all words!!!)))\n Please wait for update. Bye!)");
                        Application.Current.Windows[2].Close();
                    }
                    else
                    {
                        UkrainianWords = new ObservableCollection<UkrainianWordsWPF>();
                        progress2 = [];
                        //progress = new Dictionary<EnglishWordsWPF, bool>();

                        for (int i = 0; i < response.RqEWords.Count; i++)
                        {
                            //EnglishWords.Add(new EnglishWordsWPF { EngWordLesson = response.RqEWords[i] });
                            progress2.Add((new EnglishWordsWPF { EngWordLesson = response.RqEWords[i] }, false));
                        }
                        for (int i = 0; i < response.RqUWords.Count; i++)
                        {
                            UkrainianWords.Add(new UkrainianWordsWPF { UkrWordLesson = response.RqUWords[i] });
                        }
                    }
                    //if (progress2.Count() == 0)
                    //{
                    //    MessageBox.Show("Congratulation!!! You studiet all words!!!)))\n Please wait for update. Bye!)");
                    //    Application.Current.Windows[1].Close();
                    //}
                    networkStream.Flush();
                }
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
