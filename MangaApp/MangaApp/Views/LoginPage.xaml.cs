using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MangaApp.ViewModels;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace MangaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : INotifyPropertyChanged
    {
        HttpClient client = new HttpClient();
        public Color validateColor = Color.Transparent;
        public Color ValidateColor
        {
            get
            {
                return validateColor;
            }
            set
            {
                validateColor = value;
                OnPropertyChanged();
            }
        }
        public Color validateColor2 = Color.Transparent;
        public Color ValidateColor2
        {
            get
            {
                return validateColor2;
            }
            set
            {
                validateColor2 = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
            this.BindingContext = this;

        }

        async private void Button_Clicked_1(object sender, EventArgs e)
        {
            if (isEmpty(email.Text, password.Text, username.Text))
            {
                if (isValidEmail(email.Text) && isPass(password.Text))
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
            Preferences.Set("userID", int.Parse(userID));
                    if (int.Parse(userID) != 0)
                    {
                        await DisplayAlert("User id cua ban", Preferences.Get("userID", 0).ToString(), "ok");

                        Preferences.Set("userID", int.Parse(userID));

                        await Shell.Current.GoToAsync(state: "//AboutPage");
                    }
                    else
                    {
                        await DisplayAlert("Register Failed!", "Your email/password is invalid.", "yes");

                    }
                }
                else
                {
                    await DisplayAlert("Register Failed!", "Your email/password is invalid.", "yes");

                }
            }
            else
               await DisplayAlert("Register Failed!", "Please fill all your inputs.", "yes");

        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(state: "///LoginPage2");
        }
        public static bool isValidEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }
        public static bool isPass(string inputEmail)
        {

            if (inputEmail.Length < 8)
                return (false);
            else
                return (true);
        }
        public static bool isEmpty(string inputEmail = null, string inputPass = null, string inputName = null)
        {

            if (inputEmail is null || inputPass is null || inputName is null)
                return (false);
            else
                return (true);
        }

        private void password_Completed(object sender, EventArgs e)
        {
            if (!(password.Text is null))
            {
                if (isPass(password.Text))
                {
                    ValidateColor = Color.Transparent;
                    invalipass.IsVisible = false;
                }
                else
                {
                    ValidateColor = Color.Red;
                    invalipass.IsVisible = true;
                }
            }
        }

        private void email_Completed(object sender, EventArgs e)
        {
            if (!(email.Text is null))
            {
                if (isValidEmail(email.Text))
                {
                    ValidateColor2 = Color.Transparent;
                    invalidemail.IsVisible = false;
                }
                else
                {
                    ValidateColor2 = Color.Red;
                    invalidemail.IsVisible = true;
                }
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            email.Text = "";
            username.Text = "";
            password.Text = "";
        }
    }
}