using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CellularAutomaton.Cells
{
    class Cell : IEquatable<Cell>
    {
        public struct Center
        {
            public double x;
            public double y;

            public Center(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public Center center;

        public (int x, int y) index;

        public static SolidColorBrush Black = new SolidColorBrush(Colors.Black);

        public static SolidColorBrush White = new SolidColorBrush(Colors.White);

        public static SolidColorBrush Blue = new SolidColorBrush(Colors.Blue);

        public bool Enabled
        {
            get; set;
        } = false;

        public bool Redraw
        {
            get; set;
        } = true;

        public override bool Equals(object obj)
        {
            return obj is Cell && Equals(obj as Cell);
        }

        public virtual Shape Draw()
        {
            return new Rectangle();
        }

        public virtual void Update()
        {
            return;
        }

        public bool Equals(Cell other)
        {
            return index.x == other.index.x && index.y == other.index.y;
        }
    }
}
