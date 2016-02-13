using Prime31.StateKit;

namespace Assets.Scripts.GameFlowStates
{
    public class SpeedingUp : SKState<GameFlowController>
    {
        public override void begin()
        {
            //TODO
            _machine.changeState<DecidingNextMinigame>();
        }

        public override void update(float deltaTime)
        {
            
        }
    }
}