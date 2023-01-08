using System;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MangaApp.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MangaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForumPage : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        int i = 1;
        HttpClient http = new HttpClient();
        public List<Blogs> dslh;
        public ObservableCollection<Category> _Categorys;

        public ObservableCollection<Category> filter
        {
            get { return _Categorys; }
            set { _Categorys = value; OnPropertyChanged(); }
        }
        //List<Category> MyList2 = new List<Category>() {
        //    new Category
        //    {   categoryID = 0,
        //        categoryName = "Newest",
        //    },
        //    new Category
        //    {   categoryID = 0,
        //        categoryName = "Newest",
        //    },
        //    new Category
        //    {   categoryID = 0,
        //        categoryName = "Newest",
        //    }};
        Host host = new Host();
        async void GetBlogs()
        {
            var kq = await http.GetStringAsync
                (host.url + "api/blog/GetBlogList");
            dslh = JsonConvert.DeserializeObject<List<Blogs>>(kq);
            lstdslh.ItemsSource = Enumerable.Reverse(dslh);
        }
        public ForumPage()
        {
           
            InitializeComponent();
            GetBlogs();
            this.BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            GetBlogs();
            await Task.Delay(200);


        }

        private void lstdslh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Blogs blog = (Blogs)lstdslh.SelectedItem;
            Navigation.PushAsync(new DetailForum(blog));
        }
        

            private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddForum());
        }

        private void srch_Completed(object sender, EventArgs e)
        {
            if (srch.Text != "" || !(srch.Text is null))
            {
                if (i == 1)
                    lstdslh.ItemsSource = Enumerable.Reverse(dslh.Where(hotel => hotel.BlogName.ToLower().Contains(srch.Text)));
                else if (i == 2)
                    lstdslh.ItemsSource = dslh.Where(hotel => hotel.BlogName.ToLower().Contains(srch.Text));
                else if (i == 3)
                    lstdslh.ItemsSource = dslh.Where(hotel => hotel.UserID == User.userID && hotel.BlogName.ToLower().Contains(srch.Text));
            }
            else
            {
                if (i == 1)
                    lstdslh.ItemsSource = Enumerable.Reverse(dslh);
                else if (i == 2)
                    lstdslh.ItemsSource = dslh;
                else if (i == 3)
                    lstdslh.ItemsSource = dslh.Where(hotel => hotel.UserID == User.userID);
            }

        }
        private void btnRecent_Clicked(object sender, EventArgs e)
        {
            i = 1;
            ChangeButtonAppearance((Button)sender);
            if(srch.Text==""||srch.Text is null)
            lstdslh.ItemsSource = Enumerable.Reverse(dslh);
            else
            {
                lstdslh.ItemsSource = Enumerable.Reverse(dslh.Where(hotel => hotel.BlogName.ToLower().Contains(srch.Text)));
            }
        }

        private void btnToday_Clicked(object sender, EventArgs e)
        {
            i = 2;
            ChangeButtonAppearance((Button)sender);
            if (srch.Text == "" || srch.Text is null)
                lstdslh.ItemsSource = dslh;
            else
            {
                lstdslh.ItemsSource = dslh.Where(hotel => hotel.BlogName.ToLower().Contains(srch.Text));
            }

        }
        private void btnUpcoming_Clicked(object sender, EventArgs e)
        {
            i = 3;
            ChangeButtonAppearance((Button)sender);
            if (srch.Text == "" || srch.Text is null)
                lstdslh.ItemsSource = dslh.Where(hotel => hotel.UserID==User.userID);
            else
                lstdslh.ItemsSource = dslh.Where(hotel => hotel.UserID == User.userID && hotel.BlogName.ToLower().Contains(srch.Text));

        }

        private void ChangeButtonAppearance(Button btn)
        {
            if (btn.Text == "Newest")
            {
                ChangeButtonColor(btnRecent, true);
                ChangeButtonColor(btnToday, false);
                ChangeButtonColor(btnUpcoming, false);
            }
            else if (btn.Text == "Oldest")
            {
                ChangeButtonColor(btnRecent, false);
                ChangeButtonColor(btnToday, true);
                ChangeButtonColor(btnUpcoming, false);
            }
            else if (btn.Text == "My Posts")
            {
                ChangeButtonColor(btnRecent, false);
                ChangeButtonColor(btnToday, false);
                ChangeButtonColor(btnUpcoming, true);
            }
        }

        private void ChangeButtonColor(Button btn, bool isSelected)
        {
            if (isSelected)
            {
                btn.BackgroundColor = Color.FromHex("#FF4359");
                btn.TextColor = Color.White;
            }
            else
            {
                btn.BackgroundColor = Color.White;
                btn.TextColor = Color.FromHex("#0C1F4B");
            }
        }

       
    }
}