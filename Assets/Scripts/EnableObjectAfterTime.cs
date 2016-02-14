using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnableObjectAfterTime : MonoBehaviour
    {
        [Range(0, 100)]
        public float Time;

        public bool startOnStart;

        public GameObject objectToEnable;

        public void Start()
        {
            if(startOnStart)
            {
                StartCoroutine(EnableOnDelay());
            }
        }

        public void OnEnable()
        {
            StartCoroutine(EnableOnDelay());
        }

        public IEnumerator EnableOnDelay()
        {
            yield return new WaitForSeconds(Time);
            objectToEnable.SetActive(true);
        }
    }
}