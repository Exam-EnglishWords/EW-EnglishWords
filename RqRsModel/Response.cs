using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RqRsModel
{
    [Serializable]
    public class Response
    {
        public bool Chek;
        public char CheckLogOrPass;
        public List<string> RqEWords { get; set; } = new List<string>();
        public List<string> RqUWords { get; set; } = new List<string>();
    }
}
