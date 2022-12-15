using System.ComponentModel;
using MangaApp.ViewModels;
using Xamarin.Forms;

namespace MangaApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}