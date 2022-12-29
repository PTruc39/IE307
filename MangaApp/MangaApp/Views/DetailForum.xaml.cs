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
        async void GetComment(Blogs blog)
        {
            var kq = await http.GetStringAsync
                (host.url + "api/comment/GetCommentByManga?BlogID=" + blog.BlogID.ToString());
            cmt = JsonConvert.DeserializeObject<List<Comment>>(kq);
            lstdslh.ItemsSource = cmt;
        }
        public DetailForum(Blogs blog)
        {
            InitializeComponent();
            BlogName.Text = blog.BlogName;
            BlogImg.Source = blog.BlogImg;
            BlogContent.Text = blog.BlogContent;
            User.Text = blog.userName;
            GetComment(blog);
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}