﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MicrochartsSample
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainView : Xamarin.Forms.ContentPage
	{
		public MainView()
		{
			InitializeComponent();
		}

		public static IEnumerable<Microcharts.ChartEntry> GenerateEntries()
		{
			var values = Enumerable.Range(0, 12).Select(x => random.Next(-1000, 1000)).ToArray();
			return new Microcharts.ChartEntry[]
			{
				new Microcharts.ChartEntry(values[0]) { ValueLabel = values[0].ToString(), Label = "January", Color = SKColor.Parse("#266489") },
				new Microcharts.ChartEntry(values[1]) { ValueLabel = values[1].ToString(),Label = "February", Color = SKColor.Parse("#68B9C0") },
				new Microcharts.ChartEntry(values[2]) { ValueLabel = values[2].ToString(),Label = "March", Color = SKColor.Parse("#90D585") },
				new Microcharts.ChartEntry(values[3]) { ValueLabel = values[3].ToString(),Label = "April", Color = SKColor.Parse("#F3C151")},
				new Microcharts.ChartEntry(values[4]) { ValueLabel = values[4].ToString(),Label = "May", Color = SKColor.Parse("#F37F64")},
				new Microcharts.ChartEntry(values[5]) { ValueLabel = values[5].ToString(),Label = "June", Color = SKColor.Parse("#424856") },
				new Microcharts.ChartEntry(values[6]) { ValueLabel = values[6].ToString(),Label = "July", Color = SKColor.Parse("#8F97A4")},
				new Microcharts.ChartEntry(values[7]) { ValueLabel = values[7].ToString(),Label = "August", Color = SKColor.Parse("#DAC096") },
				new Microcharts.ChartEntry(values[8]) { ValueLabel = values[8].ToString(),Label = "September", Color = SKColor.Parse("#76846E") },
				new Microcharts.ChartEntry(values[9]) { ValueLabel = values[9].ToString(),Label = "October", Color = SKColor.Parse("#A65B69") },
				new Microcharts.ChartEntry(values[10]) { ValueLabel = values[10].ToString(),Label = "November", Color = SKColor.Parse("#DABFAF") },
				new Microcharts.ChartEntry(values[11]) { ValueLabel = values[11].ToString(),Label = "December", Color = SKColor.Parse("#97A69D") },
			};
		}

		private static Random random = new Random();

		private int chartType = 0;

		private Type[] ChartTypes =
		{
			typeof(BarChart),
			typeof(PointChart),
			typeof(LineChart),
			typeof(DonutChart),
			typeof(PieChart),
			typeof(RadarChart),
			typeof(RadialGaugeChart),
		};

		protected override void OnAppearing()
		{
			base.OnAppearing();
			ChangeChart(null, null);
		}

		private void ChangeChart(object sender, EventArgs e)
		{
			chartType = (chartType + 1) % ChartTypes.Length;
			var type = ChartTypes[chartType];
			chart.Chart = Activator.CreateInstance(type) as Chart;
			chart.Chart.MinValue = -1000;
			chart.Chart.MaxValue = 1000;
			GenerateData(null, null);
		}

		private void GenerateData(object sender, EventArgs e)
		{
			if (chart.Chart != null)
			{
				chart.Chart.Entries = GenerateEntries();
			}
		}
	}
}