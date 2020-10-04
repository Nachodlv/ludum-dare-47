namespace Entities.Enemy.Ai.States
{
    public class ResetState: IState
    {
        
        private readonly Racer _racer;

        public ResetState(Racer racer)
        {
            _racer = racer;
        }
        
        public void Tick()
        {
        }

        public void FixedTick()
        {
        }

        public void OnEnter()
        {
            // Reset
            _racer.SpawnInLastCheckpoint();
        }

        public void OnExit()
        {
        }
    }
}
