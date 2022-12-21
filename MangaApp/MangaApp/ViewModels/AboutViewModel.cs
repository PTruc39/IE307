using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MangaApp.ViewModels
{
    public class AboutViewModel : INotifyPropertyChanged
    {
        Host host = new Host();
        HttpClient client = new HttpClient();
        public ObservableCollection<Manga> _employees;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Manga> Mangas
        {
            get { return _employees; }
            set { _employees = value; OnPropertyChanged(); }
        }
        public async Task<ObservableCollection<Manga>> LayDSLoaiHoa()
        {
            //List<Manga> dslh = null;
            HttpClient http = new HttpClient();
            var kq = await http.GetStringAsync
                (host.url + "api/manga/GetMangaList");
            return await Task.FromResult(JsonConvert.DeserializeObject<ObservableCollection<Manga>>(kq));
            /*foreach (var item in dslh)
            {
                Mangas.Add(new Manga() { MangaName = item.MangaName, MangaID = item.MangaID, MangaImage = item.MangaImage, Description = item.Description });
            }*/
            //lstdslh.ItemsSource = Mangas;
            //lstdslh2.ItemsSource = dslh;
            //this.BindingContext = this;
        }
        public AboutViewModel()
        {

            Task.Run(async () =>
            {
                Mangas = await LayDSLoaiHoa();
            });
            
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}