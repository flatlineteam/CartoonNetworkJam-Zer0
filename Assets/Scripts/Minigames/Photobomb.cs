using System;
using DG.Tweening;
using TouchScript.Gestures;
using UnityEngine;

namespace Assets.Scripts
{
    public class Photobomb : MinigameScriptBase
    {
        public Transform Target;

        public Transform Player;

        public Transform Left;
        public Transform Right;

        public Collider2D TapArea;

        public float SecondsPerPass;

        public float OKDistance;

        private PressGesture pressGesture;
        private Sequence sequence;

        protected override void OnUnityStart()
        {
            Player.localPosition = Left.localPosition;

            pressGesture = TapArea.GetComponent<PressGesture>();
            
            pressGesture.Pressed += Tapped;
        }

        private void Tapped(object sender, EventArgs eventArgs)
        {
            if (Vector3.Distance(Player.position, Target.position) <= OKDistance)
            {
                MarkAsSuccess();
            }
        }

        protected override void OnStartMinigame()
        {
            var ease = Ease.InOutSine;

            Player.localPosition = Left.localPosition;

            sequence = DOTween.Sequence()
                .Append(Player.DOLocalMoveX(Right.localPosition.x, SecondsPerPass).SetEase(ease))
                .Append(Player.DOLocalMoveX(Left.localPosition.x, SecondsPerPass).SetEase(ease))
                .SetLoops(-1);
        }

        protected override void OnUnityUpdate()
        {
        }

        protected override void CancelAnyCoroutines()
        {
            sequence.Kill();
        }
    }
}