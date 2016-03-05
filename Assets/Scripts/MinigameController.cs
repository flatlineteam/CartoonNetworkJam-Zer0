using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private Stack<Minigame> randomizedMinigames;

        public void Awake()
        {
            Current = this;
            Finale.IsFinale = true;
            randomizedMinigames = new Stack<Minigame>();
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
            if (CurrentMinigame != null && GameFlowController.Current.HasFailedGame == false)
            {
                Destroy(CurrentMinigame.gameObject);
                CurrentMinigame = null;
            }

            GameFlowController.Current.MarkMinigameAsCompletelyFinished();
        }

        public void SetNextMinigameRandom()
        {
            if (randomizedMinigames.Count == 0)
            {
                var randomized = Minigames.OrderBy(x => Random.value).ToList();
                foreach (var item in randomized)
                    randomizedMinigames.Push(item);
            }

            var minigamePrefab = randomizedMinigames.Pop();
            
            SetMinigame(minigamePrefab);
        }

        public void SetFinale()
        {
            SetMinigame(Finale);
        }
    }
}