using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class LikeCounterController : MonoBehaviour
    {
        public static LikeCounterController Current { get; private set; }

        public int Likes { get { return score; } }

        public GameObject ScoreSeeker;

        public Transform CollectionTarget;

        public float CollectionRange;

        public bool TestButton = false;
        
        private int score;
        private Text scoreValue;
        private Image scoreImage;
        private Animation[] anims;
        private int pointsToSpew = 0;

        private void Start()
        {
            if (Current != null)
            {
                Destroy(this);
                Debug.Log("Multiple LikeCounterController's in scene");
            }
            Current = this;

            if(scoreValue == null)
                scoreValue = GetComponentInChildren<Text>();

            if(scoreImage == null)
                scoreImage = GetComponentInChildren<Image>();

            anims = GetComponentsInChildren<Animation>();
        }

        private void Update()
        {
            if(TestButton)
            {
                TestButton = false;
                AddToPointCount(100);
            }

            scoreValue.text = score.ToString();

            if(pointsToSpew > 1)
            {
                SpawnPoints(2);
            }
            else if (pointsToSpew > 0)
            {
                SpawnPoints(1);
            }
            else
            {
                pointsToSpew = 0;
            }
        }

        public void AddToPointCount(int count)
        {
            pointsToSpew += count;
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
                var seeker = (GameObject)Instantiate(ScoreSeeker, spawnPosition, Quaternion.identity);

                seeker.GetComponent<LikeSeeker>().target = CollectionTarget;

                --pointsToSpew;
            }
        }

        public void AddToScore(int i)
        {
            score += i;

            PlayAnimations();
        }

        public void PlayAnimations()
        {
            foreach (var anim in anims)
            {
                if (anim == null)
                    continue;

                anim.Play();
            }
        }

        public void SetScore(int score, bool includeAnimation = true)
        {
            this.score = score;

            if(includeAnimation)
                PlayAnimations();
        }
    }
}