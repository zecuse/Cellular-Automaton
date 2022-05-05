namespace CellularAutomaton.Cells
{
    abstract class LivingCell : Cell
    {
        public int Life
        {
            get; set;
        } = 0;

        public override void Update()
        {
            Update(-1);
        }

        public virtual void Update(int lifespan = 0)
        {
            --Life;
            if (Life < 0)
            {
                Life = lifespan;
            }
            else if (Life == 0)
            {
                Enabled = false;
            }
        }
    }
}
