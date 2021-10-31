using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace testingplswork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int tHeight = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtTriangle.Text, out tHeight))
            {
                if (tHeight <= 1)
                {
                    MessageBox.Show("Please make sure that the number is greater than 1.");
                    txtTriangle.Text = "";
                }
                else
                {
                    txtTriangle.IsEnabled = false;
                    btnGenerate.IsEnabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please make sure that the input is numeric.");
                txtTriangle.Text = "";
            }

            // generate pascal's triangle
            display(generateTriangle(tHeight));
        }

        public void display(int[][] pTriangle)
        {
            int startHeight = 0;
            int halfWidth = 0;
            // declare the label counterpart of pTriangle
            // pTriangle contains the generated values of Pascal's Triangle
            Label[][] lPTriangle = new Label[pTriangle.Length][];
            int[][,] lPTCoords = new int[][,] { };

            for (int x = 0; x < lPTriangle.Length; x++)
                lPTriangle[x] = new Label[pTriangle[x].Length];

            startHeight = 40;
            halfWidth = 800 / 2;

            lPTCoords = calculateCoordinates(pTriangle, startHeight, halfWidth);

            for (int x = 0; x < lPTriangle.Length; x++)
            {
                for (int y = 0; y < lPTriangle[x].Length; y++)
                {
                    Label temp = new Label();
                    temp.Content = pTriangle[x][y];
                    temp.FontFamily = new FontFamily("Courier New");
                    temp.FontSize = 12;
                    temp.Height = 20;
                    temp.Width = 50;
                    temp.Margin = new Thickness(lPTCoords[x][y, 0], lPTCoords[x][y, 1], 0, 0);
                    temp.HorizontalContentAlignment = HorizontalAlignment.Center;
                    temp.VerticalAlignment = VerticalAlignment.Top;
                    temp.HorizontalAlignment = HorizontalAlignment.Left;
                    lPTriangle[x][y] = temp;
                    displayGrid.Children.Add(lPTriangle[x][y]);
                }
            }
        }

        public static int[][] generateTriangle(int height)
        {
            int[][] triangle = new int[height][];

            for (int i = 0; i < triangle.Length; i++)
            {
                triangle[i] = new int[1 + i];
            }

            triangle[0][0] = 1;

            for (int i = 0; i < height - 1; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    triangle[i + 1][j] += triangle[i][j];
                    triangle[i + 1][j + 1] += triangle[i][j];
                }
            }

            return triangle;
        }

        public int[][,] calculateCoordinates(int[][] pTriangle, int startHeight, int halfWidth)
        {
            int[][,] coordinates = new int[tHeight][,];
            int tempw = 60;
            int temph = 22;

            for(int i = 0; i < pTriangle.Length; i++)
            {
                coordinates[i] = new int[i + 1, 2];
                int temp = ((i + 1) * 50) + (i * 10);
                startHeight += temph;
                halfWidth = 425;
                halfWidth -= temp/2;
                for (int j = 0; j < pTriangle[i].Length; j++)
                {
                    coordinates[i][j, 0] = halfWidth;
                    coordinates[i][j, 1] = startHeight;
                    halfWidth += tempw;
                }
            }

            return coordinates;
        }
    }
}

