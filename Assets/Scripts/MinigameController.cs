using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class MinigameController : MonoBehaviour
    {
        public static MinigameController Current { get; private set; }

        public Minigame CurrentMinigame { get; set; }

        public Minigame TestMinigame;

        public Minigame Finale;

        public Minigame[] Minigames;

        public Minigame SelectedMinigamePrefab;

        /// <summary>2x speed factor means you get half as much time to complete the minigame.</summary>
        public float SpeedFactor = 1;

        public void Start()
        {
            Current = this;
            Finale.IsFinale = true;
        }

        public void DoTestMinigame()
        {
            SetMinigame(TestMinigame);
        }

        public void SetMinigame(Minigame minigamePrefab)
        {
            if (CurrentMinigame != null)
                return;

            SelectedMinigamePrefab = minigamePrefab;
        }

        public void StartMinigame()
        {
            var instance = Instantiate(SelectedMinigamePrefab.gameObject);
            CurrentMinigame = instance.GetComponent<Minigame>();

            var info = new MinigameStartInfo
            {
                SecondsToComplete = CurrentMinigame.StartingSecondsToComplete / SpeedFactor
            };

            CurrentMinigame.StartMinigame(info);
        }

        public void MinigameCompletelyFinished()
        {
            if (CurrentMinigame != null)
            {
                Destroy(CurrentMinigame.gameObject);
                CurrentMinigame = null;
            }

            GameFlowController.Current.MarkMinigameAsCompletelyFinished();
        }

        public void SetNextMinigame()
        {
            var minigamePrefab = Minigames[Random.Range(0, Minigames.Length)];
            SetMinigame(minigamePrefab);
        }

        public void SetFinale()
        {
            SetMinigame(Finale);
        }
    }
}