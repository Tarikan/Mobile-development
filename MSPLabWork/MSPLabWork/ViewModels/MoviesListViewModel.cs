using MSPLabWork.Models;
using MSPLabWork.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

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