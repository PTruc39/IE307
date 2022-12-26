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

namespace MangaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class UserPage : ContentPage
    {
        Host host = new Host();
        HttpClient http = new HttpClient();

        public List<User> userinfor;

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        //}
        //public ObservableCollection<User> userinfor
        //{
        //    get { return _userinfor; }
        //    set { _userinfor = value; OnPropertyChanged(); }
        //}
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
            //this.InforUser = lstInforUser[0];
            getUser();
            //DisplayAlert("thong bao","user Name: " + user[0].userName.ToString(),"OK", "no");
        }
        //public User InforUser { get; set; }
        
        private void Logout_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(state: "///LoginPage2");
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditUserPage(userinfor[0]));
        }
    }
}
