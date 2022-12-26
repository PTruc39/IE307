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
    public partial class EditUserPage : ContentPage
    {
        HttpClient client = new HttpClient();
        public EditUserPage(User user)
        {
            InitializeComponent();
            username.Text = user.userName;
            email.Text = user.email;
            password.Text = user.password;
        }
        public User User { get; set; }

        private async void save_Clicked(object sender, EventArgs e)
        {
            Host host = new Host();
            User user = new User();           
            user.userName = username.Text;
            user.email = email.Text;
            user.password = password.Text;
            var json = JsonConvert.SerializeObject(user);
            var noidung = new StringContent(json, Encoding.UTF8, "application/json");
            var apires = await client.PostAsync(host.url + "api/user/EditUser", noidung);
            string kq = await apires.Content.ReadAsStringAsync();
            await DisplayAlert("adsad", kq.ToString(), "sd", "no");

            await Navigation.PopAsync();
        }
    }
}