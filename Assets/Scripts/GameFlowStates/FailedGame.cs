using System.Collections;
using DG.Tweening;
using Prime31.StateKit;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameFlowStates
{
    public class FailedGame : SKState<GameFlowController>
    {
        public override void begin()
        {
            // We lower the score from current to 0
            DOTween.To(() => LikeCounterController.Current.Likes,
                x => LikeCounterController.Current.SetScore(x, false),
                0, _context.FailedGameScoreLowerTime).SetEase(Ease.Linear);

            _context.StartCoroutine(Pulse());

            _context.StartCoroutine(Finish());
        }

        public IEnumerator Pulse()
        {
            var startTime = Time.time;
            while (Time.time < startTime + _context.FailedGameScoreLowerTime)
            {
                LikeCounterController.Current.PlayAnimations();
                yield return new WaitForSeconds(0.5f);
            }
        }

        private IEnumerator Finish()
        {
            yield return new WaitForSeconds(_context.FailedGameTimeToShow);

            SceneManager.LoadScene(k.Scenes.TITLE);
        }

        public override void update(float deltaTime)
        {
            
        }
    }
}