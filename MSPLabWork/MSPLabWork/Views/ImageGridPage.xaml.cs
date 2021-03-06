﻿using MSPLabWork.ViewModels;
using Plugin.Media;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MSPLabWork.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageGridPage : ContentPage
    {
        private ImageGridViewModel _viewModel;
        
        public ImageGridPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ImageGridViewModel();
            //var thread = new Thread(LoadImages);
            //thread.Start();
            _ = LoadImages();
        }

        protected async Task LoadImages()
        {
            var httpClient = new HttpClient();

            string content;

            var adress = "https://pixabay.com/api/?key=19193969-87191e5db266905fe8936d565&q=small+animals&image_type=photo&per_page=18";

            try
            {
                var req = await httpClient.GetAsync(new Uri(
                    adress
                ));
                content = await req.Content.ReadAsStringAsync();

                await App.DataBase.CacheReqestAsync(new Models.CachedRequest() { Request = adress, ResponseBody = content});
            }
            catch
            {
                content = (await App.DataBase.GetCachedRequestAsync(
                    adress)).ResponseBody;

                if (string.IsNullOrEmpty(content))
                    return;
            }

            var o = JObject.Parse(content);

            List<string> images = o["hits"].Select(u => (string)u["previewURL"]).ToList();

            foreach(var image in images)
            {
                //ImageLayout.AddElement(ImageSource.FromUri(new Uri(image)));
                ImageLayout.AddElement(new UriImageSource
                {
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(5, 0, 0, 0),
                    Uri = new Uri(image)
                });
            }
        }

        protected async void OnAddButtonClicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("no upload", "picking a photo is not supported", "ok");
                return;
            }

            NavigationPage.SetHasNavigationBar(this, false);

            var file = await CrossMedia.Current.PickPhotoAsync();

            NavigationPage.SetHasNavigationBar(this, true);

            if (file == null)
                return;
            ImageLayout.AddElement(ImageSource.FromStream(() => file.GetStream()));
        }
    }
}