namespace SnakesLaddersTue;

public partial class SettingsPage : ContentPage
{
	Settings set;
	public SettingsPage(Settings s)
	{
		set = s;
		InitializeComponent();
		BindingContext = set;
	}

    private async void SaveSettingsBtn_Clicked(object sender, EventArgs e) {
		set.SavetheSettingsFile();
		await Navigation.PopAsync();
    }
}