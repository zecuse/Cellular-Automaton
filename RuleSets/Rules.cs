using System;

namespace CellularAutomaton.RuleSets
{
    class Rules
    {
        protected CellButton[,] grid;

        protected Func<CellButton, CellButton, bool>[] ruleset;

        public Rules(CellButton[,] grid)
        {
            this.grid = grid;
        }

        public bool Pass(CellButton cur, CellButton next)
        {
            foreach (var rule in ruleset)
            {
                if(!rule(cur, next))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
