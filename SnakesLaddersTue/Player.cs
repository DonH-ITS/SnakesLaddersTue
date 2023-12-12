

namespace SnakesLaddersTue
{
    public class Player
    {
        public string Name { get; set; }
        private int position;
        private int row;
        private int column;
        private Image playerimage;
        public static Grid mainGrid;

        public int[] CurrentPosition
        {
            get
            {
                int[] pos = new int[2];
                pos[0] = row;
                pos[1] = column;
                return pos;
            }
        }

        public Player(int playerno) {
            position = 1;
            row = 9;
            column = 0;
            string filename = "player" + (playerno + 1) + ".png";
            playerimage = new Image
            {
                Source = ImageSource.FromFile(filename),
                ZIndex = 20
            };
            mainGrid.Add(playerimage, column, row);
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

        public async Task MovePlayerSnakeLadder(int r, int c) {
            double xStep = mainGrid.Width / 10;
            double yStep = mainGrid.Height / 12;
            int columnchange = c - column;
            int rowchange = r - row;
            await playerimage.TranslateTo(xStep*columnchange, yStep*rowchange, 500);
            row = r;
            column = c;
            playerimage.TranslationY = 0;
            playerimage.TranslationX = 0;
            playerimage.SetValue(Grid.RowProperty, row);
            playerimage.SetValue(Grid.ColumnProperty, column);
            position = WhatNumber(row, column);
        }

        private static int WhatNumber(int row, int col) {
            if (row % 2 == 0) {
                int start = 100 - row * 10;
                return start - col;
            }
            else {
                int start = (9 - row) * 10 + 1;
                return start + col;
            }
        }
    }
}
