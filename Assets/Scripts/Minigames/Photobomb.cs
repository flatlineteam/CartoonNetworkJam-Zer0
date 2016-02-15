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

        public Sprite photobombImage;
        public GameObject flashPrefab;

        public Transform Left;
        public Transform Right;

        public Collider2D TapArea;

        public float SecondsPerPass;

        public float OKDistance;

        public AudioClip ShutterClick;

        private SoundKit.SKSound Shutter;

        private PressGesture pressGesture;
        private Sequence sequence;
        private float actualSecondsPerPass;

        protected override void OnUnityStart()
        {
            Player.localPosition = Left.localPosition;

            pressGesture = TapArea.GetComponent<PressGesture>();
            
            pressGesture.Pressed += Tapped;
        }

        private void Tapped(object sender, EventArgs eventArgs)
        {
            if (Stopped)
                return;

            if (Vector3.Distance(Player.position, Target.position) <= OKDistance)
            {
                MarkAsSuccess();
                Player.gameObject.GetComponent<SpriteRenderer>().sprite = photobombImage;
                Player.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                Player.localScale = new Vector3(1.0f, 1.0f);
                Player.position += new Vector3(-1.0f, 0.0f);
                Instantiate(flashPrefab);
                Shutter = SoundKit.instance.playSound(ShutterClick);
            }
        }

        protected override void OnStartMinigame()
        {
            actualSecondsPerPass = SecondsPerPass / StartInfo.SpeedFactor;
            
            var ease = Ease.InOutSine;

            Player.localPosition = Left.localPosition;

            sequence = DOTween.Sequence()
                .Append(Player.DOLocalMoveX(Right.localPosition.x, SecondsPerPass).SetEase(ease).OnStepComplete(FlipPlayerRight))
                .Append(Player.DOLocalMoveX(Left.localPosition.x, SecondsPerPass).SetEase(ease).OnStepComplete(FlipPlayerLeft))
                .SetLoops(-1);
        }

        protected void FlipPlayerLeft()
        {
            Player.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        protected void FlipPlayerRight()
        {
            Player.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        protected override void OnUnityUpdate()
        {
        }

        protected override void CleanUp()
        {
            sequence.Kill();
        }
    }
}