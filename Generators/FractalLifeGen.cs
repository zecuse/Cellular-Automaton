﻿using CellularAutomaton.Cells;
using CellularAutomaton.RuleSets;
using System.Collections.Generic;
using System.Windows.Media;

namespace CellularAutomaton.Generators
{
    class FractalLifeGen : Generator
    {
        private int lifespan = 12;

        public FractalLifeGen(int height, int width) : base(height, width)
        {
            GridCells = new LivingCell[height, width];
            for (int ii = 0; ii < height; ++ii)
            {
                for (int jj = 0; jj < width; ++jj)
                {
                    GridCells[ii, jj] = new LivingCell
                    {
                        Name = "Cell",
                        Background = new SolidColorBrush(Colors.White),
                        Row = ii,
                        Col = jj,
                        Enabled = false,
                        Life = 0
                    };
                }
            }
            rules = new ToothpickRules(GridCells);
            Force(height / 2, width / 2);
        }

        public override void Update()
        {
            int count = active.Count;
            while (count > 0)
            {
                LivingCell cell = active.Dequeue() as LivingCell;
                if (cell.Life == lifespan)
                {
                    GetNextGen(cell).ForEach(c =>
                    {
                        c.Enabled = true;
                        active.Enqueue(c);
                    });
                }
                cell.Update(lifespan);
                if (cell.Enabled)
                {
                    active.Enqueue(cell);
                }
                --count;
            }
        }

        public override List<Cell> GetNextGen(Cell cell)
        {
            List<Cell> neighbors = new List<Cell>();

            for (int ii = cell.Row - 1; ii <= cell.Row + 1; ++ii)
            {
                if (ii < 0 || ii >= height)
                {
                    continue;
                }
                for (int jj = cell.Col - 1; jj <= cell.Col + 1; ++jj)
                {
                    if (jj < 0 || jj >= width)
                    {
                        continue;
                    }
                    Cell next = GridCells[ii, jj];
                    if (rules.Pass(cell, next))
                    {
                        neighbors.Add(next);
                    }
                }
            }

            return neighbors;
        }
    }
}
