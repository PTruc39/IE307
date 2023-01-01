using System;
using System.Collections.Generic;
using System.Text;

namespace MangaApp.Models
{
    public class Notify
    {
        public int userID { get; set; }
        public int mangaID { get; set; }
        public string notify { get; set; }
        public object MangaName { get; set; }
        public object MangaImage { get; set; }
        public object Description { get; set; }
    }
}
