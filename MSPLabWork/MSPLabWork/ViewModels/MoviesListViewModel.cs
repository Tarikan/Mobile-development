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
                HttpResponseMessage data;
                string json;
                List<Movie> res;
                try
                {
                    data =
                    await httpClient
                    .GetAsync(new Uri($"https://www.omdbapi.com/?s=" +
                    $"{SearchBarText.Trim().Replace(' ', '+')}" +
                    $"&apikey={Resources.Resources.ImdbAPIKey}&page=1"));
                    json = await data.Content.ReadAsStringAsync();
                    if (!data.IsSuccessStatusCode || json == null || json == string.Empty)
                        return;
                    res = Services.MovieReadService.ExtractMoviesFormString(json);
                }
                catch
                {
                    
                    res = await App.DataBase.SearchMoviesAsync(SearchBarText);
                    Console.WriteLine($"Result from db is null: {res == null}");
                    Console.WriteLine($"Results len {res.Count()}");
                    if (res == null)
                        return;
                }

                try
                {
                    await App.DataBase.StoreMoviesAsync(res);
                    Movies = new ObservableCollection<Movie>(res
                    );
                }
                catch
                {
                    IsBusy = false;
                    return;
                }
                IsBusy = false;

                return;
            }
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
            if (SearchBarText.Length >= 3 && Movies.Count() == 0)
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