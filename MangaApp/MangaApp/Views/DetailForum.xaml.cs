using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MangaApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MangaApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailForum : ContentPage
    {
        public DetailForum(Blogs blog)
        {
            InitializeComponent();
            BlogName.Text = blog.BlogName;
            BlogImg.Source = blog.BlogImg;
            BlogContent.Text = blog.BlogContent;
            User.Text = blog.userName;

        }
    }
}