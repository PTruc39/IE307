using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MangaApp.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MangaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailForum : ContentPage
    {
        public List<Comment> cmt;
        HttpClient http = new HttpClient();
        Host host = new Host();
        public Blogs blogs { get; set; }
        async void GetComment(Blogs blog)
        {
            var kq = await http.GetStringAsync
                (host.url + "api/comment/GetCommentByManga?MangaID=&BlogID=" + blog.BlogID.ToString());
            cmt = JsonConvert.DeserializeObject<List<Comment>>(kq);
            lstdslh.ItemsSource = cmt;
        }
        public DetailForum(Blogs blog)
        {
            InitializeComponent();
            BlogName.Text = blog.BlogName;
            BlogImg.Source = blog.BlogImg;
            BlogContent.Text = blog.BlogContent;
            user.Text = blog.userName;
            GetComment(blog);
            blogs = blog;

        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {

        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            Host host = new Host();
            Comment cmt = new Comment();
            cmt.userID = User.userID;
            cmt.BlogID = blogs.BlogID;
            cmt.comment = yourCmt.Text;
            var json = JsonConvert.SerializeObject(cmt);
            var noidung = new StringContent(json, Encoding.UTF8, "application/json");
            var apires = await http.PostAsync(host.url + "api/user/AddComment", noidung);
            string kq = await apires.Content.ReadAsStringAsync();
            await DisplayAlert("thong bao","Kq binh luan la: " + kq,"OK");
            yourCmt.Text = "";
            GetComment(blogs);
        }
    }
}