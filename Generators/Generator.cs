using CellularAutomaton.Cells;
using CellularAutomaton.RuleSets;
using System.Collections.Generic;

namespace CellularAutomaton.Generators
{
    abstract class Generator
    {
        public Dictionary<(int x, int y), Cell> AllCells
        {
            get; set;
        }

        public Queue<Cell> active;

        protected List<Cell> neighbors;

        protected Rules rules;

        protected Cell found;

        private int len = 1000;

        public Generator()
        {
            AllCells = new Dictionary<(int x, int y), Cell>();
            active = new Queue<Cell>(len);
            neighbors = new List<Cell>();
        }

        public abstract List<Cell> GetNextGen(Cell cur);

        public abstract void Update();

        protected abstract void Start();

        protected virtual void Activate(Cell cell)
        {
            if (!AllCells.TryGetValue(cell.index, out Cell found))
            {
                AllCells.Add((cell.index.x, cell.index.y), cell);
            }
            active.Enqueue(cell);
            cell.Enabled = true;
        }

        protected virtual void Deactivate(Cell cell)
        {
            cell.Redraw = true;
        }
    }
}
