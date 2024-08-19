using Server.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class DbInstance
    {
        private readonly AdminDbContext db;
        public DbInstance()
        {
            DbContextBuilder builder = new();
            db = builder.CreateDbContext([]);
        }

        public void SetWords(string eWord, string uWord)
        {
            if (eWord != "" && uWord != "")
            {
                db.EnglishWords.Add(new EnglishWords() { EWord = eWord });
                db.SaveChanges();
                db.UkrainianWords.Add(new UkrainianWords() { UWord = uWord });
                db.SaveChanges();
                int eId = db.EnglishWords.Where(e => e.EWord == eWord).Select(e => e.Id).SingleOrDefault();
                int uId = db.UkrainianWords.Where(u => u.UWord == uWord).Select(u => u.Id).SingleOrDefault();
                db.EnglishWordsUkrainianWords.Add(new EnglishWordsUkrainianWords() { EnglishWordsId = eId, UkrainianWordsId = uId });
                db.SaveChanges();
            }
        }
        public bool CheckAndRegUser(string login, string password)
        {
            
            string check = db.Users.Where(l => l.Login == login).Select(l=> l.Login).SingleOrDefault();
            if (check == null)
            {
                db.Users.Add(new User() { Login = login, Password = password });
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public char CheckLogAndPass(string login, string password)
        {
            char check;
            string log = db.Users.Where(l => l.Login == login).Select(l => l.Login).SingleOrDefault();
            string pass;
            if(log != null && log == login)
            {
                pass = db.Users.Where(l => l.Login == login).Select(p => p.Password).SingleOrDefault();
                if (pass == password)
                {
                    check = '+';

                }
                else
                {
                    check = 'p';
                }

            }
            else
            {
                check = 'n';
            }
            return check;
        }
        public void DeleteWords(int wordId)
        {
            var delEUWord = db.EnglishWordsUkrainianWords.Where(w => w.EnglishWordsId == wordId).SingleOrDefault();
            if (delEUWord != null)
            {
                db.EnglishWordsUkrainianWords.Remove(delEUWord);
                db.SaveChanges();
            }
                var delEWord = db.EnglishWords.Where(w => w.Id == wordId).SingleOrDefault();
                db.EnglishWords.Remove(delEWord);
            db.SaveChanges();
                var delUWord = db.UkrainianWords.Where(w => w.Id == wordId).SingleOrDefault();
                db.UkrainianWords.Remove(delUWord);
            db.SaveChanges();
        }

        //public UkrainianWords FindWord(string eWord)
        //{
        //    //var res = db.EnglishWordsUkrainianWords.Where(eu=> eu.EnglishWordsId == db.EnglishWords
        //    //.Where(e => e.EWord == /*eWord*/"car").Select(e => e.Id).FirstOrDefault()).Select(u=> u.UkrainianWords).FirstOrDefault();
        //    //return res;
        //    //db.UkrainianWords.Where(i => i.Id == db.EnglishWordsUkrainianWords.Where(e => e.EnglishWords.EWord == eWord)
        //    //.Select(e => e.).SingleOrDefault();
        //    //return db.UkrainianWords.Where(i => i.Id == db.EnglishWordsUkrainianWords
        //    // .Where(e => e.EnglishWords.EWord == eWord).Select(e => e.Id).SingleOrDefault);
        //}

        public bool CheckAnsw(string engWord, string ukrWord)
        {
            var uId = db.UkrainianWords.Where(u => u.UWord == ukrWord).Select(i => i.Id).SingleOrDefault();
            var eId = db.EnglishWords.Where(e => e.EWord == engWord).Select(i => i.Id).SingleOrDefault();
            var euId = db.EnglishWordsUkrainianWords.Where(e => e.EnglishWordsId == eId && e.UkrainianWordsId == uId).SingleOrDefault();
            if(euId == null)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }
        public void SetProgress(List<string> eWDone, string login)
        {
            int uId = db.Users.Where(l => l.Login == login).Select(i => i.Id).SingleOrDefault();
            int wId;
            for(int i =0;i<eWDone.Count;i++)
            {
                wId = db.EnglishWords.Where(w => w.EWord == eWDone[i]).Select(i => i.Id).SingleOrDefault();
                db.Progresses.Add(new Progress() { UserId = uId, EnglishWordsId = wId });
                db.SaveChanges();
            }
        }
        public (List<string>, List<string>) GetFiveWordsForLesson(string login)
        {
            Random rnd = new Random();
            var eWord = new List<string>();
            var uWord = new List<string>();
            int job;
            int uId;
            int count = 0;
            //bool checkWordInProgress = false;
            uId = db.Users.Where(l => l.Login == login).Select(i => i.Id).FirstOrDefault();
            var tempWords = db.EnglishWords.ToList();
            List<EnglishWords> tempEW = new List<EnglishWords>();
            for(int i =0; i< tempWords.Count;i++)
            {
                if(!db.Progresses.Any(id=> id.UserId == uId && id.EnglishWordsId == tempWords[i].Id))
                {
                    tempEW.Add(tempWords[i]);
                }
            }
            if(tempEW.Count() > 5)
            {
                job = 5;
            }
            else
            {
                job = tempEW.Count();
            }
            while(job > 0)
            {
                
                
                int j = rnd.Next(0, tempEW.Count());
                //Console.WriteLine($"{count}, {j}");
                //var check = tempEW.Where(i => i.Id == j).FirstOrDefault();
                var check = tempEW[j];
                //checkWordInProgress = db.Progresses.Where(i=> i.UserId == uId).Any(w => w.EnglishWords == check);
                if (check != null)
                {
                    if (!eWord.Contains(check.EWord))
                    {
                        eWord.Add(check.EWord);
                        uWord.Add(db.UkrainianWords.Where(i => i.Id == db.EnglishWordsUkrainianWords
                        .Where(eI => eI.EnglishWordsId == check.Id).Select(uId => uId.UkrainianWordsId)
                        .SingleOrDefault()).Select(uW => uW.UWord).SingleOrDefault() ?? "");
                        job--;
                    }
                }
            }
            if(uWord.Count()< 3)
            {
                while (uWord.Count() < 3) 
                {
                    int j = rnd.Next(0, tempWords.Count());
                    uWord.Add(db.UkrainianWords.Where(k => k.Id == j).Select(u => u.UWord).SingleOrDefault());
                }
            }
            return (eWord, uWord);
        }
        public List<EnglishWords> GetEWords()
        {
            return db.EnglishWords.ToList();
        }
        public List<UkrainianWords> GetUWords()
        {
            return db.UkrainianWords.ToList();
        }
    }
}
