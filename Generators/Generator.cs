using CellularAutomaton.RuleSets;
using System.Collections.Generic;

namespace CellularAutomaton.Generators
{
    abstract class Generator
    {
        public CellButton[,] GridCells
        {
            get; set;
        }

        public Queue<CellButton> active;

        protected Rules rules;

        protected int width;

        protected int height;

        public abstract void Update();

        public abstract List<CellButton> GetNextGen(CellButton cell);

        public Generator(int width, int height)
        {
            this.width = width;
            this.height = height;
            GridCells = new CellButton[width, height];
            active = new Queue<CellButton>(width * height);
        }

        public void Toggle(int ii, int jj)
        {
            GridCells[ii, jj].Enabled = true;
            active.Enqueue(GridCells[ii, jj]);
        }
    }
}
