using CellularAutomaton.RuleSets;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Media;

namespace CellularAutomaton.Generators
{
    class ElementaryCAGen : Generator
    {
        private int lastGen = 0;

        private int[] val = new int[1];

        private BitArray bits = new BitArray(3);

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
            int current = (lastGen + 1) % height;
            for (int ii = 1; ii < width - 1; ++ii)
            {
                for (int jj = -1; jj <= 1; ++jj)
                {
                    bits[jj + 1] = GridCells[lastGen, ii + jj].Enabled;
                }
                bits.CopyTo(val, 0);
                GridCells[current, ii].Enabled = (rules as ElementaryCARule).Rule(val[0]);
                GridCells[current, ii].Background = GridCells[current, ii].Enabled ? Cell.Black : Cell.White;
            }
            lastGen = current;
        }
    }
}
