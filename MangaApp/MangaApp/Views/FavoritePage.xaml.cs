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

        private void Button_Clicked(object sender, EventArgs e)
        {
           
            DisplayAlert("aaa", User.userID.ToString(), "Aa", "aa");
        }
    }
}