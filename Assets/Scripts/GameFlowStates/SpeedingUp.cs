using System.Collections;
using Prime31.StateKit;
using UnityEngine;

namespace Assets.Scripts.GameFlowStates
{
    public class SpeedingUp : SKState<GameFlowController>
    {
        private GameObject speedingUpInstance;

        public override void begin()
        {
            speedingUpInstance = Object.Instantiate(_context.SpeedingUpTextPrefab.gameObject);
            speedingUpInstance.transform.SetParent(_context.MainCanvas.transform, false);

            _context.StartCoroutine(Sequence());
        }

        private IEnumerator Sequence()
        {
            yield return speedingUpInstance.GetComponent<SpeedingUpText>().Sequence();

            _context.SpeedUpBy(0.1f);

            _machine.changeState<DecidingNextMinigame>();
        }

        public override void update(float deltaTime)
        {
            
        }
    }
}