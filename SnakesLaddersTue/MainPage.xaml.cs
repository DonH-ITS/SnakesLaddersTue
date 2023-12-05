
using Microsoft.Maui.Controls.Shapes;

namespace SnakesLaddersTue
{
    public partial class MainPage : ContentPage
    {
        private Color BoardColour = Color.FromArgb("#2B0B98");
        private Random random;
        private Player player1;
        private bool dicerolling;
        private List<SnakeLadder> snakeLadderList;

        public bool Dicerolling
        {
            get => dicerolling;
            set
            {
                if (dicerolling == value) return;
                dicerolling = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NotDiceRolling));
            }
        }
        public bool NotDiceRolling => !Dicerolling;

        public MainPage() {
            InitializeComponent();
            InitialiseVariables();
            BindingContext = this;
        }

        public void InitialiseVariables()
        {
            CreatetheGrid();
            random = new Random();
            player1 = new Player(Player1Piece, "Donny", GameBoardGrid);
            dicerolling = false;
            CreateSnakesandLadders();
        }

        private void CreateSnakesandLadders() {
            snakeLadderList = new List<SnakeLadder>();
            SnakeLadder.grid = GameBoardGrid;

            snakeLadderList.Add(new SnakeLadder(5, 3, 5, 5));

            snakeLadderList.Add(new SnakeLadder(9, 8, 3, 6));

            snakeLadderList.Add(new SnakeLadder(5, 3, 9, 6));

            //Straight snakes
            snakeLadderList.Add(new SnakeLadder(0, 5, 2, 2));

            snakeLadderList.Add(new SnakeLadder(8, 9, 2, 2));

            //4x3 snakes
            snakeLadderList.Add(new SnakeLadder(0, 3, 4, 6));
            snakeLadderList.Add(new SnakeLadder(3, 6, 9, 7));

            //3x2 snakes
            snakeLadderList.Add(new SnakeLadder(5, 7, 4, 3));
            snakeLadderList.Add(new SnakeLadder(7, 9, 1, 2));

            //Diagonal snakes
            snakeLadderList.Add(new SnakeLadder(7, 8, 6, 5));
            snakeLadderList.Add(new SnakeLadder(0, 1, 1, 2));


        }

        private static int WhatNumber(int row, int col) {
            if( row % 2 == 0 ) {
                int start = 100 - row * 10;
                return start - col;
            }
            else
            {
                int start = (9 - row) * 10 + 1;
                return start + col;
            }
        }
        
        private static LayoutOptions WhatPosition(int row) {
            if (row % 2 == 0) return LayoutOptions.End;
            else return LayoutOptions.Start;
        }

        private void CreatetheGrid() {
            for (int i = 0; i < 10; ++i) {
                for (int j = 0; j < 10; ++j) {
                    Border border = new Border
                    {
                        StrokeThickness = 2,
                        Background = BoardColour,
                        Padding = new Thickness(3, 3),
                        HorizontalOptions = LayoutOptions.Fill,
                        StrokeShape = new RoundRectangle
                        {
                            CornerRadius = new CornerRadius(2, 2, 2, 2)
                        },
                        Stroke = new LinearGradientBrush
                        {
                            EndPoint = new Point(0, 1),
                            GradientStops = new GradientStopCollection
                            {
                                new GradientStop { Color = Colors.Orange, Offset = 0.1f },
                                new GradientStop { Color = Colors.Brown, Offset = 1.0f }
                            },
                        },
                        Content = new Label
                        {
                            Text = WhatNumber(i, j).ToString(),
                            TextColor = Colors.White,
                            FontSize = 10,
                            HorizontalOptions = WhatPosition(i),
                            FontAttributes = FontAttributes.Bold
                        }
                    };

                    GameBoardGrid.Add(border, j, i);
                }

            }
        }

        private async void DiceRollBtn_Clicked(object sender, EventArgs e) {
            if (Dicerolling)
                return;
            Dicerolling = true;
            await RollTheDice();
            Dicerolling = false;
        }

        private static Ellipse GetEllipse() {
            Ellipse ell = new Ellipse()
            {
                Fill = Color.FromRgb(0, 0, 0),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            return ell;
        }

        private async Task RollTheDice() {
            int roll = 0;
            int howmany = random.Next(4, 10);
            int lastthrow = 0;
            DiceGridBorder.TranslationX = 0;
            DiceGridBorder.TranslateTo(96, 0, 300*(uint)howmany);
            for (int i = 0; i < howmany; i++) {
                //DiceRollLbl.Text = roll.ToString();
                while(lastthrow == roll)
                    roll = random.Next(1, 7);
                lastthrow = roll;
                await DiceGridBorder.RotateYTo(DiceGridBorder.RotationY + 90, 150);
                CleartheDiceGrid(DiceGrid);
                FillinDiceGrid(roll, DiceGrid);
                await DiceGridBorder.RotateYTo(DiceGridBorder.RotationY + 90, 150);
            }
            await player1.MovePlayerCharacter(roll);

            int[] playerpos = player1.CurrentPosition;
            foreach(var boardpiece in snakeLadderList) {
                if (boardpiece.IsPlayerOnIt(playerpos[0], playerpos[1])) {
                    await player1.MovePlayerSnakeLadder(boardpiece.EndPosition[0], boardpiece.EndPosition[1]);
                }
            }
        }

        private static void CleartheDiceGrid(Grid grid) {
            List<View> childrenToRemove = new();
            foreach (var item in grid.Children) {
                if (item.GetType() == typeof(Ellipse)) {
                    childrenToRemove.Add((Ellipse)item);
                }
            }

            //Actually remove them from the Grid
            foreach (var item in childrenToRemove) {
                grid.Remove(item);
            }
        }

        private static void FillinDiceGrid(int roll, Grid grid) {
            switch (roll) {
                case 1:
                    grid.Add(GetEllipse(), 1, 1);
                    break;
                case 2:
                    grid.Add(GetEllipse(), 0, 0);
                    grid.Add(GetEllipse(), 2, 2);
                    break;
                case 3:
                    for (int i = 0; i < 3; i++) {
                        grid.Add(GetEllipse(), i, i);
                    }
                    break;
                case 4:
                    for (int j = 0; j < 3; j += 2) {
                        for (int k = 0; k < 3; k += 2) {
                            grid.Add(GetEllipse(), j, k);
                        }
                    }
                    break;
                case 5:
                    for (int j = 0; j < 3; j += 2) {
                        for (int k = 0; k < 3; k += 2) {
                            grid.Add(GetEllipse(), j, k);
                        }
                    }
                    grid.Add(GetEllipse(), 1, 1);
                    break;
                case 6:
                    for (int j = 0; j < 3; j += 2) {
                        for (int k = 0; k < 3; ++k) {
                            grid.Add(GetEllipse(), k, j);
                        }
                    }
                    break;
            }
        }

    }
}