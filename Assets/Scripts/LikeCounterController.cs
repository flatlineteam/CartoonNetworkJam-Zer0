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
        public bool testButton = false;


        private int Score;
        private Text ScoreValue;
        private Image ScoreImage;
        private Animation[] anims;
        private int PointsToSpew = 0;

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
            if(testButton)
            {
                testButton = false;
                AddToPointCount(100);
            }

            ScoreValue.text = Score.ToString();

            if(PointsToSpew > 1)
            {
                SpawnPoints(2);
            }
            else if (PointsToSpew > 0)
            {
                SpawnPoints(1);
            }
            else
            {
                PointsToSpew = 0;
            }
        }

        public void AddToPointCount(int count)
        {
            PointsToSpew += count;
        }

        public void SpawnPoints(int count)
        {
            SpawnPoints(count, Random.insideUnitCircle);
        }

        public void SpawnPoints(int count, Vector3 spawnPosition)
        {
            // instantiate a "count" of thumb particles that seek a transform target "collectionTarget"
            for (int i = 0; i < count; ++i)
            {
                GameObject seeker = Instantiate(ScoreSeeker, spawnPosition, Quaternion.identity) as GameObject;

                seeker.GetComponent<LikeSeekingScript>().target = collectionTarget;

                --PointsToSpew;
            }
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