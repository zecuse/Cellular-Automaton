using System;

namespace CellularAutomaton.RuleSets
{
    class ToothpickRules : Rules
    {
        public ToothpickRules(Cell[,] grid) : base(grid)
        {
            ruleset = new Func<Cell, Cell, bool>[]
            {
                rule1,
                rule2
            };
        }

        private bool rule1(Cell cur, Cell next)
        {
            return !next.Enabled && !(next.Row != cur.Row && next.Col != cur.Col) && !next.Equals(cur);
        }

        private bool rule2(Cell cur, Cell next)
        {
            for (int ii = next.Row - 1; ii <= next.Row + 1; ++ii)
            {
                if (ii < 0 || ii >= grid.GetLength(0))
                {
                    continue;
                }
                for (int jj = next.Col - 1; jj <= next.Col + 1; ++jj)
                {
                    if (jj < 0 || jj >= grid.GetLength(1) || grid[ii, jj].Equals(cur) || grid[ii, jj].Equals(next) || (ii != next.Row && jj != next.Col))
                    {
                        continue;
                    }
                    if (grid[ii, jj].Enabled)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
