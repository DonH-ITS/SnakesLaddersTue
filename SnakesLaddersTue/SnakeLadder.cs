namespace SnakesLaddersTue
{
    public class SnakeLadder
    {
        private int StartCol;
        private int EndCol;
        private int StartRow;
        private int EndRow;
        private Image img;
        private Grid grid;

        public SnakeLadder(int StartR, int EndR, int StartC, int EndC, Grid grid) {
            this.StartRow = StartR;
            this.EndRow = EndR;
            this.StartCol = StartC;
            this.EndCol = EndC;
            this.grid = grid;
            if(StartRow > EndRow) 
                placeladderongrid();
        }

        private void CreateImage(double height, double width, string fileName) {
            img = new Image
            {
                Source = ImageSource.FromFile(fileName),
                WidthRequest = width,
                HeightRequest = height,
                Aspect = Aspect.Fill,
                ZIndex = 5
            };
        }

        private void placeladderongrid() {
            int Gridwidth = Math.Abs(EndCol - StartCol) + 1;
            int Gridheight = StartRow - EndRow + 1;
            double xStep = grid.WidthRequest / 10;
            double yStep = grid.HeightRequest / 12;
            double direction = 1.0;
            if (Gridwidth == 1) {
                CreateImage(yStep*Gridheight - 20, xStep - 5, "ladder01.png");
            }
            else {
                double endHeight = Math.Sqrt( Gridwidth*Gridwidth + Gridheight*Gridheight );
                if(StartCol > EndCol) {
                    direction = -1.0;
                }
                CreateImage(yStep * endHeight - 25, xStep - 10, "ladder01.png");
                double tan = direction*Gridwidth / Gridheight;
                double radian = Math.Atan(tan);
                double degrees = 180 * radian / Math.PI;
                img.Rotation = degrees;
            }

            grid.Add(img);
            img.SetValue(Grid.RowProperty, EndRow);
            img.SetValue(Grid.RowSpanProperty, Gridheight);
            if(direction > 0)
                img.SetValue(Grid.ColumnProperty, StartCol);
            else
                img.SetValue(Grid.ColumnProperty, EndCol);
            img.SetValue(Grid.ColumnSpanProperty, Gridwidth);
        }



        private bool IsPlayerOnIt() {
            return true;
        }
    }
}
