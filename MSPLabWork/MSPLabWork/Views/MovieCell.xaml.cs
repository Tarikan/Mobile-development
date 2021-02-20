using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSPLabWork.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieCell : ViewCell
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title",
            typeof(string), typeof(MovieCell), "Title");
        public static readonly BindableProperty TypeProperty = BindableProperty.Create("Type",
            typeof(string), typeof(MovieCell), "Type");
        public static readonly BindableProperty YearProperty = BindableProperty.Create("Year",
            typeof(string), typeof(MovieCell), "Year");
        public static readonly BindableProperty PosterProperty = BindableProperty.Create("Poster",
            typeof(string), typeof(MovieCell), "Poster");
        public static readonly BindableProperty ImdbIdProperty = BindableProperty.Create("ImdbId",
            typeof(string), typeof(MovieCell), "ImdbId");

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Type
        {
            get => (string)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        public string Year
        {
            get => (string)GetValue(YearProperty);
            set => SetValue(YearProperty, value);
        }

        public string Poster
        {
            get => (string)GetValue(PosterProperty);
            set => SetValue(PosterProperty, value);
        }

        public string ImdbId
        {
            get => (string)GetValue(ImdbIdProperty);
            set => SetValue(ImdbIdProperty, value);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext != null)
            {
                TitleLabel.Text = Title;
                TypeLabel.Text = Type;
                YearLabel.Text = Year;
                //ImdbLabel.Text = $"imdbID {ImdbId}";
                if (Poster != null)
                    try
                    {
                        //PosterImage.Source = ImageSource.FromUri(new Uri(Poster));
                        PosterImage.Source = new UriImageSource
                        {
                            CachingEnabled = true,
                            Uri = new Uri(Poster),
                        };
                    }
                    catch
                    {
                    }
                //PosterImage.Source =
                //    ImageSource.FromResource("MSPLabWork.Resources.Posters." + Poster,
                //        typeof(MovieCell).GetTypeInfo().Assembly);
            }
        }
        public MovieCell()
        {
            InitializeComponent();
            TypeLabel.SetBinding(Label.TextProperty, "Type");
            TitleLabel.SetBinding(Label.TextProperty, "Title");
            YearLabel.SetBinding(Label.TextProperty, "Year");
            //ImdbLabel.SetBinding(Label.TextProperty, "ImdbId");
            PosterImage.SetBinding(Image.SourceProperty, "Poster");
        }
    }
}