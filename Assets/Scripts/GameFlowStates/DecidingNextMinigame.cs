using Prime31.StateKit;

namespace Assets.Scripts.GameFlowStates
{
    public class DecidingNextMinigame : SKState<GameFlowController>
    {
        private readonly int numCompleteThenFinale;

        public DecidingNextMinigame(int numCompleteThenFinale)
        {
            this.numCompleteThenFinale = numCompleteThenFinale;
        }

        public override void begin()
        {
            if (_context.NumCompleted == numCompleteThenFinale)
            {
                MinigameController.Current.SetFinale();
            }
            else
            {
                MinigameController.Current.SetNextMinigame();
            }
        }

        public override void reason()
        {
            _machine.changeState<ShowingCellphone>();
        }

        public override void update(float deltaTime)
        {
            
        }
    }
}