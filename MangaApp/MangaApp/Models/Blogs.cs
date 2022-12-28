using System;
using System.Collections.Generic;
using System.Text;

namespace MangaApp.Models
{
    public class Blogs
    {
        public int BlogID { get; set; }
        public string BlogName { get; set; }
        public string BlogImg { get; set; }
        public string BlogContent { get; set; }
        public int UserID { get; set; }
        public string userName { get; set; }
    }
}
