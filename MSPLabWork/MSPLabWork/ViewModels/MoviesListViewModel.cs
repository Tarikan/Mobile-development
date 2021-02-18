using MSPLabWork.Models;
using MSPLabWork.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;
using Newtonsoft.Json;

namespace MSPLabWork.ViewModels
{
    public class MoviesListViewModel : BaseViewModel
    {
        #region Properties
        public Command AddItemCommand { get; }
        public Command<Movie> ItemTappedCommand { get; }
        public Command RefreshCommand { get; }
        public Command OnAppearingCommand { get; }
        public Command<Movie> DeleteMovieCommand { get; }

        private HttpClient httpClient;

        public ObservableCollection<Movie> Movies
        {
            get => movies;
            set => SetProperty(ref movies, value);
        }

        private ObservableCollection<Movie> movies;

        public string SearchBarText
        {
            get
            {
                if (searchBarText == null)
                    return String.Empty;
                return searchBarText;
            }
            set
            {
                SetProperty(ref searchBarText, value);
                RefreshItems();
            }
        }

        private string searchBarText;

        #endregion Properties


        public MoviesListViewModel()
        {
            Title = "Movies";

            httpClient = new HttpClient();

            Movies = new ObservableCollection<Movie>();

            RefreshCommand = new Command(RefreshItems);

            ItemTappedCommand = new Command<Movie>(OnItemClicked);

            AddItemCommand = new Command(OnAddItem);

            OnAppearingCommand = new Command(OnAppearing);

            DeleteMovieCommand = new Command<Movie>(OnDeleteMovie);
        }

        public async void RefreshItems()
        {
            IsBusy = true;

            if (SearchBarText.Length >= 3)
            {
                var data =
                    await httpClient
                    .GetAsync(new Uri($"https://www.omdbapi.com/?s=" +
                    $"{SearchBarText.Trim().Replace(' ', '+')}" +
                    $"&apikey={Resources.Resources.ImdbAPIKey}&page=1"));
                var json = await data.Content.ReadAsStringAsync();
                if (!data.IsSuccessStatusCode || json == null || json==string.Empty)
                    return;
                //Console.WriteLine(await data.Content.ReadAsStringAsync());
                //Console.WriteLine($"{SearchBarText.Trim().Replace(' ', '+')}");
                try
                {
                    Movies = new ObservableCollection<Movie>(
                    Services.MovieReadService.ExtractMoviesFormString(json));
                }
                catch
                {
                    IsBusy = false;
                    return;
                }
                IsBusy = false;

                return;
            }

            /*
            var data = await App.DataStore.GetItemsAsync();

            if (SearchBarText == String.Empty)
            {
                Movies = new ObservableCollection<Movie>(data);
                //foreach (var item in data.Where(m => !Movies.Contains(m)))
                //    Movies.Add(item);
                IsBusy = false;
                
                return;
            }

            Movies = new ObservableCollection<Movie>(data
                .Where(m => m.Title.IndexOf(SearchBarText, StringComparison.OrdinalIgnoreCase) >= 0));
            /*
            data = data
                .Where(m => m.Title.IndexOf(SearchBarText, StringComparison.OrdinalIgnoreCase) >= 0);
            foreach (var item in Movies.Where(m => !data.Contains(m)))
                Movies.Remove(item);
            foreach (var item in data.Where(m => !Movies.Contains(m)))
                Movies.Add(item);
            */
            IsBusy = false;
        }

        protected async void OnAddItem()
        {
            await Shell.Current.GoToAsync(nameof(NewMoviePage));
            RefreshItems();
        }


        protected async void OnItemClicked(Movie movie)
        {
            if (movie.ImdbID == null ||
                movie.ImdbID == "noid")
                return;

            //Console.WriteLine($"{nameof(MovieDescriptionPage)}?{nameof(MovieDescriptionViewModel.ImdbId)}={movie.ImdbID}");
            await Shell.Current.GoToAsync($"{nameof(MovieDescriptionPage)}?{nameof(MovieDescriptionViewModel.ImdbId)}={movie.ImdbID}");
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        public async void OnDeleteMovie(Movie movie)
        {
            Console.WriteLine(movie.Title);

            await App.DataStore.DeleteItemAsync(movie);

            Movies.Remove(movie);
        }
    }
}