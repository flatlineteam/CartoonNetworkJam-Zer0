using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PowerBattleEffects : MonoBehaviour
    {
        public Text PowerBattleText;

        public void Start()
        {
            var sequence = DOTween.Sequence();

            var startScale = PowerBattleText.transform.localScale.x;

            float smallSize = startScale * 0.9f;
            float speed = 0.25f;

            var largeSize = startScale * 1.2f;

            PowerBattleText.transform.localScale = new Vector3(smallSize, smallSize);

            sequence.Append(PowerBattleText.transform.DOScale(largeSize, speed).SetEase(Ease.InQuad));
            sequence.Append(PowerBattleText.transform.DOScale(smallSize, speed));
            sequence.SetLoops(-1);
            sequence.SetEase(Ease.Linear);

            Camera.main.GetComponent<ScreenShake>().ShakeCamera(1.0f, TimeSpan.MaxValue);
        }
    }
}