using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Minigame))]
    public abstract class MinigameScriptBase : MonoBehaviour
    {
        public Minigame Minigame { get; set; }

        protected float TimeRemaining { get; private set; }

        protected float TimeForMinigame { get; set; }

        protected float StartTime { get; private set; }

        protected bool Stopped { get; private set; }

        protected MinigameStartInfo StartInfo { get; private set; }

        private Coroutine timer;

        public void Start()
        {
            OnUnityStart();
        }

        protected abstract void OnUnityStart();

        public void StartMinigame(MinigameStartInfo info)
        {
            StartInfo = info;
            StartTime = Time.time;
            TimeForMinigame = info.SecondsToComplete;

            StartElapsedTimer();

            OnStartMinigame();
        }

        private void StartElapsedTimer()
        {
            timer = StartCoroutine(Timer());
        }

        public IEnumerator Timer()
        {
            yield return new WaitForSeconds(TimeForMinigame);

            if (Stopped)
                yield break;

            OnTimeElapsed();
        }

        public virtual float PercentComplete()
        {
            return TimeRemaining / TimeForMinigame;
        }

        public virtual int CalculateScore(int baseScore)
        {
            var percent = PercentComplete();
            return (int)(baseScore * percent);
        }

        protected abstract void OnStartMinigame();

        protected virtual void OnTimeElapsed()
        {
            MarkAsFailed();
        }

        public void Update()
        {
            TimeRemaining = StartTime + TimeForMinigame - Time.time;

            if(Stopped == false)
                OnUnityUpdate();
        }

        protected abstract void OnUnityUpdate();

        protected void MarkAsSuccess()
        {
            Minigame.Finished(true);
            StopMinigame();
        }

        protected void MarkAsFailed()
        {
            Minigame.Finished(false);
            StopMinigame();
        }

        public void StopMinigame()
        {
            Stopped = true;
            CancelAnyCoroutines();
            if(timer != null)
                StopCoroutine(timer);
        }

        protected abstract void CancelAnyCoroutines();
    }

    public class MinigameStartInfo
    {
        /// <summary>2x speed factor means you get half as much time to complete the minigame.</summary>
        public float SpeedFactor { get; set; }

        public float SecondsToComplete { get; set; }
    }
}