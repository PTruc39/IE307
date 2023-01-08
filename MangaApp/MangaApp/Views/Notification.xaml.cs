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
    public partial class Notification : ContentPage
    {
        Host host = new Host();
        HttpClient client = new HttpClient();
        async void GetComment()
        {
            var kq = await client.GetStringAsync
                (host.url + "api/user/GetNotifyByUser?userID=" + User.userID.ToString());
            List<Notify> cmts = JsonConvert.DeserializeObject<List<Notify>>(kq);
            lstdslh.ItemsSource = cmts;
            if(cmts.Count==0)
            {
                emptyntf.IsVisible = true;
            }
            else
            {
                emptyntf.IsVisible = false;
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetComment();
          
        }
        public Notification()
        {
            InitializeComponent();
            GetComment();
        }

        private async void lstdslh_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Notify ntf = (Notify)lstdslh.SelectedItem;
            Favorite favorite = new Favorite();
            favorite.mangaID = ntf.mangaID;
            favorite.userID = User.userID;
            var json = JsonConvert.SerializeObject(favorite);
            var noidung = new StringContent(json, Encoding.UTF8, "application/json");
            var apires = await client.PostAsync(host.url + "api/user/DeleteNotify", noidung);
            Manga manga = new Manga();
            manga.MangaID = ntf.mangaID;
            manga.MangaImage = ntf.MangaImage.ToString();
            manga.MangaName = ntf.MangaName.ToString();
            manga.Description = ntf.Description.ToString();
            await Navigation.PushAsync(new DetailMangaPage(manga));
        }
        private void lstdslh_Refreshing(object sender, EventArgs e)
        {
            lstdslh.ItemsSource = null;
            try
            { GetComment(); }
            finally { lstdslh.EndRefresh(); }

        }
    }
}