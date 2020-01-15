using CellularAutomaton.RuleSets;
using System.Collections.Generic;
using System.Windows.Media;

namespace CellularAutomaton.Generators
{
    class ElementaryCAGen : Generator
    {
        private int lastGen = 0;

        public ElementaryCAGen(int height, int width, int rule) : base(height, width)
        {
            GridCells = new Cell[height, width];
            for (int ii = 0; ii < height; ++ii)
            {
                for (int jj = 0; jj < width; ++jj)
                {
                    GridCells[ii, jj] = new Cell
                    {
                        Name = "Cell",
                        Background = new SolidColorBrush(Colors.White),
                        Row = ii,
                        Col = jj,
                        Enabled = false
                    };
                }
            }
            rules = new ElementaryCARule(GridCells, rule);
            Force(0, width / 2);
        }

        public override List<Cell> GetNextGen(Cell cell)
        {
            throw new System.NotImplementedException();
        }

        public override void Update()
        {
            int val = 0;
            int current = (lastGen + 1) % height;
            for (int ii = 0; ii < width; ++ii)
            {
                val *= 2;
                val += GridCells[lastGen, ii].Enabled ? 1 : 0;
                val &= ~8;
                if(ii >= 1)
                {
                    GridCells[current, ii - 1].Enabled = (rules as ElementaryCARule).Rule(val);
                    GridCells[current, ii - 1].Background = GridCells[current, ii - 1].Enabled ? Cell.Black : Cell.White;
                }
            }
            lastGen = current;
        }
    }
}
