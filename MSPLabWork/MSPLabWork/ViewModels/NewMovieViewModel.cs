using MSPLabWork.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MSPLabWork.ViewModels
{
    class NewMovieViewModel : BaseViewModel
    {
        #region Properties
        public Command GoBackCommand { get; }

        public Command SubmitCommand { get; }

        public string MovieTitle {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string title;

        public string Type {
            get => movieType;
            set => SetProperty(ref movieType, value);
        }

        private string movieType;

        public string Year {
            get => year;
            set => SetProperty(ref year, value);
        }

        private string year;

        #endregion Properties

        public NewMovieViewModel()
        {
            Title = "New Movie";

            GoBackCommand = new Command(OnBack);

            SubmitCommand = new Command(OnSubmit);
        }

        private async void OnBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSubmit()
        {
            if (Year == null ||
                Title == null ||
                Type == null)
            {
                Console.WriteLine($"Year is null {Year == null}\n" +
                    $"Title is null {Title == null}\n" +
                    $"Type is null {Type == null}");
                return;
            }

            try
            {
                int.Parse(Year);
            }
            catch
            {
                Console.WriteLine($"Cannot parse year {Year}");
                return;
            }

            var movie = new Movie()
            {
                Year = Year,
                Title = MovieTitle,
                Type = Type,
                ImdbID = "noid"
            };

            await App.DataStore.AddItemAsync(movie);

            OnBack();
        }
    }
}
