using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnableObjectOnState : MonoBehaviour
    {
        public GameObject ToEnableOnState;

        public Minigame.MinigameState StateToEnableOn;

        public bool enabledToSet = true;

        public Minigame Minigame;

        public void Start()
        {
            Minigame.StateChanged += MinigameOnStateChanged;            
        }

        private void MinigameOnStateChanged(Minigame.MinigameState minigameState)
        {
            if (minigameState == StateToEnableOn)
            {
                ToEnableOnState.SetActive(enabledToSet);
            }
        }
    }
}