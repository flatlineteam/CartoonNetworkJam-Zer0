using System;
using UnityEngine;

namespace Assets.Scripts
{
    public abstract class CompletedMinigameScriptBase : MonoBehaviour
    {
        public Minigame Minigame { get; set; }

        public event Action FinishedScript;

        public void MinigameCompletedSuccessfully()
        {
            Action action = () =>
            {
                if (FinishedScript != null)
                    FinishedScript();
            };

            OnMinigameCompletedSuccessfully(action);
        }

        public void MinigameFailed()
        {
            Action action = () =>
            {
                if (FinishedScript != null)
                    FinishedScript();
            };

            OnMinigameFailed(action);
        }

        protected abstract void OnMinigameCompletedSuccessfully(Action finished);

        protected abstract void OnMinigameFailed(Action finished);
    }
}