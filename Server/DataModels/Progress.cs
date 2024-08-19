using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Server.DataModels
{
    internal class Progress
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EnglishWordsId { get; set; }
        public User User { get; set; }
        public EnglishWords EnglishWords { get; set; }
    }
}
