using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class DestroyAfterTime : MonoBehaviour
    {
        [Range(0, 100)]
        public float Time;

        public bool StartOnStart;

        private Coroutine coroutine;  

        public void Start()
        {
            if(StartOnStart)
            {
                coroutine = StartCoroutine(DestroyOnDelay());
            }
        }

        public void OnEnable()
        {
            if(coroutine == null)
                coroutine = StartCoroutine(DestroyOnDelay());
        }

        public IEnumerator DestroyOnDelay()
        {
            yield return new WaitForSeconds(Time);
            Destroy(gameObject);
        }
    }
}