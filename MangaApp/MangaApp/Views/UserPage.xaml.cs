using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace MangaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class UserPage : ContentPage
    {
        Host host = new Host();
        HttpClient http = new HttpClient();

        public List<User> userinfor;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            getUser();
        }
        public async void getUser()
        {
            HttpClient http = new HttpClient();

            var kq = await http.GetStringAsync
                (host.url + "api/userInfor/GetUserByID?userID=" + User.userID.ToString());
            userinfor = JsonConvert.DeserializeObject<List<User>>(kq);
            username.Text = userinfor[0].userName;
            email.Text = userinfor[0].email;
            password.Text = userinfor[0].password;
        }
        public UserPage()
        {
            InitializeComponent();
            getUser();
        }
        
        private void Logout_Clicked(object sender, EventArgs e)
        {
            Preferences.Clear();
            Shell.Current.GoToAsync(state: "///LoginPage2");
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditUserPage(userinfor));
        }

        private bool isOpen = false;
        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            if (isOpen == false)
            {
                overlay.IsVisible = true;
                isOpen = true;
                //Scale to smaller
                await ((Frame)sender).ScaleTo(0.8, 50, Easing.Linear);
                //Wait a moment
                await Task.Delay(50);
                //Scale to normal
                await ((Frame)sender).ScaleTo(1, 50, Easing.Linear);

                //Show FloatMenuItem1
                FloatMenuItem1.IsVisible = true;
                await FloatMenuItem1.TranslateTo(0, 0, 50);
                await FloatMenuItem1.TranslateTo(0, -20, 50);
                await FloatMenuItem1.TranslateTo(0, 0, 50);

                //Show FloatMenuItem2
                FloatMenuItem2.IsVisible = true;
                await FloatMenuItem2.TranslateTo(0, 0, 50);
                await FloatMenuItem2.TranslateTo(0, -20, 50);
                await FloatMenuItem2.TranslateTo(0, 0, 50);

                //Show FloatMenuItem3
                FloatMenuItem3.IsVisible = true;
                await FloatMenuItem3.TranslateTo(0, 0, 100);
                await FloatMenuItem3.TranslateTo(0, -20, 100);
                await FloatMenuItem3.TranslateTo(0, 0, 200);
            }
            else
            {
                isOpen = false;
                //Scale to smaller
                await ((Frame)sender).ScaleTo(0.8, 50, Easing.Linear);
                //Wait a moment
                await Task.Delay(50);
                //Scale to normal
                await ((Frame)sender).ScaleTo(1, 50, Easing.Linear);

                //Hide FloatMenuItem1
                await FloatMenuItem1.TranslateTo(0, 0, 100);
                await FloatMenuItem1.TranslateTo(0, -5, 100);
                await FloatMenuItem1.TranslateTo(0, 0, 200);
                FloatMenuItem1.IsVisible = false;

                //Hide FloatMenuItem2
                await FloatMenuItem2.TranslateTo(0, 0, 100);
                await FloatMenuItem2.TranslateTo(0, -5, 100);
                await FloatMenuItem2.TranslateTo(0, 0, 200);
                FloatMenuItem2.IsVisible = false;

                //Hide FloatMenuItem3
                await FloatMenuItem3.TranslateTo(0, 0, 100);
                await FloatMenuItem3.TranslateTo(0, -5, 100);
                await FloatMenuItem3.TranslateTo(0, 0, 200);
                FloatMenuItem3.IsVisible = false;

                overlay.IsVisible = false;

            }

        }

        private async void FloatMenuItem1Tap_OnTapped(object sender, EventArgs e)
        {
            LabelStatus.Text = "Menu 1";
            await Navigation.PushAsync(new EditUserPage(userinfor));
        }

        private async void FloatMenuItem2Tap_OnTapped(object sender, EventArgs e)
        {
            LabelStatus.Text = "Menu 3";
            await Navigation.PushAsync(new ResetPasswordPage(userinfor));
        }

        private void FloatMenuItem3Tap_OnTapped(object sender, EventArgs e)
        {
            LabelStatus.Text = "Menu 3";
        }
    }
}
