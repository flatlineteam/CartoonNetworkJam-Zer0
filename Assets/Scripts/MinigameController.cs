using UnityEngine;

namespace Assets.Scripts
{
    public class MinigameController : MonoBehaviour
    {
        public static MinigameController Current { get; private set; }

        public Minigame CurrentMinigame { get; set; }

        public Minigame TestMinigame;

        public Minigame[] Minigames;

        /// <summary>2x speed factor means you get half as much time to complete the minigame.</summary>
        public float SpeedFactor = 1;

        public void Start()
        {
            Current = this;
        }

        public void DoTestMinigame()
        {
            SetMinigame(TestMinigame.gameObject);
        }

        public void SetMinigame(GameObject minigamePrefab)
        {
            if (CurrentMinigame != null)
                return;

            var instance = Instantiate(minigamePrefab);
            CurrentMinigame = instance.GetComponent<Minigame>();
            StartMinigame();
        }

        public void StartMinigame()
        {
            var info = new MinigameStartInfo
            {
                SecondsToComplete = CurrentMinigame.StartingSecondsToComplete / SpeedFactor
            };

            CurrentMinigame.StartMinigame(info);
        }

        public void MinigameCompletelyFinished()
        {
            GameFlowController.Current.MarkMinigameAsCompletelyFinished();

            if (CurrentMinigame != null)
            {
                Destroy(CurrentMinigame.gameObject);
                CurrentMinigame = null;
            }
        }

        public void SetNextMinigame()
        {
            var minigamePrefab = Minigames[Random.Range(0, Minigames.Length)];
            SetMinigame(minigamePrefab.gameObject);
        }
    }
}