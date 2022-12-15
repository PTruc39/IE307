using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Reading : ContentPage
    {
        Host host = new Host();
        public Reading()
        {
            InitializeComponent();
        }
        async void GetChapterList(Chapter chapter)
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                (host.url + "api/chapter/GetListByChapter?ChapterID=" + chapter.ChapterID.ToString());
            var dslh = JsonConvert.DeserializeObject<List<ListImg>>(kq);
            lstdslh.ItemsSource = dslh;
        }
        public Reading(Chapter chapter)
        {
            InitializeComponent();
            GetChapterList(chapter);
        }
    }
}