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
    public partial class LoginPage2 : ContentPage
    {
        HttpClient client = new HttpClient();
        public LoginPage2()
        {
            InitializeComponent();
        }
        async private void Button_Clicked_1(object sender, EventArgs e)
        {
            Host host = new Host();
            User user = new User();
            user.email = email.Text;
            user.password = password.Text;
            var json = JsonConvert.SerializeObject(user);
            var noidung = new StringContent(json, Encoding.UTF8, "application/json");
            var apires = await client.PostAsync(host.url + "api/user/Login", noidung);
            string userID = await apires.Content.ReadAsStringAsync();
            User.userID = int.Parse(userID);
            if (User.userID != 0)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("OPPSIE", "Wrong password or email", "Yes");
            }
        }
    }
}