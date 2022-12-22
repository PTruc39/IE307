using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class DetailMangaPage : ContentPage
    {
        Host host = new Host();
        public List<Chapter> dslh;
        public int favouriteTapCount = 0;
        public ObservableCollection<Manga> Mangas { get; set; }
        HttpClient http = new HttpClient();
        public async void GetMangas()
        {
            var kq = await http.GetStringAsync
                (host.url + "api/manga/GetMangaList");
            Mangas = JsonConvert.DeserializeObject<ObservableCollection<Manga>>(kq);
        }
        public DetailMangaPage()
        {
            InitializeComponent();
        }
        async void GetChapterList(Manga manga)
        {
            var kq = await http.GetStringAsync
                (host.url+"api/chapter/GetChapterByManga?MangaID="+manga.MangaID.ToString());
            dslh = JsonConvert.DeserializeObject<List<Chapter>>(kq);
            lstdslh.ItemsSource = dslh;
        }
        public DetailMangaPage(Manga manga)
        {
            InitializeComponent();
            this.Manga = manga;
            this.BindingContext = this;
            GetChapterList(manga);
        }
        public Manga Manga { get; set; }
        private void lstdslh_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            Chapter chapter = (Chapter)lstdslh.SelectedItem;
            Navigation.PushAsync(new Reading(chapter,dslh));
        }
        private void GoBack(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        private void ImgAddToWishlist_Tapped(object sender, EventArgs e)
        {
            favouriteTapCount++;
            Image img = (Image)sender;
            img.Source = favouriteTapCount % 2 == 0 ? "FavouriteBlackIcon.png" : "FavouriteRedIcon.png";
        }
        private void ReadNow(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Reading(dslh[0],dslh));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DetailsView.TranslationY = 600;
            DetailsView.TranslateTo(0, 0, 500, Easing.SinInOut);
        }
    }
}