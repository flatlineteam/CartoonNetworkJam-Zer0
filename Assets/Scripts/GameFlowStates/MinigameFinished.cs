using Prime31.StateKit;

namespace Assets.Scripts.GameFlowStates
{
    public class MinigameFinished : SKState<GameFlowController>
    {
        public override void update(float deltaTime)
        {
        }

        public void MinigameIsCompletelyFinished()
        {
            if (_context.NumCompleted == _context.NumToCompleteThenFinale + 1)
            {
                _context.ResetNumCompleted();
                _machine.changeState<SpeedingUp>();
            }
            else
            {
                _machine.changeState<DecidingNextMinigame>();
            }
        }
    }
}