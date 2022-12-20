using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MangaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritePage : ContentPage
    {
        Host host = new Host();
        HttpClient http = new HttpClient();
        HttpClient client = new HttpClient();
        async void LayDSLoaiHoa()
        {
            var kq = await http.GetStringAsync
                (host.url + "api/favorite/GetFavoriteByUser?userID="+User.userID.ToString());
            var dslh = JsonConvert.DeserializeObject<List<Manga>>(kq);
            lstdslh.ItemsSource = dslh;
        }
        public FavoritePage()
        {
            InitializeComponent();
            LayDSLoaiHoa();
        }
        
        private void lsthotel_Refreshing(object sender, EventArgs e)
        {
            lstdslh.ItemsSource = null;
            try
            { LayDSLoaiHoa(); }
            finally { lstdslh.EndRefresh(); }
            
        }
        private void lstdslh_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            Manga manga = (Manga)lstdslh.SelectedItem;
            Navigation.PushAsync(new DetailMangaPage(manga));
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            Manga manga = menuItem.CommandParameter as Manga;
            Favorite favorite = new Favorite();
            favorite.mangaID = manga.MangaID;
            favorite.userID = User.userID;
            var json = JsonConvert.SerializeObject(favorite);
            var noidung = new StringContent(json, Encoding.UTF8, "application/json");
            var apires = await client.PostAsync(host.url + "api/user/DeleteFavorite", noidung);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
           
            DisplayAlert("aaa", User.userID.ToString(), "Aa", "aa");
        }
    }
}