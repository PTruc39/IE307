using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MangaApp.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MangaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommentPage : ContentPage
    {
        Host host = new Host();
        HttpClient client = new HttpClient();
        public CommentPage(Manga manga)
        {
            InitializeComponent();
            testing.Text = manga.MangaName;
            this.Manga=manga;
        }

        public Manga Manga { get; }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Comment cmt = new Comment();
           
            cmt.MangaID = this.Manga.MangaID;
            cmt.userID = User.userID;
            cmt.comment = Name.Text;
            var json = JsonConvert.SerializeObject(cmt);
            var noidung = new StringContent(json, Encoding.UTF8, "application/json");
            var apires = await client.PostAsync(host.url + "api/user/AddComment", noidung);
        }
    }
}