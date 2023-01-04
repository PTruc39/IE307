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
        public List<Comment> cmts ;
        public ObservableCollection<Manga> Mangas { get; set; }
        Host host = new Host();
        HttpClient client = new HttpClient();
        public CommentPage(Manga manga, List<Comment> cmt)
        {
            InitializeComponent();
            cmts =cmt;
            lstdslh.ItemsSource = cmt;
            this.Manga=manga;
        }
        public CommentPage(Manga manga, List<Comment> cmt,int i)
        {
            InitializeComponent();
            cmts = cmt;
            lstdslh.ItemsSource = cmt;
            this.Manga = manga;
            Name.Focus();
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
            GetComment(this.Manga);
            
        }
        async void GetComment(Manga manga)
        {
            var kq = await client.GetStringAsync
                (host.url + "api/comment/GetCommentByManga?MangaID=" + manga.MangaID.ToString());
            cmts = JsonConvert.DeserializeObject<List<Comment>>(kq);
            lstdslh.ItemsSource = cmts;
            Name.Text = "";
        }
        private void lstdslh_Refreshing(object sender, EventArgs e)
        {
            lstdslh.ItemsSource = null;
            try
            { GetComment(this.Manga); }
            finally { lstdslh.EndRefresh(); }
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            Comment binhluan = menuItem.CommandParameter as Comment;
            if (User.userID == binhluan.userID)
            {
                Comment cmt = new Comment();
                cmt.commentID = binhluan.commentID;
                var json = JsonConvert.SerializeObject(cmt);
                var noidung = new StringContent(json, Encoding.UTF8, "application/json");
                var apires = await client.PostAsync(host.url + "api/comment/DeleteComment", noidung);
                GetComment(this.Manga);
            }
            else
                DisplayAlert("KHONG duoc xoa", "KHONG", "KHONG", "KHONGGG");

        }
    }
}