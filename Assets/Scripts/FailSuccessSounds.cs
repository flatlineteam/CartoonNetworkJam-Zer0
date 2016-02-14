using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class FailSuccessSounds : CompletedMinigameScriptBase
    {
        public AudioClip WinGame;
        public AudioClip LoseGame;

        [Range(0, 5)]
        public float HoldSeconds = 1;

        private SoundKit.SKSound soundPlaying;
        private Action finished;

        protected override void OnMinigameCompletedSuccessfully(Action finished)
        {
            this.finished = finished;
            soundPlaying = SoundKit.instance.playPitchedSound(WinGame, GameFlowController.Current.CurrentSpeed);
            StartCoroutine(StopAfterTime());
        }

        protected override void OnMinigameFailed(Action finished)
        {
            this.finished = finished;
            soundPlaying = SoundKit.instance.playPitchedSound(LoseGame, GameFlowController.Current.CurrentSpeed);
            StartCoroutine(StopAfterTime());
        }

        public IEnumerator StopAfterTime()
        {
            yield return new WaitForSeconds(HoldSeconds);
            Finished();
        }

        private void Finished()
        {
            finished();

            if (soundPlaying != null)
                soundPlaying.stop();
        }
    }
}