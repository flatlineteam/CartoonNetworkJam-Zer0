using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    [Serializable]
    public struct RandomSpriteInfo
    {
        public Sprite Sprite;
        public float Scale;
    }

    [RequireComponent(typeof(SpriteRenderer))]
    public class RandomSprite : MonoBehaviour
    {
        public RandomSpriteInfo[] Sprites;

        public void Start()
        {
            var info = Sprites[Random.Range(0, Sprites.Length)];

            GetComponent<SpriteRenderer>().sprite = info.Sprite;
            transform.localScale = Vector3.one * info.Scale;
        }
    }
}