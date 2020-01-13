using System.Collections;

namespace CellularAutomaton.RuleSets
{
    class ElementaryCARule : Rules
    {
        private BitArray rule;

        public ElementaryCARule(Cell[,] grid, int rule) : base(grid)
        {
            this.rule = new BitArray(new byte[] { (byte)rule });
        }

        public bool Rule(int bit)
        {
            return rule[bit];
        }
    }
}
