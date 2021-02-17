using MSPLabWork.ViewModels;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MSPLabWork.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageGridPage : ContentPage
    {
        private int _photoCounter = 0;

        private int _gridCounter = 0;

        private ImageGridViewModel _viewModel;

        public static readonly BindableProperty FrameWidthProperty =
            BindableProperty.Create("Width", typeof(double), typeof(ImageGridPage));

        public double FrameWidth
        {
            get => (double)GetValue(FrameWidthProperty);
            set => SetValue(FrameWidthProperty, value);
        }
        
        public ImageGridPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ImageGridViewModel();
            
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            //Console.WriteLine(ImageLayout.Width);
            FrameWidth = ImageLayout.Width / 5;
        }

        protected BoxView CreateBox()
        {
            
            var ret = new BoxView()
            {
                Color = Color.Blue,
                Margin=5
            };

            


            switch (_photoCounter % 6)
            {
                case 0:
                    AbsoluteLayout.SetLayoutBounds(ret, new Rectangle(0,
                        0 + _gridCounter* FrameWidth * 4,
                        3 * FrameWidth,
                        FrameWidth * 3));
                    break;
                case 1:
                    AbsoluteLayout.SetLayoutBounds(ret, new Rectangle(0.6,
                        0 + _gridCounter * FrameWidth * 4,
                        2 * FrameWidth,
                        FrameWidth * 2));
                    break;
                case 2:
                    AbsoluteLayout.SetLayoutBounds(ret, new Rectangle(0.6,
                        FrameWidth * 2 + _gridCounter * FrameWidth * 4,
                        2 * FrameWidth,
                        FrameWidth * 2));
                    break;
                case 3:
                    AbsoluteLayout.SetLayoutBounds(ret, new Rectangle(0,
                        FrameWidth * 3 + _gridCounter * FrameWidth * 4,
                        FrameWidth,
                        FrameWidth));
                    break;
                case 4:
                    AbsoluteLayout.SetLayoutBounds(ret, new Rectangle(0.2,
                        FrameWidth * 3 + _gridCounter * FrameWidth * 4,
                        FrameWidth,
                        FrameWidth));
                    break;
                case 5:
                    AbsoluteLayout.SetLayoutBounds(ret, new Rectangle(0.4,
                        FrameWidth * 3 + _gridCounter * FrameWidth * 4,
                        FrameWidth,
                        FrameWidth));
                    _gridCounter++;
                    break;
            }
            _photoCounter++;

            //AbsoluteLayout.SetLayoutFlags(ret, AbsoluteLayoutFlags.XProportional);
            //AbsoluteLayout.SetLayoutFlags(ret, AbsoluteLayoutFlags.WidthProportional);
            AbsoluteLayout.SetLayoutFlags(ret, AbsoluteLayoutFlags.XProportional);

            return ret;
        }

        protected async void OnAddButtonClicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("no upload", "picking a photo is not supported", "ok");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return;
            ImageLayout.AddElement(ImageSource.FromStream(() => file.GetStream()));
        }
    }
}