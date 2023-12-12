namespace SnakesLaddersTue;

public partial class WelcomePage : ContentPage
{
    private int noofPlayers;
	public WelcomePage()
	{
		InitializeComponent();
        noofPlayers = 2;
        UpdatePlayerNumbers();

    }

    private void UpdatePlayerNumbers() {
        noPlayersText.Text = noofPlayers + " players selected";
        for (int i=0;  i<noofPlayers; i++) {
            PlayerNameGrid.RowDefinitions[i].Height = 50;
        }
        for(int j=noofPlayers; j<4; j++) {
            PlayerNameGrid.RowDefinitions[j].Height = 0;
        }
        PlayerNameGrid.HeightRequest = 50 * noofPlayers;
    }

    private void stepPlayers_ValueChanged(object sender, ValueChangedEventArgs e) {
        noofPlayers = (int)stepPlayers.Value;
        UpdatePlayerNumbers();
    }

    private async void PlayGame_Clicked(object sender, EventArgs e) {
        Preferences.Default.Set("numberofplayers", noofPlayers);
        for (int i = 0; i < noofPlayers; i++) {
            switch (i) {
                case 0:
                    Preferences.Default.Set("player" + i, player1Entry.Text);
                    break;
                case 1:
                    Preferences.Default.Set("player" + i, player2Entry.Text);
                    break;
            }
        }
        Preferences.Default.Set("windowwidth", this.Width);
        await Shell.Current.GoToAsync("//MainPage", true);
    }
}