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
    public partial class DetailMangaPage : ContentPage
    {
        Host host = new Host();
        public DetailMangaPage()
        {
            InitializeComponent();
        }
        async void GetChapterList(Manga manga)
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                (host.url+"api/chapter/GetChapterByManga?MangaID="+manga.MangaID.ToString());
            var dslh = JsonConvert.DeserializeObject<List<Chapter>>(kq);
            lstdslh.ItemsSource = dslh;
        }
        public DetailMangaPage(Manga manga)
        {
            InitializeComponent();
            MangaImage.Source = manga.MangaImage;
            MangaName.Text = manga.MangaName;
            Description.Text = manga.Description;
            GetChapterList(manga);
        }
        private void lstdslh_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            Chapter chapter = (Chapter)lstdslh.SelectedItem;
            Navigation.PushAsync(new Reading(chapter));
        }
    }
}