using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraInit : MonoBehaviour
    {
        public void Awake()
        {
            StartCoroutine(Refresh());
        }

        public IEnumerator Refresh()
        {
            while (true)
            {
                var aspectRatio = 4.0f / 3.0f;

                var screenAspectRatio = (float)Screen.width / Screen.height;

                var selfCamera = GetComponent<Camera>();

                var scaleHeight = screenAspectRatio / aspectRatio;
                Debug.Log(scaleHeight);

                if (scaleHeight > 1.0f)
                {
                    selfCamera.orthographicSize = 5 / scaleHeight;
                }

                yield return new WaitForSeconds(1);
            }
        }
    }
}