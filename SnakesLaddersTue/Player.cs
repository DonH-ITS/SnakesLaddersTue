using Microsoft.Web.WebView2.Core;

namespace SnakesLaddersTue
{
    public class Player
    {
        public string Name { get; set; }
        public int position { get; set; }
        public int row {  get; set; }
        public int column { get; set; }
        public Image playerimage { get; set; }

        public Grid mainGrid { get; set; }

        public Player(Image img, string name, Grid grid) {
            position = 1;
            row = 9;
            column = 0;
            playerimage = img;
            this.Name = name;
            this.mainGrid = grid;
        }

        public async Task MovePlayerCharacter(int amount) {
            //Calculate the steps for a movement. Do it now in case we change the dimensions while the game is running
            double xStep = mainGrid.Width / 10;
            double yStep = mainGrid.Height / 12;
            //column += amount;
            for(int i=0; i < amount; i++) {
                if (position % 10 == 0) { //We will want them to move Vertically when their position is a multiple of 10
                    await MoveVertically(xStep);
                }
                else {
                    await MoveHorizontally(yStep);
                }
            }
            
        }

        public async Task MoveHorizontally(double step) {
            position++;
            int direction = 1;

            //If on an even row move right to left
            if (row % 2 == 0) direction = -1;

            column += direction;
            await playerimage.TranslateTo(step*direction , 0, 300);
            playerimage.TranslationX = 0;
            playerimage.SetValue(Grid.ColumnProperty, column);
        }

        public async Task MoveVertically(double step) {
            position++;
            row--;
            await playerimage.TranslateTo(0, -1*step, 300);
            playerimage.TranslationY = 0;
            playerimage.SetValue(Grid.RowProperty, row);
        }
    }
}
