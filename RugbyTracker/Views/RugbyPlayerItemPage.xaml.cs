using System;
using Xamarin.Forms;

namespace RugbyTracker
{
	public partial class RugbyPlayerItemPage : ContentPage
	{
		public int TeamID { get; set; }

		public RugbyPlayerItemPage(int newTeamID)
		{
			InitializeComponent();

			this.TeamID = newTeamID;

			Title = "Add Player";

			var nameEntry = new Entry();
			nameEntry.SetBinding(Entry.TextProperty, "Name");

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += async (sender, e) =>
			{
				var rugbyPlayer = (RugbyPlayer)BindingContext;
				rugbyPlayer.TeamId = this.TeamID;
				await App.Database.SavePlayer(rugbyPlayer);
				await Navigation.PopAsync();
			};

			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += async (sender, e) =>
			{
				var rugbyItem = (RugbyItem)BindingContext;
				await App.Database.DeleteItem(rugbyItem);
				await Navigation.PopAsync();
			};

			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += async (sender, e) =>
			{
				await Navigation.PopAsync();
			};

			Content = new StackLayout
			{
				Margin = new Thickness(20),
				VerticalOptions = LayoutOptions.StartAndExpand,
				Children =
				{
					new Label { Text = "Player Name" },
					nameEntry,
					saveButton,
					deleteButton,
					cancelButton
				}
			};
		}

		async void OnSaveClicked(object sender, EventArgs e)
		{
			var rugbyPlayer = (RugbyPlayer)BindingContext;
			rugbyPlayer.TeamId = this.TeamID;
			await App.Database.SavePlayer(rugbyPlayer);
			await Navigation.PopAsync();
		}

		async void OnDeleteClicked(object sender, EventArgs e)
		{
			var rugbyItem = (RugbyItem)BindingContext;
			await App.Database.DeleteItem(rugbyItem);
			await Navigation.PopAsync();
		}

		async void OnCancelClicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

	}
}
