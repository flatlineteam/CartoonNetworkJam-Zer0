using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Minigame : MonoBehaviour
    {
        public event Action<MinigameState> StateChanged;

        [Range(0, 10)]
        public float StartingSecondsToComplete = 5;

        [Range(10, 1000)]
        public int maxPointValueForWin = 100;

        public MinigameScriptBase MinigameScript { get; private set; }

        private CompletedMinigameScriptBase CompletedScript { get; set; }

        public MinigameController MinigameController { get; set; }

        public string TextSentBy = "";

        public string TextMessageContents = "";

        public bool IsFinale { get; set; }

        [Serializable]
        public enum MinigameState { Running, Failed, Succeeded }

        public MinigameState CurrentState { get; private set; }

        public void Awake()
        {
            MinigameScript = GetComponent<MinigameScriptBase>();

            CompletedScript = GetComponent<CompletedMinigameScriptBase>();

            if (MinigameController == null)
                MinigameController = GameObject.Find("Minigame Controller").GetComponent<MinigameController>();

            if (MinigameScript == null || CompletedScript == null)
                throw new InvalidOperationException("Missing minigame script or completed script.");

            CompletedScript.Minigame = this;
            MinigameScript.Minigame = this;

            CompletedScript.FinishedScript += OnMinigameCompletelyFinished;           
        }

        private void OnMinigameCompletelyFinished()
        {
            MinigameController.MinigameCompletelyFinished();
        }

        public void StartMinigame(MinigameStartInfo info)
        {
            SetState(MinigameState.Running);
            MinigameScript.StartMinigame(info);
        }

        public void Finished(bool success)
        {
            GameFlowController.Current.MarkMinigameAsFinished();

            MinigameScript.StopMinigame();

            if (success)
            {
                SetState(MinigameState.Succeeded);
                CompletedScript.MinigameCompletedSuccessfully();
                var scoreEarned = MinigameScript.CalculateScore(maxPointValueForWin);
                LikeCounterController.Current.AddToPointCount( scoreEarned );
            }
            else
            {
                SetState(MinigameState.Failed);
                CompletedScript.MinigameFailed();
            }
        }

        private void SetState(MinigameState state)
        {
            CurrentState = state;
            if (StateChanged != null)
                StateChanged(CurrentState);
        }
    }
}