using CellularAutomaton.RuleSets;
using System.Collections.Generic;

namespace CellularAutomaton.Generators
{
    abstract class Generator
    {
        public Cell[,] GridCells
        {
            get; set;
        }

        public Queue<Cell> active;

        protected Rules rules;

        protected int height;

        protected int width;

        public Generator(int height, int width)
        {
            this.height = height;
            this.width = width;
            active = new Queue<Cell>(height * width);
        }

        public abstract void Update();

        public abstract List<Cell> GetNextGen(Cell cell);

        public void Force(int ii, int jj)
        {
            if (!active.Contains(GridCells[ii, jj]))
            {
                GridCells[ii, jj].Enabled = true;
                active.Enqueue(GridCells[ii, jj]);
            }
        }
    }
}
