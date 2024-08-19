using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RqRsModel
{
    [Serializable]
    public class Request
    {
        public string Title { get; set; }
        public string RqEWord { get; set; }
        public string RqUWord { get; set; }
        public int RqId { get; set; }
        public string RqLogin { get; set; }
        public string RqPassword { get; set; }
        public List<string> RqEWords { get; set; } = new List<string>();
    }
}
