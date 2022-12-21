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

namespace MangaApp.Views
{
    
    public partial class AboutPage : ContentPage
    {
        Host host = new Host();
        HttpClient client = new HttpClient();
        public List<Manga> dslh;
        public ObservableCollection<Manga> Mangas { get; set; }

        public async void LayDSLoaiHoa()
        {
            List<Manga> dslh = null;
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                (host.url+"api/manga/GetMangaList");
            Mangas = JsonConvert.DeserializeObject<ObservableCollection<Manga>>(kq);
            /*foreach (var item in dslh)
            {
                Mangas.Add(new Manga() { MangaName = item.MangaName, MangaID = item.MangaID, MangaImage = item.MangaImage, Description = item.Description });
            }*/
            //lstdslh.ItemsSource = Mangas;
            //lstdslh2.ItemsSource = dslh;
            //this.BindingContext = this;
        }
        public AboutPage()
        {
            InitializeComponent();
            LayDSLoaiHoa();
            this.BindingContext = this;
        }

         private void lstdslh_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
          
            //Manga manga = (Manga)lstdslh.SelectedItem;
            //Navigation.PushAsync(new DetailMangaPage(manga));
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








        public List<PropertyType> PropertyTypeList => GetPropertyTypes();
        public List<Property> PropertyList => GetProperties();

        private List<PropertyType> GetPropertyTypes()
        {
            return new List<PropertyType>
            {
                new PropertyType { TypeName = "All" },
                new PropertyType { TypeName = "Studio" },
                new PropertyType { TypeName = "4 Bed" },
                new PropertyType { TypeName = "3 Bed" },
                new PropertyType { TypeName = "Office" }
            };
        }

        private List<Property> GetProperties()
        {
            return new List<Property>
            {
                new Property { Image = "apt1.png", Address = "2162 Patricia Ave, LA", Location = "Califonia", Price = "$1500/mo", Bed = "4 Bed", Bath = "3 Bath", Space = "1600 sqft", Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Bibendum est ultricies integer quis. Iaculis urna id volutpat lacus laoreet. Mauris vitae ultricies leo integer malesuada. Ac odio tempor orci dapibus ultrices in. Egestas diam in arcu cursus euismod. Dictum fusce ut" },
                new Property { Image = "apt2.png", Address = "2168 Cushions Dr, LA", Location = "Califonia", Price = "$1000/mo", Bed = "3 Bed", Bath = "1 Bath", Space = "1100 sqft", Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Bibendum est ultricies integer quis. Iaculis urna id volutpat lacus laoreet. Mauris vitae ultricies leo integer malesuada. Ac odio tempor orci dapibus ultrices in. Egestas diam in arcu cursus euismod. Dictum fusce ut" },
                new Property { Image = "apt3.png", Address = "2112 Anthony Way, LA", Location = "Califonia", Price = "$900/mo", Bed = "2 Bed", Bath = "2 Bath", Space = "1200 sqft", Details = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Bibendum est ultricies integer quis. Iaculis urna id volutpat lacus laoreet. Mauris vitae ultricies leo integer malesuada. Ac odio tempor orci dapibus ultrices in. Egestas diam in arcu cursus euismod. Dictum fusce ut" },
            };
        }

        private async void PropertySelected(object sender, EventArgs e)
        {
            var manga = (sender as View).BindingContext as Manga;
            await this.Navigation.PushAsync(new DetailMangaPage(manga));


        }

        private void SelectType(object sender, EventArgs e)
        {
            var view = sender as View;
            var parent = view.Parent as StackLayout;

            foreach (var child in parent.Children)
            {
                VisualStateManager.GoToState(child, "Normal");
                ChangeTextColor(child, "#707070");
            }

            VisualStateManager.GoToState(view, "Selected");
            ChangeTextColor(view, "#FFFFFF");
        }

        private void ChangeTextColor(View child, string hexColor)
        {
            var txtCtrl = child.FindByName<Label>("PropertyTypeName");

            if (txtCtrl != null)
                txtCtrl.TextColor = Color.FromHex(hexColor);
        }
    }

    public class PropertyType
    {
        public string TypeName { get; set; }
    }

    public class Property
    {
        public string Id => Guid.NewGuid().ToString("N");
        public string PropertyName { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Price { get; set; }
        public string Bed { get; set; }
        public string Bath { get; set; }
        public string Space { get; set; }
        public string Details { get; set; }
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
