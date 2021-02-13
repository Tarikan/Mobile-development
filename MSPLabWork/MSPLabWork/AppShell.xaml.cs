using MSPLabWork.ViewModels;
using MSPLabWork.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MSPLabWork
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MovieDescriptionPage), typeof(MovieDescriptionPage));
            Routing.RegisterRoute(nameof(NewMoviePage), typeof(NewMoviePage));
        }

    }
}
