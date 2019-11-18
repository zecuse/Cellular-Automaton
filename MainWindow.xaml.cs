using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CellularAutomaton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public bool Paused
        {
            get; set;
        } = true;

        public CellButton[,] GridCells
        {
            get; set;
        }

        public SolidColorBrush White
        {
            get; set;
        } = new SolidColorBrush(Colors.White);

        public SolidColorBrush Black
        {
            get; set;
        } = new SolidColorBrush(Colors.Black);

        private const int width = 11;//151;

        private const int height = 11;//75;

        private Queue<CellButton> active;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            PopulateGrid();
            active = new Queue<CellButton>(width * height);
            GridCells[height / 2, width / 2].Enabled = true;
            active.Enqueue(GridCells[height / 2, width / 2]);
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate {};

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private void PopulateGrid()
        {
            GridCells = new CellButton[height, width];

            for (int ii = 0; ii < height; ++ii)
            {
                RowDefinition row = new RowDefinition();
                Grid.RowDefinitions.Add(row);
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                Grid.SetRow(panel, ii);
                Grid.Children.Add(panel);

                for (int jj = 0; jj < width; ++jj)
                {
                    CellButton cell = new CellButton
                    {
                        Name = "Cell",
                        Style = (Style)Resources["Cell"],
                        Background = White,
                        Row = ii,
                        Col = jj,
                        Enabled = false,
                        Neighbors = new List<CellButton>()
                    };
                    cell.Click += Toggle;
                    AddNeighbors(cell, ii, jj);
                    panel.Children.Add(cell);
                    GridCells[ii, jj] = cell;
                }
            }
        }

        private void AddNeighbors(CellButton cell, int row, int col)
        {
            for (int ii = row; ii >= row - 1; --ii)
            {
                for (int jj = col; jj >= col - 1; --jj)
                {
                    if (ii == row && jj == col)
                    {
                        continue;
                    }
                    if (ii >= 0 && jj >= 0) 
                    {
                        cell.Neighbors.Add(GridCells[ii, jj]);
                        GridCells[ii, jj].Neighbors.Add(cell);
                    }
                }
            }
        }

        private void Toggle(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Name.Equals("PlayPause"))
            {
                Paused = !Paused;
                OnPropertyChanged("Paused");
            }
            else
            {
                CellButton cell = button as CellButton;
                GridCells[cell.Row, cell.Col].Enabled = !GridCells[cell.Row, cell.Col].Enabled;
                button.Background = GridCells[cell.Row, cell.Col].Enabled ? Black : White;
            }
        }

        private void Step(object sender, RoutedEventArgs e)
        {
            GridCells[0, 0].Enabled = !GridCells[0, 0].Enabled;
        }
    }
}
