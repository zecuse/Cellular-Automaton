using System;

namespace CellularAutomaton.RuleSets
{
    class Rules
    {
        protected Cell[,] grid;

        protected Func<Cell, Cell, bool>[] ruleset;

        public Rules(Cell[,] grid)
        {
            this.grid = grid;
        }

        public bool Pass(Cell cur, Cell next)
        {
            foreach (var rule in ruleset)
            {
                if (!rule(cur, next))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
