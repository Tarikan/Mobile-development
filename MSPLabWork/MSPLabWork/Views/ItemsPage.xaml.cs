using Microcharts;
using MSPLabWork.ViewModels;
using System.Linq;
using Xamarin.Forms;
using Lib;

namespace MSPLabWork.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        protected PieChart pieChart;

        protected LineChart lineChart;

        public ItemsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ItemsViewModel();
        }

        protected Chart CreatePie()
        {
            if (pieChart != null)
                return pieChart;
            pieChart = new PieChart
            {
                Entries = new ChartEntry[] {
                new ChartEntry(10) { Color=SkiaSharp.SKColor.Parse("#FFFF00"), Label = "10%", ValueLabelColor = SkiaSharp.SKColor.Parse("#FFFF00")},
                new ChartEntry(20) { Color=SkiaSharp.SKColor.Parse("#996600"), Label = "20%", ValueLabelColor = SkiaSharp.SKColor.Parse("#996600")},
                new ChartEntry(25) { Color=SkiaSharp.SKColor.Parse("#808080"), Label = "25%", ValueLabelColor = SkiaSharp.SKColor.Parse("#808080")},
                new ChartEntry(5) { Color=SkiaSharp.SKColor.Parse("#ff0000"), Label = "5%", ValueLabelColor = SkiaSharp.SKColor.Parse("#ff0000")},
                new ChartEntry(40) { Color=SkiaSharp.SKColor.Parse("#8a2be2"), Label = "40%", ValueLabelColor = SkiaSharp.SKColor.Parse("#8a2be2")}
                }
            };
            return pieChart;
        }

        protected Chart CreateLine()
        {
            if (lineChart != null)
                return lineChart;

            PlotPoints.Function del = (float x) =>
            {
                return (float)System.Math.Log(x);
            };

            lineChart = new LineChart()
            {
                Entries = PlotPoints.CreateDots(-4, 4, del, 200)
                    .Where(p => !float.IsNaN(p))
                    .Select(p => new ChartEntry(p)
                        { Color = SkiaSharp.SKColor.Parse("#ff0000")}).ToArray(),
                LineMode = LineMode.Straight, PointMode = PointMode.None, EnableYFadeOutGradient=false
            };
            
            return lineChart;
        }

        protected async void OnToggled(object sender, ToggledEventArgs e)
        {
            if (chartSwitch.IsToggled)
            {
                chartLabel.Text = "Pie Diagram";
                chartView.Chart = CreatePie();
            }
            else
            {
                chartLabel.Text = "Line Diagram";
                chartView.Chart = CreateLine();
            }
            chartView.Chart.AnimationProgress = 0;
            await chartView.Chart.AnimateAsync(true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            chartLabel.Text = "Line Diagram";
            chartSwitch.IsToggled = false;
            chartView.Chart = CreateLine();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            chartView.HeightRequest = height - chartLabel.Height - chartSwitch.Height - 20;
        }
    }
}