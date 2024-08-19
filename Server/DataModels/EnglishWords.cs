using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Server.DataModels
{
    internal class EnglishWords
    {
        [Key]
        public int Id { get; set; } 
        public string EWord { get; set; }
        public List<EnglishWordsUkrainianWords> EnglishWordsUkrainianWords { get; set; }
        public List<Progress> Progress { get; set; }
    }
}
