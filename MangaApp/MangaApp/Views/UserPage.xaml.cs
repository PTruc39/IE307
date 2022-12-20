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
    public partial class UserPage : ContentPage
    {
        Host host = new Host();
        HttpClient http = new HttpClient();
        async void LayDSLoaiHoa()
        {
            var kq = await http.GetStringAsync
                (host.url + "api/userInfor/GetUserByID?userID=" + User.userID.ToString());
            var dslh = JsonConvert.DeserializeObject<List<User>>(kq);
            name.Text = dslh[0].userName;
            email.Text = dslh[0].email;
        }
        public UserPage()
        {
            InitializeComponent();
            LayDSLoaiHoa();
        }
    }
}