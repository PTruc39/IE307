using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace MangaApp
{
    public class User
    {
        public static int userID = Preferences.Get("userID", 0);
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
