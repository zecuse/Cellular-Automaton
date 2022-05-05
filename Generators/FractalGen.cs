using CellularAutomaton.Cells;
using CellularAutomaton.RuleSets;
using System.Collections.Generic;

namespace CellularAutomaton.Generators
{
    class FractalGen : Generator
    {
        public FractalGen() : base()
        {
            rules = new FractalRules(AllCells);
            Start();
        }

        public override List<Cell> GetNextGen(Cell cur)
        {
            neighbors.Clear();
            for (int ii = -1; ii <= 1; ++ii)
            {
                for (int jj = -1; jj <= 1; ++jj)
                {
                    SquareCell next;
                    if (AllCells.TryGetValue((cur.index.x + ii, cur.index.y + jj), out found))
                    {
                        next = found as SquareCell;
                    }
                    else
                    {
                        next = new SquareCell(cur.center.x + ii * Constants.SquareSize, cur.center.y + jj * Constants.SquareSize);
                    }
                    next.index = (cur.index.x + ii, cur.index.y + jj);
                    if (rules.Pass(cur, next))
                    {
                        neighbors.Add(next);
                    }
                }
            }

            return neighbors;
        }

        public override void Update()
        {
            int count = active.Count;
            while (count > 0)
            {
                Cell cell = active.Dequeue();
                GetNextGen(cell).ForEach(c => Activate(c));
                cell.Update();
                --count;
            }
        }

        protected override void Start()
        {
            Activate(new SquareCell
            {
                center = new Cell.Center(0, 0),
                index = (0, 0)
            });
        }
    }
}
