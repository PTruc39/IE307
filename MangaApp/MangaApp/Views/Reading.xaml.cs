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
        int length;
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
            length = dslh.Count;
        }
        public Reading(Chapter chapter)
        {
            InitializeComponent();
            GetChapterList(chapter);
        }

        private void lstdslh_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            
        }

        private void lstdslh_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            try
            {
                int curr = e.CurrentPosition;
                currentItem.Text = $"Position:{curr}";
            }
            catch { }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            int page = 0;
            if (pagenum.Text != null && pagenum.Text != "")
                page = Int32.Parse(pagenum.Text);

            lstdslh.ScrollTo(page, position: ScrollToPosition.MakeVisible, animate: true);
        }
    }
}