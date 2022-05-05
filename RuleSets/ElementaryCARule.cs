//namespace CellularAutomaton.RuleSets
//{
//    class ElementaryCARule : Rules
//    {
//        private bool[] bits;

//        public ElementaryCARule(Cell[,] grid, int rule) : base(grid)
//        {
//            bits = new bool[8];
//            for (int ii = 0; ii < 8; ++ii)
//            {
//                bits[ii] = rule % 2 == 1;
//                rule /= 2;
//            }
//        }

//        public bool Rule(int bit)
//        {
//            return bits[bit];
//        }
//    }
//}
