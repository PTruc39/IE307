using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MangaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResetPasswordPage : ContentPage
    {
        HttpClient client = new HttpClient();
        public int result;
        public ResetPasswordPage(List<User> user)
        {
            InitializeComponent();
        }

        public async void ResetPass(int userID, string curP, string newP)
        {
            Host host = new Host();
            var json = JsonConvert.SerializeObject("");
            var noidung = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage kq = await client.PostAsync
               (host.url + "api/user/ResetPassword?userID=" + userID.ToString() + "&currentPassword=" + curP + "&newPassword=" + newP,noidung);
            var kq1 = await kq.Content.ReadAsStringAsync();
            await DisplayAlert("result", kq1, "okk");
            result = int.Parse(kq1.ToString());
            await DisplayAlert("result", result.ToString(), "okk");

            if (result == 0)
            {
                await DisplayAlert("result", "Current password is not correct!", "okk");
            }
            else
            {
                await DisplayAlert("result", "Change password success!", "okk");
                await Navigation.PopAsync();
            }
        }

        private async void save_Clicked(object sender, EventArgs e)
        {
            if (curP.Text == "" || newP.Text == "")
            {
                await DisplayAlert("result", "Please fill out this form", "okk");
            }
            else
            {
                ResetPass(User.userID, curP.Text, newP.Text);
            }
        }
    }
}
