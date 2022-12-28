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
    public partial class AddForum : ContentPage
    {
        Host host = new Host();
        HttpClient client = new HttpClient();
        public AddForum()
        {
            InitializeComponent();
        }

        private async void Upload_Clicked(object sender, EventArgs e)
        {
            Blogs blog = new Blogs();

            blog.UserID = User.userID;
            blog.BlogImg = BlogImg.Text;
            blog.BlogName = BlogName.Text;
            blog.BlogContent = BlogContent.Text;
            var json = JsonConvert.SerializeObject(blog);
            var noidung = new StringContent(json, Encoding.UTF8, "application/json");
            var apires = await client.PostAsync(host.url + "api/blog/AddBlog", noidung);
            
        }
    }
}