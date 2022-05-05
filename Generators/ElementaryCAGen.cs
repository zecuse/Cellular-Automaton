//using CellularAutomaton.RuleSets;
//using System.Collections.Generic;
//using System.Windows.Media;

//namespace CellularAutomaton.Generators
//{
//    class ElementaryCAGen : Generator
//    {
//        private int lastGen = 0;

//        public ElementaryCAGen(int rule) : base()
//        {
//            //AllCells = new List<Cell>();
//            //rules = new ElementaryCARule(AllCells, rule);
//            //Start(0, width / 2);
//        }

//        public override List<SquareCell> GetNextGen<SquareCell>(SquareCell cell)
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void Start(params Cell[] cell)
//        {
//            throw new System.NotImplementedException();
//        }

//        public override void Update()
//        {
//            int val = 0;
//            int current = (lastGen + 1) % height;
//            for (int ii = 0; ii < width; ++ii)
//            {
//                val *= 2;
//                val += AllCells[lastGen, ii].Enabled ? 1 : 0;
//                val &= ~8;
//                if(ii >= 1)
//                {
//                    AllCells[current, ii - 1].Enabled = (rules as ElementaryCARule).Rule(val);
//                    AllCells[current, ii - 1].Background = AllCells[current, ii - 1].Enabled ? Cell.Black : Cell.White;
//                }
//            }
//            lastGen = current;
//        }
//    }
//}
