using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Diagnostics;
using System.Reflection;

namespace RugbyTracker
{
	public partial class RugbyPlayerPage : ContentPage
	{
		public int TeamID { get; set; }

		public int GetID(object obj)
		{
			Type type = obj.GetType();
			IEnumerable<PropertyInfo> props = type.GetRuntimeProperties();

			int ID = 0;
			foreach (PropertyInfo prop in props)
			{
				if (String.Equals(prop.Name, "ID"))
				{
					ID = Convert.ToInt32(prop.GetValue(obj).ToString());
				}
			}

			return ID;
		}

		public RugbyPlayerPage(object Team)
		{
			InitializeComponent();

			Title = "Players";

			this.TeamID = this.GetID(Team);

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
				int PlayerID = this.GetID(e.SelectedItem);
				await Navigation.PushAsync(new RugbyItemListPage(PlayerID)
				{
					BindingContext = e.SelectedItem as RugbyItem
				});
			};

			Content = listView;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			listView.ItemsSource = await App.Database.GetPlayers(this.TeamID);
		}

		async void OnItemAdded(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new RugbyPlayerItemPage(this.TeamID)
			{
				BindingContext = new RugbyPlayer()
			});
		}

		async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			int PlayerID = this.GetID(e.SelectedItem);
			await Navigation.PushAsync(new RugbyItemListPage(PlayerID)
			{
				BindingContext = e.SelectedItem as RugbyItem
			});
		}
	}
}
