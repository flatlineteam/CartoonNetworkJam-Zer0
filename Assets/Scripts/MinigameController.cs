using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class MinigameController : MonoBehaviour
    {
        public static MinigameController Current { get; private set; }

        public Minigame CurrentMinigame { get; set; }
        public Minigame NextMinigame { get; set; }

        public Minigame TestMinigame;

        public Minigame Finale;

        public Minigame[] Minigames;

        public Minigame SelectedMinigamePrefab;

        /// <summary>2x speed factor means you get half as much time to complete the minigame.</summary>
        public float SpeedFactor = 1;

        private Minigame previousMinigame;

        public void Awake()
        {
            Current = this;
            Finale.IsFinale = true;
        }

        public void DoTestMinigame()
        {
            SetMinigame(TestMinigame);
            StartMinigame();
        }

        public void SetMinigame(Minigame minigamePrefab)
        {
            if (CurrentMinigame != null)
                return;

            SelectedMinigamePrefab = minigamePrefab;
            NextMinigame = minigamePrefab;
        }

        public void StartMinigame()
        {
            if (CurrentMinigame != null)
                return;

            previousMinigame = SelectedMinigamePrefab;

            var instance = Instantiate(SelectedMinigamePrefab.gameObject);
            CurrentMinigame = instance.GetComponent<Minigame>();
            NextMinigame = null;

            var info = new MinigameStartInfo
            {
                SpeedFactor = SpeedFactor,
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

        public void SetNextMinigameRandom()
        {
            Minigame minigamePrefab;
            do
            {
                minigamePrefab = Minigames[Random.Range(0, Minigames.Length)];
            } while (previousMinigame != null && previousMinigame == minigamePrefab);
            
            SetMinigame(minigamePrefab);
        }

        public void SetFinale()
        {
            SetMinigame(Finale);
        }
    }
}