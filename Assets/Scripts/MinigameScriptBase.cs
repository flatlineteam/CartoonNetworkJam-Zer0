using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Minigame))]
    public abstract class MinigameScriptBase : MonoBehaviour
    {
        public Minigame Minigame { get; set; }

        protected float StartTime { get; private set; }

        protected float TimeRemaining { get; private set; }

        protected float TimeForMinigame { get; set; }

        protected bool Stopped { get; private set; }

        private Coroutine timer;

        public void Start()
        {
            OnUnityStart();
        }

        protected abstract void OnUnityStart();

        public void StartMinigame(MinigameStartInfo info)
        {
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
            StopMinigame();
        }

        protected abstract void OnStartMinigame();

        protected virtual void OnTimeElapsed()
        {
            MarkAsFailed();
            Stopped = true;
        }

        public void Update()
        {
            TimeRemaining = StartTime + TimeForMinigame - Time.time;

            OnUnityUpdate();
        }

        protected abstract void OnUnityUpdate();

        protected void MarkAsSuccess()
        {
            Minigame.Finished(true);
        }

        protected void MarkAsFailed()
        {
            Minigame.Finished(false);
        }

        public void StopMinigame()
        {
            Stopped = true;
            CancelAnyCoroutines();
            StopCoroutine(timer);
        }

        protected abstract void CancelAnyCoroutines();
    }

    public class MinigameStartInfo
    {
        public float SecondsToComplete { get; set; }
    }
}