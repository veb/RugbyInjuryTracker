using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

namespace RugbyTracker
{
	public partial class RugbyTeamPage : ContentPage
	{
		public RugbyTeamPage()
		{
			InitializeComponent();

			Title = "Teams";

			listView = new ListView
			{
				Margin = new Thickness(20),
				ItemTemplate = new DataTemplate(() =>
				{
					var label = new Label
					{
						VerticalTextAlignment = TextAlignment.Center,
						HorizontalOptions = LayoutOptions.StartAndExpand
					};
					label.SetBinding(Label.TextProperty, "Name");

					var stackLayout = new StackLayout
					{
						Margin = new Thickness(20, 0, 0, 0),
						Orientation = StackOrientation.Horizontal,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Children = { label }
					};

					return new ViewCell { View = stackLayout };
				})
			};
			listView.ItemSelected += async (sender, e) =>
			{
				var test = e.SelectedItem;

				await Navigation.PushAsync(new RugbyPlayerPage(e.SelectedItem)
				{
					BindingContext = e.SelectedItem as RugbyPlayer
				});
			};

			Content = listView;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			listView.ItemsSource = await App.Database.GetTeams();
		}

		async void OnItemAdded(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new RugbyTeamItemPage
			{
				BindingContext = new RugbyTeam()
			});
		}

		async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			await Navigation.PushAsync(new RugbyPlayerPage(e.SelectedItem)
			{
				BindingContext = e.SelectedItem as RugbyPlayer
			});
		}
	}
}
