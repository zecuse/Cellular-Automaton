using CellularAutomaton.Cells;
using CellularAutomaton.RuleSets;
using System.Collections.Generic;

namespace CellularAutomaton.Generators
{
    //This doesn't work exactly the way I imagine it should.
    class FractalLifeGen : Generator
    {
        private int lifespan = 6;

        public FractalLifeGen() : base()
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
                    SquareLivingCell next;
                    if (AllCells.TryGetValue((cur.index.x + ii, cur.index.y + jj), out found))
                    {
                        next = found as SquareLivingCell;
                    }
                    else
                    {
                        next = new SquareLivingCell(cur.center.x + ii * Constants.SquareSize, cur.center.y + jj * Constants.SquareSize);
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
                SquareLivingCell cell = active.Dequeue() as SquareLivingCell;
                if (cell.Life == lifespan)
                {
                    GetNextGen(cell).ForEach(c => Activate(c));
                }
                cell.Update(lifespan);
                if (cell.Enabled)
                {
                    active.Enqueue(cell);
                }
                else
                {
                    Deactivate(cell);
                }
                --count;
            }
        }

        protected override void Start()
        {
            Activate(new SquareLivingCell
            {
                center = new Cell.Center(0, 0),
                index = (0, 0)
            });
        }
    }
}
