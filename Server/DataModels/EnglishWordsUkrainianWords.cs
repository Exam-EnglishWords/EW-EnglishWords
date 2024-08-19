using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Server.DataModels
{
    internal class EnglishWordsUkrainianWords
    {
        [Key]
        public int Id { get; set; }
        public int EnglishWordsId { get; set; }
        public int UkrainianWordsId { get; set; }
        public EnglishWords EnglishWords { get; set; }
        public UkrainianWords UkrainianWords { get; set; }
    }
}
