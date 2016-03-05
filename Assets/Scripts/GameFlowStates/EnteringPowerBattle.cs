using System.Collections;
using Prime31.StateKit;
using UnityEngine;

namespace Assets.Scripts.GameFlowStates
{
    public class EnteringPowerBattle : SKState<GameFlowController>
    {
        private GameObject instance;

        public override void begin()
        {
            instance = Object.Instantiate(_context.EnteringPowerBattlePrefab);

            _context.StartCoroutine(WaitForTime());
        }

        private IEnumerator WaitForTime()
        {
            yield return new WaitForSeconds(_context.EnteringPowerBattleTime / _context.CurrentSpeed);

            _machine.changeState<InMinigame>();
        }

        public override void end()
        {
            Object.Destroy(instance);
        }

        public override void update(float deltaTime)
        {
            
        }
    }
}