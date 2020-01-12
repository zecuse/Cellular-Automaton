using CellularAutomaton.Generators;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
        } = false;

        private Generator generate;

        private const int width = 151;

        private const int height = 75;

        private const int timeStep = 100;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            generate = new ToothpickLifeGen(height, width);
            PopulateGrid();
            generate.Force(height / 2, width / 2);
            Update();
        }

        public async void Update()
        {
            while (true)
            {
                if (!Paused)
                {
                    generate.Update();
                }
                await Task.Delay(TimeSpan.FromMilliseconds(timeStep / StepRate.Value));
            }
        }

        private void PopulateGrid()
        {
            for (int ii = 0; ii < height; ++ii)
            {
                Grid.RowDefinitions.Add(new RowDefinition());
                StackPanel panel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };
                Grid.SetRow(panel, ii);
                Grid.Children.Add(panel);

                for (int jj = 0; jj < width; ++jj)
                {
                    generate.GridCells[ii, jj].Style = (Style)Resources["Cell"];
                    generate.GridCells[ii, jj].Click += Toggle;
                    panel.Children.Add(generate.GridCells[ii, jj]);
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
                Cell cell = button as Cell;
                generate.Force(cell.Row, cell.Col);
            }
        }

        private void Step(object sender, RoutedEventArgs e)
        {
            generate.Update();
        }
    }
}
