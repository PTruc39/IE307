using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class DetailMangaPage : ContentPage
    {
        Host host = new Host();
        HttpClient client = new HttpClient();
        //int checking;
        public List<Chapter> dslh;
        public List<Comment> cmt ;
        public List<Category> _Categorys;
        public int favouriteTapCount = 0;
        public List<Manga> Mangas { get; set; }
        public List<Category> categories
        {
            get { return _Categorys; }
            set { _Categorys = value; OnPropertyChanged(); }
        }
        HttpClient http = new HttpClient();
        public async void GetMangas()
        {
            var kq = await http.GetStringAsync
                (host.url + "api/manga/GetMangaList");
            Mangas = JsonConvert.DeserializeObject<List<Manga>>(kq);
        }
        public async void CheckFavorite(Manga mg)
        {
            var kq = await http.GetStringAsync
                (host.url + "api/checkfavorite?MangaID="+ mg.MangaID.ToString() +"&userID=" + User.userID.ToString());
            //await DisplayAlert("checking", kq.ToString(), "yes", "no");
            //checking = int.Parse(kq);
            if (int.Parse(kq) == 1)
                Heart.Source = "heartpink.png";
            else
                Heart.Source = "heartblack.png";

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
        async void GetComment(Manga manga)
        {
            var kq = await http.GetStringAsync
                (host.url + "api/comment/GetCommentByManga?MangaID=" + manga.MangaID.ToString());
            cmt = JsonConvert.DeserializeObject<List<Comment>>(kq);
            lstdslh2.ItemsSource = cmt;
        }
        async void GetCategoryByMangaID (int mangaID)
        {
            var kq = await http.GetStringAsync
               (host.url + "api/category/GetCategoryByMangaID?MangaID=" + mangaID.ToString());
            categories = JsonConvert.DeserializeObject<List<Category>>(kq);
        }
        public DetailMangaPage(Manga manga)
        {
            InitializeComponent();
            this.Manga = manga;
            this.BindingContext = this;
            GetChapterList(manga);
            CheckFavorite(manga);
            GetComment(manga);
            GetCategoryByMangaID(manga.MangaID);
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

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CommentPage(this.Manga,cmt,1));

        }
        private async void SelectType(object sender, EventArgs e)
        {
            var category = (sender as View).BindingContext as Category;
            Navigation.PushAsync(new CategoryPage(category.categoryID));


        }

        private void ChangeTextColor(View child, string hexColor)
        {
            var txtCtrl = child.FindByName<Label>("PropertyTypeName");

            if (txtCtrl != null)
                txtCtrl.TextColor = Color.FromHex(hexColor);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CommentPage(this.Manga, cmt));

        }
        private async void ImgAddToWishlist_Tapped(object sender, EventArgs e)
        {
            Image img = (Image)sender;
            Favorite favorite = new Favorite();
            favorite.mangaID = this.Manga.MangaID;
            favorite.userID = User.userID;
            if (img.Source.ToString() == "File: heartblack.png")
            {
                var json = JsonConvert.SerializeObject(favorite);
                var noidung = new StringContent(json, Encoding.UTF8, "application/json");
                var apires = await client.PostAsync(host.url + "api/user/AddFavorite", noidung);
                img.Source = "heartpink.png";
            }
            else
            {
                var json = JsonConvert.SerializeObject(favorite);
                var noidung = new StringContent(json, Encoding.UTF8, "application/json");
                var apires = await client.PostAsync(host.url + "api/user/DeleteFavorite", noidung);
                img.Source = "heartblack.png";

            }
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            Follow favorite = new Follow();
            favorite.mangaID = this.Manga.MangaID;
            favorite.userID = User.userID;
            var json = JsonConvert.SerializeObject(favorite);
            var noidung = new StringContent(json, Encoding.UTF8, "application/json");
            var apires = await http.PostAsync(host.url + "api/follow/AddFollow", noidung);
        }
        //private void ImgAddToWishlist_Tapped(object sender, EventArgs e)
        //{
        //    favouriteTapCount++;
        //    Image img = (Image)sender;
        //    img.Source = favouriteTapCount % 2 == 0 ? "heartblack.png" : "heartpink.png";
        //}
    }
}