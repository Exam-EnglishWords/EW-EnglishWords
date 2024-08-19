using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Server.DataModels
{
    internal class UkrainianWords
    {
        [Key]
        public int Id { get; set; }
        public string UWord { get; set; }
        public List<EnglishWordsUkrainianWords> EnglishWordsUkrainianWords { get; set; }
    }
}
