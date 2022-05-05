using CellularAutomaton.Cells;
using CellularAutomaton.Generators;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace CellularAutomaton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public bool Paused
        {
            get; set;
        } = false;

        private Generator generate;

        private const int timeStep = 250;

        private double scale = 1.0;

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
            generate = new FractalLifeGen();
            Update();
        }

        public async void Update()
        {
            while (true)
            {
                if (!Paused)
                {
                    Draw(generate.active);
                    generate.Update();
                }
                await Task.Delay(TimeSpan.FromMilliseconds(timeStep));
            }
        }

        private void Draw(IEnumerable<Cell> cells, bool redrawall = false)
        {
            foreach (Cell c in cells)
            {
                if (redrawall || c.Redraw)
                {
                    Shape shape = c.Draw();
                    Canvas.SetLeft(shape, c.center.x + Canvas.ActualWidth / 2);
                    Canvas.SetTop(shape, c.center.y + Canvas.ActualHeight / 2);
                    Canvas.Children.Add(shape);
                    c.Redraw = false;
                }
            }
        }

        private void Redraw(object sender, System.Windows.SizeChangedEventArgs e)
        {
            Canvas.Children.Clear();
            Draw(generate.AllCells.Values, true);
        }
    }
}
