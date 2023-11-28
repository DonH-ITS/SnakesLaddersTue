using Microsoft.Maui.Controls.Shapes;

namespace SnakesLaddersTue
{
    public partial class MainPage : ContentPage
    {
        private Color BoardColour = Color.FromArgb("#2B0B98");
        private Random random;

        public MainPage() {
            InitializeComponent();
            CreatetheGrid();
            random = new Random();
        }

        private int WhatNumber(int row, int col) {
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
        
        private LayoutOptions WhatPosition(int row) {
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

        private void DiceRollBtn_Clicked(object sender, EventArgs e) {
            int roll = random.Next(1, 7);
            DiceRollLbl.Text = roll.ToString();
        }
    }
}