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

        public Player(Image img, string name) {
            position = 1;
            row = 9;
            column = 0;
            playerimage = img;
            this.Name = name;
        }

        public async Task MovePlayerCharacter(int amount) {
            //column += amount;
            for(int i=0; i < amount; i++) {
                if (position % 10 == 0) { //We will want them to move Vertically
                    await MoveVertically();
                }
                else {
                    await MoveHorizontally();
                }
            }
            
        }

        public async Task MoveHorizontally() {
            position++;
            int direction = 1;
            if (row % 2 == 0) direction = -1;

            column += direction;
            await playerimage.TranslateTo(48*direction , 0, 300);
            playerimage.TranslationX = 0;
            playerimage.SetValue(Grid.ColumnProperty, column);
        }

        public async Task MoveVertically() {
            position++;
            row--;
            await playerimage.TranslateTo(0, -48, 300);
            playerimage.TranslationY = 0;
            playerimage.SetValue(Grid.RowProperty, row);
        }
    }
}
