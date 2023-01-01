using System;
using System.Collections.Generic;
using System.Text;

namespace MangaApp.Models
{
    public class Comment
    {
        public int? MangaID { get; set; } = null;
        public int? BlogID { get; set; } = null;
        public int userID { get; set; } 
        public string userName { get; set; }
        public string comment { get; set; }
        public int commentID { get; set; }
    }
}
