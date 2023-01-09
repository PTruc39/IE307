using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using MangaApp.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MangaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumPage : ContentPage
    {

        HttpClient http = new HttpClient();
        Host host = new Host();
        async void GetBlogs()
        {
            var kq = await http.GetStringAsync
                (host.url + "api/blog/GetBlogList");
            var dslh = JsonConvert.DeserializeObject<List<Blogs>>(kq);
            lstdslh.ItemsSource = dslh;
        }
        public ForumPage()
        {
            InitializeComponent();
            GetBlogs();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetBlogs();
        }

        private void lstdslh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Blogs blog = (Blogs)lstdslh.SelectedItem;
            Navigation.PushAsync(new DetailForum(blog));
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddForum());
        }
    }
}