using System;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SimpleStateBackground : MonoBehaviour
    {
        public Minigame Minigame;

        public Sprite RunningSprite;

        public Sprite SuccessSprite;

        public Sprite FailedSprite;

        private SpriteRenderer spriteRenderer;

        public void Start()
        {
            Minigame.StateChanged += OnStateChanged;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnStateChanged(Minigame.MinigameState minigameState)
        {
            if (minigameState == Minigame.MinigameState.Running)
            {
                spriteRenderer.sprite = RunningSprite;
            }
            else if (minigameState == Minigame.MinigameState.Failed)
            {
                spriteRenderer.sprite = FailedSprite;
            }
            else if (minigameState == Minigame.MinigameState.Succeeded)
            {
                spriteRenderer.sprite = SuccessSprite;
            }
        }
    }
}