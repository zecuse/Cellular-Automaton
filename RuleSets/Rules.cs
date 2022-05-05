using CellularAutomaton.Cells;
using CellularAutomaton.Generators;
using System;
using System.Collections.Generic;

namespace CellularAutomaton.RuleSets
{
    abstract class Rules
    {
        protected Dictionary<(int x, int y), Cell> world;

        protected Func<Cell, Cell, bool>[] ruleset;

        protected Cell checker, found;

        public Rules(Dictionary<(int x, int y), Cell> cells)
        {
            world = cells;
            checker = new Cell();
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
