using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MangaApp.ViewModels;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MangaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        HttpClient client = new HttpClient();
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }

        async private void Button_Clicked_1(object sender, EventArgs e)
        {
            Host host = new Host();
            User user = new User();
            user.userName = username.Text;
            user.email = email.Text;
            user.password = password.Text;
            var json = JsonConvert.SerializeObject(user);
            var noidung = new StringContent(json,Encoding.UTF8,"application/json");
            var apires = await client.PostAsync(host.url+"api/user/AddUser", noidung);
            string userID = await apires.Content.ReadAsStringAsync();
            User.userID = int.Parse(userID);

            await DisplayAlert("adsad", User.userID.ToString(), "sd", "no");

            //await Application.Current.MainPage.Navigation.PopAsync();
            //Application.Current.MainPage = new AppShell();
            await Shell.Current.GoToAsync(state: "//AboutPage");

        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(state: "///LoginPage2");
        }
    }
}