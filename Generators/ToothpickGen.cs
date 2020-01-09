using CellularAutomaton.RuleSets;
using System.Collections.Generic;

namespace CellularAutomaton.Generators
{
    class ToothpickGen : Generator
    {
        public ToothpickGen(int width, int height) : base(width, height)
        {
            rules = new ToothpickRules(GridCells);
        }

        public override void Update()
        {
            int count = active.Count;
            while (count > 0)
            {
                CellButton cell = active.Dequeue();
                var list = GetNextGen(cell);
                list.ForEach(c => c.Enabled = true);
                list.ForEach(active.Enqueue);
                --count;
            }
        }

        public override List<CellButton> GetNextGen(CellButton cell)
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
                    CellButton next = GridCells[ii, jj];
                    if(rules.Pass(cell, next))
                    {
                        neighbors.Add(next);
                    }
                }
            }

            return neighbors;
        }
    }
}
