﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        private const int width = 151;

        private const int height = 75;

        private const int timeStep = 1000;

        private Queue<CellButton> active;

        public event PropertyChangedEventHandler PropertyChanged = delegate {};

        protected void OnPropertyChanged(string property)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            PopulateGrid();
            active = new Queue<CellButton>(width * height);
            GridCells[height / 2, width / 2].Enabled = true;
            active.Enqueue(GridCells[height / 2, width / 2]);
            Update();
        }

        public async void Update()
        {
            while (true)
            {
                if(!Paused)
                {
                    Step();
                }
                await Task.Delay(TimeSpan.FromMilliseconds(timeStep / StepRate.Value));
            }
        }

        private void PopulateGrid()
        {
            GridCells = new CellButton[height, width];

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
                    CellButton cellButton = new CellButton
                    {
                        Name = "Cell",
                        Style = (Style)Resources["Cell"],
                        Background = new SolidColorBrush(Colors.White),
                        Row = ii,
                        Col = jj,
                        Enabled = false
                    };
                    cellButton.Click += Toggle;
                    panel.Children.Add(cellButton);
                    GridCells[ii, jj] = cellButton;
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
                active.Enqueue(cell);
            }
        }

        private void Step(object sender, RoutedEventArgs e)
        {
            Step();
        }

        private void Step()
        {
            int count = active.Count;
            while (count > 0)
            {
                CellButton cell = active.Dequeue();
                var list = GetNeighbors(cell);
                list.ForEach(c => c.Enabled = true);
                list.ForEach(active.Enqueue);
                --count;
            }
        }

        private List<CellButton> GetNeighbors(CellButton cell)
        {
            List<CellButton> neighbors = new List<CellButton>();

            for (int ii = cell.Row - 1; ii <= cell.Row + 1; ++ii)
            {
                if (ii < 0 || ii >= height)
                {
                    continue;
                }
                for (int jj = cell.Col - 1; jj <= cell.Col + 1; ++jj)
                {
                    if (jj < 0 || jj >= width)
                    {
                        continue;
                    }
                    if (!GridCells[ii, jj].Enabled && ((ii == cell.Row && jj != cell.Col) || (ii != cell.Row && jj == cell.Col)))
                    {
                        CellButton next = GridCells[ii, jj];
                        if (CheckNeighbors(cell, next))
                        {
                            neighbors.Add(next);
                        }
                    }
                }
            }

            return neighbors;
        }

        private bool CheckNeighbors(CellButton prev, CellButton next)
        {
            for (int ii = next.Row - 1; ii <= next.Row + 1; ++ii)
            {
                if (ii < 0 || ii >= height)
                {
                    continue;
                }
                for (int jj = next.Col - 1; jj <= next.Col + 1; ++jj)
                {
                    if (jj < 0 || jj >= width || GridCells[ii, jj].Equals(prev) || (ii != next.Row && jj != next.Col))
                    {
                        continue;
                    }
                    if (GridCells[ii, jj].Enabled)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
