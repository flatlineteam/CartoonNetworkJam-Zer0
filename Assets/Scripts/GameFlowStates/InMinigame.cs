using Prime31.StateKit;

namespace Assets.Scripts.GameFlowStates
{
    public class InMinigame : SKState<GameFlowController>
    {
        public override void begin()
        {
            MinigameController.Current.StartMinigame();
        }

        public override void update(float deltaTime)
        {
            
        }
    }
}