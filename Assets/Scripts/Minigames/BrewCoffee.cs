using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(PressAndHold))]
    public class BrewCoffee : MonoBehaviour
    {
        public SpriteRenderer Background;

        public SpriteRenderer SteamLayer;

        public Sprite PouringSprite1;
        public Sprite PouringSprite2;

        public Sprite NotPouringSprite;

        private bool isPouring;
        private Sprite currentSprite;
        private PressAndHold pressAndHold;

        private Coroutine spriteSwitcherCoroutine;

        public void Start()
        {
            pressAndHold = GetComponent<PressAndHold>();

            pressAndHold.PressStarted += PressAndHoldOnPressStarted;
            pressAndHold.PressStopped += PressAndHoldOnPressStopped;

            SetSteamOpacity(0);
        }

        private void SetSteamOpacity(float opacity)
        {
            SteamLayer.color = new Color(1.0f, 1.0f, 1.0f, opacity);
        }

        private void PressAndHoldOnPressStarted()
        {
            isPouring = true;

            currentSprite = PouringSprite1;
            Background.sprite = currentSprite;

            spriteSwitcherCoroutine = StartCoroutine(DoSpriteSwitcher());
        }

        private void PressAndHoldOnPressStopped()
        {
            isPouring = false;

            if (spriteSwitcherCoroutine != null)
                StopCoroutine(spriteSwitcherCoroutine);

            Background.sprite = NotPouringSprite;
        }

        public IEnumerator DoSpriteSwitcher()
        {
            var a = 0.15f;
            while (true)
            {
                Background.sprite = currentSprite = PouringSprite1;
                yield return new WaitForSeconds(a);
                Background.sprite = currentSprite = PouringSprite2;
                yield return new WaitForSeconds(a);
            }
        }

        public void Update()
        {
            if (!isPouring)
                return;
            
            var percentThrough = pressAndHold.TimeHeld / pressAndHold.ActualHoldTime;
            SetSteamOpacity(percentThrough);
        }
    }
}