using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MangaApp.Views
{
    
    public partial class AboutPage : ContentPage
    {
        Host host = new Host();
        HttpClient client = new HttpClient();
        async void LayDSLoaiHoa()
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                (host.url+"api/manga/GetMangaList");
            var dslh = JsonConvert.DeserializeObject<List<Manga>>(kq);
            lstdslh.ItemsSource = dslh;
        }
        public AboutPage()
        {
            InitializeComponent();
            LayDSLoaiHoa();
        }

         private void lstdslh_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
          
            Manga manga = (Manga)lstdslh.SelectedItem;
            Navigation.PushAsync(new DetailMangaPage(manga));
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            Manga manga= menuItem.CommandParameter as Manga;
            Favorite favorite = new Favorite();
            favorite.mangaID = manga.MangaID;
            favorite.userID = User.userID;
            var json = JsonConvert.SerializeObject(favorite);
            var noidung = new StringContent(json, Encoding.UTF8, "application/json");
            var apires = await client.PostAsync(host.url+"api/user/AddFavorite", noidung);
        }
        /*private void Button_Clicked(object sender, EventArgs e)
        {
            testglobal.GlobalVariable = 12;
        }*/


        /*async private void lstdslh_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            HttpClient client = new HttpClient();
            Favorite addf = new Favorite();
            Manga lh = (Manga)lstdslh.SelectedItem;
            addf.mangaID = lh.MangaID;
            addf.userID = 7;
            var json = JsonConvert.SerializeObject(addf);
            var noidung = new StringContent(json, Encoding.UTF8, "application/json");
            var apires = await client.PostAsync("http://172.30.91.30/MangaApi/api/user/AddFavorite", noidung);
            string userID = await apires.Content.ReadAsStringAsync();
            int id = int.Parse(userID);

            await DisplayAlert("adsad", id.ToString(), "sd", "no");


        }*/

    }
}