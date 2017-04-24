using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;

namespace RugbyTracker
{
	public partial class RugbyItemListPage : ContentPage
	{
		public int PlayerID { get; set; }

		public RugbyItemListPage(int newPlayerID)
		{
			InitializeComponent();

			Title = "Injuries";

            this.PlayerID = newPlayerID;

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

					var tick = new Image
					{
						Source = ImageSource.FromFile("check.png"),
						HorizontalOptions = LayoutOptions.End
					};
					tick.SetBinding(VisualElement.IsVisibleProperty, "Done");

					var stackLayout = new StackLayout
					{
						Margin = new Thickness(20, 0, 0, 0),
						Orientation = StackOrientation.Horizontal,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Children = { label, tick }
					};

					return new ViewCell { View = stackLayout };
				})
			};
			listView.ItemSelected += async (sender, e) =>
			{
				var test = e.SelectedItem;

				await Navigation.PushAsync(new RugbyItemPage(this.PlayerID)
				{
					BindingContext = e.SelectedItem as RugbyItem
				});
			};

			Content = listView;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			listView.ItemsSource = await App.Database.GetItems(this.PlayerID);
		}

		async void OnItemAdded(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new RugbyItemPage(this.PlayerID)
			{
				BindingContext = new RugbyItem()
			});
		}

		async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			await Navigation.PushAsync(new RugbyItemPage(this.PlayerID)
			{
				BindingContext = e.SelectedItem as RugbyItem
			});
		}
	}
}
