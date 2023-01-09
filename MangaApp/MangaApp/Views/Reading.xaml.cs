using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;

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
        int Status;
        public List<Chapter> listchaps;
        public ObservableCollection<ListImg> _imgs;
        public ObservableCollection<ListImg> images
        {
            get { return _imgs; }
            set { _imgs = value; OnPropertyChanged(); }
        }
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
            var dslh = JsonConvert.DeserializeObject<ObservableCollection<ListImg>>(kq);
            images = dslh;
            lstdslh.ItemsSource = dslh;

        }
        public Reading(Chapter chapter, List<Chapter> listchapter)
        {
            InitializeComponent();
            GetChapterList(chapter.ChapterID);
            this.BindingContext = this;
            listchaps = listchapter;
            chap = chapter;
            categoryPicker.ItemsSource = listchapter;
            categoryPicker.SelectedIndex = listchapter.FindIndex(item => item.ChapterID == chapter.ChapterID);
            Status = categoryPicker.SelectedIndex;
            length = listchapter.Count;
            
        }

        private void lstdslh_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            
        }

        //private void lstdslh_PositionChanged(object sender, PositionChangedEventArgs e)
        //{
        //    try
        //    {
        //        int curr = e.CurrentPosition;
        //        currentItem.Text = $"Position:{curr}";
        //    }
        //    catch { }
        //}

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    int page = 0;
        //    if (pagenum.Text != null && pagenum.Text != "")
        //        page = Int32.Parse(pagenum.Text);

        //    lstdslh.ScrollTo(page, position: ScrollToPosition.MakeVisible, animate: true);
        //}

        private void Button_Clicked_back(object sender, EventArgs e)
        {
            Status = listchaps.FindIndex(item => item.ChapterID == chap.ChapterID) - 1;
            if(Status != -1)
            {
                //GetChapterList(chap.ChapterID - 1);
                categoryPicker.SelectedIndex = Status;
                chap = listchaps[Status];
            }
            else
            {
                DisplayAlert("Announcement", "You are reading the first chapter", "OK");
            }
        }

        private void Button_Clicked_next(object sender, EventArgs e)
        {
            Status = listchaps.FindIndex(item => item.ChapterID == chap.ChapterID) + 1;
            if (Status < length)
            {
                //GetChapterList(chap.ChapterID - 1);
                categoryPicker.SelectedIndex = Status;
                chap = listchaps[Status];
            }
            else
            {
                DisplayAlert("Announcement", "You are reading the last chapter", "OK");
            }

        }

        private void categoryPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

            Picker picker = (Picker)sender;
            Chapter selected = picker.SelectedItem as Chapter;
            GetChapterList(selected.ChapterID);
            //listimg.ScrollToAsync(0, 0, true);
            chap = selected;
        }
    }
}