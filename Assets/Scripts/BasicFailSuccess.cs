using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class BasicFailSuccess : CompletedMinigameScriptBase
    {
        public GameObject FailSuccessContainerPrefab;
        public AudioClip WinGame;
        public AudioClip LoseGame;

        [Range(0, 5)]
        public float HoldSeconds = 1;

        private GameObject failSuccessContainer;
        private Action finished;
        private SoundKit.SKSound laserSound;

        protected override void OnMinigameCompletedSuccessfully(Action finished)
        {
            this.finished = finished;
            Init();
            failSuccessContainer.transform.FindChild("SuccessMinigameBasic").gameObject.SetActive(true);

            laserSound = SoundKit.instance.playSound(WinGame);

            StartCoroutine(StopAfterTime());
        }

        private void Init()
        {
            failSuccessContainer = Instantiate(FailSuccessContainerPrefab);
            failSuccessContainer.transform.SetParent(MainController.Current.MinigameCanvas.transform, false);
        }

        private void Stop()
        {
            Destroy(failSuccessContainer.gameObject);
        }

        protected override void OnMinigameFailed(Action finished)
        {
            this.finished = finished;
            Init();
            failSuccessContainer.transform.FindChild("FailedMinigameBasic").gameObject.SetActive(true);

            laserSound = SoundKit.instance.playSound(LoseGame);

            StartCoroutine(StopAfterTime());
        }

        public IEnumerator StopAfterTime()
        {
            yield return new WaitForSeconds(HoldSeconds);
            Finished();
        }

        private void Finished()
        {
            Destroy(failSuccessContainer.gameObject);
            finished();
        }
    }
}