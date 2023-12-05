using Microsoft.Maui.Controls.Shapes;

namespace SnakesLaddersTue
{
    public partial class MainPage : ContentPage
    {
        private Color BoardColour = Color.FromArgb("#2B0B98");
        private Random random;
        private Player player1;
        private bool dicerolling;

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

        private Ellipse GetEllipse() {
            Ellipse ell = new Ellipse()
            {
                Fill = Color.FromRgb(0, 0, 0),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };
            return ell;
        }

        private async Task RollTheDice() {
            int roll = random.Next(1, 7);
            //DiceRollLbl.Text = roll.ToString();
            FillinDiceGrid(roll, DiceGrid);
            await player1.MovePlayerCharacter(roll);
        }

        private void FillinDiceGrid(int roll, Grid grid) {
            switch (roll) {
                case 1:
                    grid.Add(GetEllipse(), 1, 1);
                    break;
                case 2:
                    grid.Add(GetEllipse(), 0, 0);
                    grid.Add(GetEllipse(), 2, 2);
                    break;
                case 3:
                    for(int i=0; i < 3; i++) {
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