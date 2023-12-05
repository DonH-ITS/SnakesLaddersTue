namespace SnakesLaddersTue
{
    public class SnakeLadder {
        private int StartCol;
        private int EndCol;
        private int StartRow;
        private int EndRow;
        private Image image;
        private Grid grid;

        public int[] EndPosition{
            get
            {
                int[] pos = new int[2];
                pos[0] = EndRow;
                pos[1] = EndCol;
                return pos;
            }
        }

        public SnakeLadder(int StartR, int EndR, int StartC, int EndC, Grid grid) {
            this.StartRow = StartR;
            this.EndRow = EndR;
            this.StartCol = StartC;
            this.EndCol = EndC;
            this.grid = grid;
            if (StartRow > EndRow)
                placeladderongrid();
            else if(EndRow > StartRow)
                placesnakeongrid();
        }

        private void placesnakeongrid() {
            int Gridheight = Math.Abs(StartRow - EndRow) + 1;
            int Gridwidth = Math.Abs(StartCol - EndCol) + 1;
            double xStep = grid.WidthRequest / 10;
            double yStep = grid.HeightRequest / 12;
            if (Gridwidth == 1) {
                place1colsnake(xStep, yStep, Gridheight);
            }
            else if (Gridheight == 4 && Gridwidth == 3) {
                place4x3snake(xStep, yStep);
            }
            else if (Gridheight == 3 && Gridwidth == 2) {
                place3x2snake(xStep, yStep);
            }
            else {
                //Diagonal Snakes
                placeothersnake(xStep, yStep, Gridwidth, Gridheight);
            }

        }

        private void place1colsnake(double xStep, double yStep, int Gridheight) {
            CreateImage(yStep * Gridheight - 10, xStep - 5, "snake1.png");
            image.SetValue(Grid.RowProperty, StartRow);
            image.SetValue(Grid.RowSpanProperty, Gridheight);
            image.SetValue(Grid.ColumnProperty, EndCol);
            image.SetValue(Grid.ColumnSpanProperty, 1);

        }

        private void place4x3snake(double xStep, double yStep) {
            CreateImage(4 * (yStep - 5), 3 * (xStep - 5), "snake3.png");
            if (StartCol < EndCol) {
                image.SetValue(Grid.ColumnProperty, StartCol);
            }
            else {
                image.SetValue(Grid.ColumnProperty, EndCol);
                image.RotationY = 180;
            }
            image.SetValue(Grid.RowProperty, StartRow);
            image.SetValue(Grid.RowSpanProperty, 4);
            image.SetValue(Grid.ColumnSpanProperty, 3);
        }

        private void place3x2snake(double xStep, double yStep) {
            CreateImage(3 * (yStep - 5), 2 * (xStep - 5), "snake2.png");
            if (StartCol < EndCol) {
                image.SetValue(Grid.ColumnProperty, StartCol);
            }
            else {
                image.SetValue(Grid.ColumnProperty, EndCol);
                image.RotationY = 180;
            }
            image.SetValue(Grid.RowProperty, StartRow);
            image.SetValue(Grid.RowSpanProperty, 3);
            image.SetValue(Grid.ColumnSpanProperty, 2);
        }

        //Diagonal Snake
        private void placeothersnake(double xStep, double yStep, int width, int height) {
            //Pythagoras Theorem
            double endHeight = Math.Sqrt(width * width + height * height);
            CreateImage(endHeight * yStep - 20, xStep - 20, "snake1.png");
            double direction = 1.0;
            if (StartCol < EndCol)
                direction = -1.0;
            double tan = direction * width / height;
            double radian = Math.Atan(tan);
            double degrees = radian * 180 / Math.PI;
            image.Rotation = degrees;
            image.SetValue(Grid.RowProperty, StartRow);
            image.SetValue(Grid.RowSpanProperty, height);
            if (StartCol < EndCol)
                image.SetValue(Grid.ColumnProperty, StartCol);
            else
                image.SetValue(Grid.ColumnProperty, EndCol);
            image.SetValue(Grid.ColumnSpanProperty, width);
        }

        private void CreateImage(double height, double width, string fileName) {
            image = new Image
            {
                Source = ImageSource.FromFile(fileName),
                WidthRequest = width,
                HeightRequest = height,
                Aspect = Aspect.Fill,
                ZIndex = 5
            };

            grid.Add(image);
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
                image.Rotation = degrees;
            }

            image.SetValue(Grid.RowProperty, EndRow);
            image.SetValue(Grid.RowSpanProperty, Gridheight);
            if(direction > 0)
                image.SetValue(Grid.ColumnProperty, StartCol);
            else
                image.SetValue(Grid.ColumnProperty, EndCol);
            image.SetValue(Grid.ColumnSpanProperty, Gridwidth);
        }



        public bool IsPlayerOnIt(int row, int column) {
            /*  if (row == StartRow && column == StartCol)
                  return true;
              else
                  return false;*/
            return (row == StartRow && column == StartCol);
        }
    }
}
