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
        Chapter chap = new Chapter();
        public List<Chapter> listchaps;
        int length;
        public Reading()
        {
            InitializeComponent();
        }
        async void GetChapterList(int ChapterID)
        {
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                (host.url + "api/chapter/GetListByChapter?ChapterID=" + ChapterID.ToString());
            var dslh = JsonConvert.DeserializeObject<List<ListImg>>(kq);
            lstdslh.ItemsSource = dslh;
            length = dslh.Count;
        }
        public Reading(Chapter chapter, List<Chapter> listchapter)
        {
            InitializeComponent();
            GetChapterList(chapter.ChapterID);
            listchaps = listchapter;
            chap = chapter;
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

        private void Button_Clicked_back(object sender, EventArgs e)
        {
            int index = listchaps.FindIndex(item => item.ChapterID == chap.ChapterID-1);
            if(index != -1)
            {
                GetChapterList(chap.ChapterID - 1);
                chap = listchaps[index];
            }
            else
            {
                DisplayAlert("no", "ko co chap de back nua", "no", "no");
            }
        }

        private void Button_Clicked_next(object sender, EventArgs e)
        {
            int index = listchaps.FindIndex(item => item.ChapterID == chap.ChapterID + 1);
            if (index != -1)
            {
                GetChapterList(chap.ChapterID + 1);
                chap = listchaps[index];
            }
            else
            {
                DisplayAlert("no", "ko co chap de next nua", "no", "no");
            }

        }
    }
}