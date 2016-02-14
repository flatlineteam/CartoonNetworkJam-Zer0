using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class ImageSequencer : MonoBehaviour 
    {
        public Sprite[] backgroundImages;

        private int CurImageIndex = 0;

        private SpriteRenderer SpriteRend;

        void Start()
        {
            SpriteRend = GetComponent<SpriteRenderer>();
            SpriteRend.sprite = backgroundImages[0];
        }

        void Update()
        {
            SpriteRend.sprite = backgroundImages[CurImageIndex];
        }

        public void incrementIndex()
        {
            CurImageIndex++;
            if (CurImageIndex >= backgroundImages.Length)
                CurImageIndex = backgroundImages.Length;
        }
    }
}
