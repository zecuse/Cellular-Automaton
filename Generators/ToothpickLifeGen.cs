using CellularAutomaton.RuleSets;
using System.Collections.Generic;

namespace CellularAutomaton.Generators
{
    class ToothpickLifeGen : Generator
    {
        public ToothpickLifeGen(int width, int height) : base(width, height)
        {
            rules = new ToothpickRules(GridCells);
        }

        public override void Update()
        {
            int count = active.Count;
            while (count > 0)
            {
                CellButton cell = active.Dequeue();
                if(cell.Life == 12)
                {
                    GetNextGen(cell).ForEach(c =>
                    {
                        c.Enabled = true;
                        active.Enqueue(c);
                    });
                }
                cell.Update(12);
                if(cell.Enabled)
                {
                    active.Enqueue(cell);
                }
                --count;
            }
        }

        public override List<CellButton> GetNextGen(CellButton cell)
        {
            List<CellButton> neighbors = new List<CellButton>();

            for (int ii = cell.Row - 1; ii <= cell.Row + 1; ++ii)
            {
                if (ii < 0 || ii >= width)
                {
                    continue;
                }
                for (int jj = cell.Col - 1; jj <= cell.Col + 1; ++jj)
                {
                    if (jj < 0 || jj >= height)
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
