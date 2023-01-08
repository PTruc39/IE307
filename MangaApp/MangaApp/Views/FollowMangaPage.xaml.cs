using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MangaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FollowMangaPage : ContentPage
    {
        Host host = new Host();
        HttpClient http = new HttpClient();
        HttpClient client = new HttpClient();
        public List<Manga> _mangas;
        public List<Category> _Categorys;
        public List<Category> categories
        {
            get { return _Categorys; }
            set { _Categorys = value; OnPropertyChanged(); }
        }
        public async void LayDSLoaiHoa()
        {
            var kq = await http.GetStringAsync
                (host.url + "api/follow/GetFollowByUser?userID=" + User.userID.ToString());
            var result = JsonConvert.DeserializeObject<List<Manga>>(kq);
            lstFavourites.ItemsSource = result;
        }
        public FollowMangaPage()
        {
            InitializeComponent();
            LayDSLoaiHoa();
        }
        private async void OnDeleteSwipeItemInvoked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Warning", "Do you really want to delete this comic?", "Yes", "No");
            if (answer)
            {
                SwipeItem swipeItem = (SwipeItem)sender;
                Manga manga = swipeItem.CommandParameter as Manga;
                Favorite favorite = new Favorite();
                favorite.mangaID = manga.MangaID;
                favorite.userID = User.userID;
                var json = JsonConvert.SerializeObject(favorite);
                var noidung = new StringContent(json, Encoding.UTF8, "application/json");
                var apires = await client.PostAsync(host.url + "api/user/DeleteFollow", noidung);
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {

            DisplayAlert("aaa", User.userID.ToString(), "Aa", "aa");
        }

        private void lstFavourites_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Manga manga = (Manga)lstFavourites.SelectedItem;
            Navigation.PushAsync(new DetailMangaPage(manga));
        }

        private void refreshCV_Refreshing(object sender, EventArgs e)
        {
            lstFavourites.ItemsSource = null;
            LayDSLoaiHoa();
            refreshCV.IsRefreshing = false;
        }

        private async void lstFavourites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var manga = e.CurrentSelection.FirstOrDefault() as Manga;
            await Navigation.PushAsync(new DetailMangaPage(manga));
        }
    }
}