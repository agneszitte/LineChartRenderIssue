#if __IOS__
using Syncfusion.SfChart.iOS;
using UIKit;
using Foundation;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using LineChartRenderIssue.Shared;
using Windows.UI.Core;

namespace LineChartRenderIssue
{
	public partial class LineChart : ContentControl
	{
		private SFChart chart;

		private ObservableCollection<ChartData> Data = new ObservableCollection<ChartData>()
			{
				new ChartData { Name = "David", Score = 20 },
				new ChartData { Name = "Michael", Score = 45 },
				new ChartData { Name = "Steve", Score = 63 },
				new ChartData { Name = "Joel", Score = 84 }
			};

		public LineChart()
		{
			DefaultStyleKey = typeof(LineChart);
		}

		public ICollection ItemSource
		{
			get => (ICollection)GetValue(ItemSourceProperty);
			set => SetValue(ItemSourceProperty, value);
		}

		public static readonly DependencyProperty ItemSourceProperty =
			DependencyProperty.Register(nameof(ItemSource), typeof(IList), typeof(LineChart), new PropertyMetadata(default(IList), ItemSourceChanged));

		protected override Size MeasureOverride(Size availableSize)
		{
			return base.MeasureOverride(availableSize);

		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			return base.ArrangeOverride(finalSize);
		}

		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			chart = new SFChart();
			chart.BackgroundColor = UIColor.White;
			chart.Alpha = (System.nfloat)0.8;

			//Initializing primary Axis
			SFCategoryAxis primaryAxis = new SFCategoryAxis
			{
				EdgeLabelsDrawingMode = SFChartAxisEdgeLabelsDrawingMode.Fit
			};

			primaryAxis.MajorTickStyle.LineSize = 4;
			chart.PrimaryAxis = primaryAxis;

			//Initializing secondary Axis
			SFNumericalAxis secondaryAxis = new SFNumericalAxis
			{
				Interval = new NSNumber(20),
				Minimum = new NSNumber(0),
				Maximum = new NSNumber(100),
				OpposedPosition = true
			};

			secondaryAxis.MajorTickStyle.LineSize = 0;
			chart.SecondaryAxis = secondaryAxis;

			SFLineSeries series = new SFLineSeries
			{
				ItemsSource = Data,
				XBindingPath = "Name",
				YBindingPath = "Score",
				Color = UIColor.Purple,
			};

			series.DataMarker.MarkerType = SFChartDataMarkerType.Ellipse;
			series.DataMarker.ShowMarker = true;
			series.DataMarker.MarkerWidth = 7;
			series.DataMarker.MarkerHeight = 7;
			series.DataMarker.MarkerColor = UIColor.Purple;
			chart.Series.Add(series);

			Content = chart;
		}

		private static void ItemSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var lineChart = d as LineChart;
			if (lineChart.chart != null)
			{
				lineChart.chart.Series.First().ItemsSource = lineChart.Data;
			}
		}
	}
}
