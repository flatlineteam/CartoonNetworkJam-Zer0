using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class DestroyOnMinigameState : MonoBehaviour
    {
        public Minigame Minigame;

        public Minigame.MinigameState StateToDestroyOn;

        public void Start()
        {
            Minigame.StateChanged += MinigameOnStateChanged;            
        }

        private void MinigameOnStateChanged(Minigame.MinigameState minigameState)
        {
            if (this == null || gameObject == null)
                return;

            if (minigameState == StateToDestroyOn && gameObject != null)
                Destroy(gameObject);
        }
    }
}