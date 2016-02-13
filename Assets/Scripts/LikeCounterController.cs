using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts
{
    public class LikeCounterController : MonoBehaviour {
        public static LikeCounterController Current { get; private set; }
        public GameObject ScoreSeeker;
        public Transform collectionTarget;
        public float collectionRange;

        private int Score;
        private Text ScoreValue;
        private Image ScoreImage;
        private Animation[] anims;

        private void Start()
        {
            if (Current != null)
            {
                Destroy(this);
                Debug.Log("Multiple LikeCounterController's in scene");
            }
            Current = this;

            if(ScoreValue == null)
                ScoreValue = GetComponentInChildren<Text>();

            if(ScoreImage == null)
                ScoreImage = GetComponentInChildren<Image>();

            anims = GetComponentsInChildren<Animation>();
        }

        private void Update()
        {
            ScoreValue.text = Score.ToString();
            AddToScore(1);
        }

        public void SpawnPoints(int count)
        {
            // instantiate a "count" of thumb particles that seek a transform target "collectionTarget"

        }

        public void AddToScore(int i)
        {
            Score += i;
            foreach (var anim in anims)
            {
                if(anim == null)
                    continue;
                anim.Play();
            }             
        }
    }
}