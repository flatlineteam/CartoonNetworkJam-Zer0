using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [Range(0, 100)]
        public float Time;

        public bool startOnStart;       

        public void Start()
        {
            if(startOnStart)
            {
                StartCoroutine(DestroyOnDelay());
            }
        }

        public void OnEnable()
        {
            StartCoroutine(DestroyOnDelay());
        }

        public IEnumerator DestroyOnDelay()
        {
            yield return new WaitForSeconds(Time);
            Destroy(gameObject);
        }
    }
}