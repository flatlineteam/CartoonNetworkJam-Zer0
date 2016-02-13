using System.Collections;
using Prime31.StateKit;
using UnityEngine;

namespace Assets.Scripts.GameFlowStates
{
    public class StartingUp : SKState<GameFlowController>
    {
        [Range(0, 5)]
        public float WaitTime = 2;
        
        private bool finished;

        public override void begin()
        {
            finished = false;
            _context.StartCoroutine(Delay());
        }

        public IEnumerator Delay()
        {
            yield return new WaitForSeconds(WaitTime);
            finished = true;
        }

        public override void reason()
        {
            if (finished)
            {
                _machine.changeState<ShowingCellphone>();
            }
        }

        public override void update(float deltaTime)
        {
        }
    }
}