using Prime31.StateKit;

namespace Assets.Scripts.GameFlowStates
{
    public class DecidingNextMinigame : SKState<GameFlowController>
    {

        public override void begin()
        {
            if (_context.NumCompleted == _context.NumToCompleteThenFinale)
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