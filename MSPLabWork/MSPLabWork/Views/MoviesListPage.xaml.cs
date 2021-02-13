using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MSPLabWork.Models;
using MSPLabWork.ViewModels;
using MSPLabWork.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSPLabWork.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesListPage : ContentPage
    {
        MoviesListViewModel _viewModel;
        public MoviesListPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MoviesListViewModel();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            Movie movie = e.Item as Movie;


            if (movie.ImdbID == "noid")
                return;

            //Console.WriteLine($"{nameof(MovieDescriptionPage)}?{nameof(MovieDescriptionViewModel.ImdbId)}={movie.ImdbID}");
            await Shell.Current.GoToAsync($"{nameof(MovieDescriptionPage)}?{nameof(MovieDescriptionViewModel.ImdbId)}={movie.ImdbID}");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        protected void OnDeleteMovie(object sender, EventArgs e)
        {
            var movie = ((MenuItem)sender).CommandParameter as Movie;
            Console.WriteLine(movie.Title);
            _viewModel.OnDeleteMovie(movie);
        }
    }
}
