using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [Range(0, 100)]
        public float Time;

        public bool StartOnStart = true;

        public void Start()
        {
            if (StartOnStart)
                StartCoroutine(DestroyDelay());
        }

        public void OnEnable()
        {
            StartCoroutine(DestroyDelay());
        }

        public IEnumerator DestroyDelay()
        {
            yield return new WaitForSeconds(Time);
            Destroy(gameObject);
        }
    }
}