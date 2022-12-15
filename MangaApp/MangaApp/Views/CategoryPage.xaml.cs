using System;
using System.Collections.Generic;
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
    public partial class CategoryPage : ContentPage
    {
        HttpClient http = new HttpClient();
        Host host = new Host();
        async void GetManga()
        {
            var kq = await http.GetStringAsync
                (host.url + "api/manga/GetMangaList");
            var dslh = JsonConvert.DeserializeObject<List<Manga>>(kq);
            lstdslh.ItemsSource = dslh;
        }
        async void GetCategory ()
        {
            var kq = await http.GetStringAsync
                (host.url + "api/category/GetCategory");
            var dslh = JsonConvert.DeserializeObject<List<Category>>(kq);
            categoryPicker.ItemsSource = dslh;
        }
        public CategoryPage()
        {
            InitializeComponent();
            GetCategory();
            GetManga();

        }
        private async void categoryPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker)sender;
            Category selected = picker.SelectedItem as Category;
            var kq = await http.GetStringAsync
               (host.url + "api/manga/GetMangaByCategory?categoryID=" + selected.categoryID.ToString());
            var dslh = JsonConvert.DeserializeObject<List<Manga>>(kq);
            lstdslh.ItemsSource = dslh;

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
            var apires = await http.PostAsync(host.url + "api/user/AddFavorite", noidung);
        }
    }
}