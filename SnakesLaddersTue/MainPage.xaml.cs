using Microsoft.Maui.Controls.Shapes;

namespace SnakesLaddersTue
{
    public partial class MainPage : ContentPage
    {
        private Color BoardColour = Color.FromArgb("#2B0B98");
        public MainPage() {
            InitializeComponent();
            CreatetheGrid();
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
                            Text = "1",
                            TextColor = Colors.White,
                            FontSize = 18,
                            FontAttributes = FontAttributes.Bold
                        }
                    };

                    GameBoardGrid.Add(border, j, i);
                }

            }
        }

    }
}