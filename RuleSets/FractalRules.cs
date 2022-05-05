using CellularAutomaton.Cells;
using System;
using System.Collections.Generic;

namespace CellularAutomaton.RuleSets
{
    class FractalRules : Rules
    {
        public FractalRules(Dictionary<(int x, int y), Cell> cells) : base(cells)
        {
            ruleset = new Func<Cell, Cell, bool>[]
            {
                rule1,
                rule2
            };
        }

        private bool rule1(Cell cur, Cell next)
        {
            return !next.Enabled && !(Math.Abs(next.center.x - cur.center.x) > 1e-5 && Math.Abs(next.center.y - cur.center.y) > 1e-5);
        }

        private bool rule2(Cell cur, Cell next)
        {
            for (int ii = -1; ii <= 1; ++ii)
            {
                for (int jj = -1; jj <= 1; ++jj)
                {
                    if ((ii != 0 && jj != 0) || (ii == 0 && jj == 0))
                    {
                        continue;
                    }

                    checker.center.x = next.center.x + ii * Constants.SquareSize;
                    checker.center.y = next.center.y + jj * Constants.SquareSize;
                    if (world.TryGetValue((next.index.x + ii, next.index.y + jj), out found) && !found.Equals(cur))
                    {
                        if (found.Enabled)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}
