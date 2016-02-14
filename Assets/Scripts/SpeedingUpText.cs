using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts
{
    public class SpeedingUpText : MonoBehaviour
    {
        private RectTransform rectTransform;

        public void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public IEnumerator Sequence()
        {
            const float inTime = 0.6f;
            const float outTime = 0.5f;
            const float maxScale = 10;
            const float positionChange = 4500;
            const float waitTime = 1;
            const float minorMovementAmount = 50;

            rectTransform.anchoredPosition = new Vector2(-positionChange, 0);
            rectTransform.localScale = new Vector3(maxScale, rectTransform.localScale.y);

            var ease = Ease.InOutQuart;

            var sequence = DOTween.Sequence();
            sequence.Append(rectTransform.DOAnchorPosX(-minorMovementAmount, inTime).SetEase(ease));
            sequence.Join(rectTransform.DOScaleX(1, inTime).SetEase(ease));

            sequence.Append(rectTransform.DOAnchorPosX(minorMovementAmount, waitTime).SetEase(Ease.Linear));

            sequence.Append(rectTransform.DOAnchorPosX(positionChange, outTime).SetEase(ease));
            sequence.Join(rectTransform.DOScaleX(maxScale, outTime).SetEase(ease));

            sequence.SetEase(Ease.Linear);

            yield return sequence.WaitForCompletion();
        }
    }
}