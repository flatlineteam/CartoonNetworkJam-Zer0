﻿using System.Collections;
using Prime31.StateKit;
using UnityEngine;

namespace Assets.Scripts.GameFlowStates
{
    public class StartingUp : SKState<GameFlowController>
    {
        private readonly bool startAutomatically;

        [Range(0, 5)]
        public float WaitTime = 2;
        
        private bool finished;

        public StartingUp(bool startAutomatically)
        {
            this.startAutomatically = startAutomatically;
        }

        public override void begin()
        {
            if (startAutomatically == false)
                return;

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