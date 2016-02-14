using Prime31.StateKit;
using UnityEngine;

namespace Assets.Scripts.GameFlowStates
{
    public class MinigameFinished : SKState<GameFlowController>
    {
        public override void begin()
        {
            _context.GetComponent<AudioSource>().Pause();
        }
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