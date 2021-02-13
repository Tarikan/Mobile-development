using MSPLabWork.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace MSPLabWork.ViewModels
{
    [QueryProperty(nameof(ImdbId), nameof(ImdbId))]
    public class MovieDescriptionViewModel : BaseViewModel
    {
        public Command GoBackCommand { get; }

        public string Title
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

        protected void LoadInfo(string imdbId)
        {
            var info = Services.MovieReadService.ExtractMovieInfo(imdbId);
            Console.WriteLine("MSPLabWork.Resources.Posters." + info.Poster);
            if (info != null)
            {
                Utils.PropertyCopier<MovieInfo, MovieDescriptionViewModel>.Copy(info, this);
            }
            else
            {
                Console.WriteLine("Info is null");
                return;
            }
            PosterImageSource = ImageSource.FromResource("MSPLabWork.Resources.Posters." + Poster,
                    typeof(MovieDescriptionViewModel).GetTypeInfo().Assembly);
        }

        async void OnBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
