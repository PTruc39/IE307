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
        public EditUserPage(List<User> user)
        {
            InitializeComponent();
            username.Text = user[0].userName;
            email.Text = user[0].email;
            User = user[0];
        }
        public User User { get; set; }

        private async void save_Clicked(object sender, EventArgs e)
        {
            Host host = new Host();
            usertry a = new usertry();
            a.userID = User.userID;
            a.userName = username.Text;
            a.email = email.Text;
            a.password = User.password;
            var json = JsonConvert.SerializeObject(a);
            var noidung = new StringContent(json, Encoding.UTF8, "application/json");
            var apires = await client.PostAsync(host.url + "api/user/EditUser", noidung);
            string kq = await apires.Content.ReadAsStringAsync();

            await Navigation.PopAsync();
        }

        public class usertry
        {
            public int userID { get; set; }
            public string userName { get; set; }
            public string email { get; set; }
            public string password { get; set; }
        }
    }
}