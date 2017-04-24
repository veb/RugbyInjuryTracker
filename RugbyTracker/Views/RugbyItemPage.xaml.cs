using System;
using Xamarin.Forms;

namespace RugbyTracker
{
	public partial class RugbyItemPage : ContentPage
	{
		public int PlayerID { get; set; }

		public RugbyItemPage(int newPlayerID)
		{
			InitializeComponent();

			Title = "Injury Details";

            this.PlayerID = newPlayerID;

			var nameEntry = new Entry();
			nameEntry.SetBinding(Entry.TextProperty, "Name");

			var notesEntry = new Entry();
			notesEntry.SetBinding(Entry.TextProperty, "Notes");

			var doneSwitch = new Switch();
			doneSwitch.SetBinding(Switch.IsToggledProperty, "Done");

			var saveButton = new Button { Text = "Save" };
			saveButton.Clicked += async(sender, e) =>
			{
				var rugbyItem = (RugbyItem)BindingContext;
				rugbyItem.PlayerId = this.PlayerID;
				await App.Database.SaveItem(rugbyItem);
				await Navigation.PopAsync();
			};

			var deleteButton = new Button { Text = "Delete" };
			deleteButton.Clicked += async(sender, e) =>
			{
				var rugbyItem = (RugbyItem)BindingContext;
				await App.Database.DeleteItem(rugbyItem);
				await Navigation.PopAsync();
			};

			var cancelButton = new Button { Text = "Cancel" };
			cancelButton.Clicked += async(sender, e) =>
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
					new Label { Text = "Done" },
					doneSwitch,
					saveButton,
					deleteButton,
					cancelButton
				}
			};
		}

		async void OnSaveClicked(object sender, EventArgs e)
		{
			var rugbyItem = (RugbyItem)BindingContext;
			rugbyItem.PlayerId = this.PlayerID;
			await App.Database.SaveItem(rugbyItem);
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
