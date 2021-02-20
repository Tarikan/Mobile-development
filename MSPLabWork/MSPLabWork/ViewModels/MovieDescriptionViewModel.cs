using MSPLabWork.Models;
using System;
using System.Net.Http;
using Xamarin.Forms;

namespace MSPLabWork.ViewModels
{
    [QueryProperty(nameof(ImdbId), nameof(ImdbId))]
    public class MovieDescriptionViewModel : BaseViewModel
    {
        public Command GoBackCommand { get; }

        public new string Title
        {
            get => movieTitle;
            set => SetProperty(ref movieTitle, value);
        }

        private string movieTitle;

        public string Genre
        {
            get => genre;
            set => SetProperty(ref genre, value);
        }

        private string genre;

        public string Year
        {
            get => year;
            set => SetProperty(ref year, value);
        }

        private string year;

        public string Director
        {
            get => director;
            set => SetProperty(ref director, value);
        }

        private string director;

        public string Actors
        {
            get => actors;
            set => SetProperty(ref actors, value);
        }

        private string actors;

        public string Country
        {
            get => country;
            set => SetProperty(ref country, value);
        }

        private string country;

        public string Language
        {
            get => language;
            set => SetProperty(ref language, value);
        }

        private string language;

        public string Production
        {
            get => production;
            set => SetProperty(ref production, value);
        }

        private string production;

        public string Released
        {
            get => released;
            set => SetProperty(ref released, value);
        }

        private string released;

        public string Runtime
        {
            get => runtime;
            set => SetProperty(ref runtime, value);
        }

        private string runtime;

        public string Awards
        {
            get => awards;
            set => SetProperty(ref awards, value);
        }

        private string awards;

        public string imdbRating
        {
            get => rating;
            set => SetProperty(ref rating, value);
        }

        private string rating;

        public string Plot
        {
            get => plot;
            set => SetProperty(ref plot, value);
        }

        private string plot;

        public string Poster
        {
            get => poster;
            set => SetProperty(ref poster, value);
        }

        private string poster;

        public ImageSource PosterImageSource
        {
            get => posterImageSource;
            set => SetProperty(ref posterImageSource, value);
        }

        private ImageSource posterImageSource;

        public string ImdbId { get => imdbId; set {
                imdbId = value;
                LoadInfo(value);
            } }

        private string imdbId;

        public MovieDescriptionViewModel()
        {
            Title = "Movie Description";
            GoBackCommand = new Command(OnBack);
        }

        protected async void LoadInfo(string imdbId)
        {
            var httpClient = new HttpClient();
            MovieInfo info;
            HttpResponseMessage data;
            try
            {
                data = await httpClient.GetAsync(new Uri($"https://www.omdbapi.com/?i=" +
                    $"{imdbId}" +
                    $"&apikey={Resources.Resources.ImdbAPIKey}"));
                info = Services.MovieReadService.ExtractMovieInfoFromString(await data.Content.ReadAsStringAsync());
            }
            catch
            {
                info = await App.DataBase.GetMovieInfoAsync(imdbId);
                if (info == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Cannot retrieve movie info", "ok");
                    await Shell.Current.GoToAsync("..");
                    return;
                }
                
            }
            //var info = Services.MovieReadService.ExtractMovieInfo(imdbId);
            
            
            if (info != null)
            {
                await App.DataBase.StoreMovieInfoAsync(info);
                Utils.PropertyCopier<MovieInfo, MovieDescriptionViewModel>.Copy(info, this);
            }
            else
            {
                Console.WriteLine("Info is null");
                return;
            }
            try
            {
                //PosterImageSource = ImageSource.FromUri(new Uri(Poster));
                PosterImageSource = new UriImageSource
                {
                    CachingEnabled = true,
                    Uri = new Uri(Poster)
                };
            }
            catch
            { }
        }

        async void OnBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
