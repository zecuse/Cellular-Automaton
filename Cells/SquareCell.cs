using System.Windows.Shapes;

namespace CellularAutomaton.Cells
{
    class SquareCell : Cell
    {
        public SquareCell(double x = 0, double y = 0)
        {
            center = new Center(x, y);
        }

        public override Shape Draw()
        {
            return new Rectangle()
            {
                Fill = Black,
                Width = Constants.SquareSize,
                Height = Constants.SquareSize,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            };
        }
    }
}
