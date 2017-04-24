using System;
using Xamarin.Forms;

namespace RugbyTracker
{
	public partial class RugbyTeamItemPage : ContentPage
	{
		public RugbyTeamItemPage()
		{
			InitializeComponent();

			Title = "Add";

			var nameEntry = new Entry();
			nameEntry.SetBinding(Entry.TextProperty, "Name");

			var notesEntry = new Entry();
			notesEntry.SetBinding(Entry.TextProperty, "Notes");

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += async (sender, e) =>
			{
				var rugbyTeam = (RugbyTeam)BindingContext;
				await App.Database.SaveTeam(rugbyTeam);
				await Navigation.PopAsync();
			};

			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += async (sender, e) =>
			{
				var rugbyTeam = (RugbyTeam)BindingContext;
				await App.Database.DeleteTeam(rugbyTeam);
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
					new Label { Text = "Name" },
					nameEntry,
					new Label { Text = "Notes" },
					notesEntry,
					saveButton,
					deleteButton,
					cancelButton
				}
			};
		}

		async void OnSaveClicked(object sender, EventArgs e)
		{
			var rugbyTeam = (RugbyTeam)BindingContext;
			await App.Database.SaveTeam(rugbyTeam);
			await Navigation.PopAsync();
		}

		async void OnDeleteClicked(object sender, EventArgs e)
		{
			var rugbyTeam = (RugbyTeam)BindingContext;
			await App.Database.DeleteTeam(rugbyTeam);
			await Navigation.PopAsync();
		}

		async void OnCancelClicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

	}
}
