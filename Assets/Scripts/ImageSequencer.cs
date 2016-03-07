using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class ImageSequencer : MonoBehaviour 
    {
        public GameObject[] backgroundImages;

        public AudioClip nailSound;

        private int CurImageIndex = -1;              

        public void incrementIndex()
        {
            CurImageIndex++;
            if (CurImageIndex >= backgroundImages.Length)
                CurImageIndex = backgroundImages.Length;
            else
            {
                backgroundImages[CurImageIndex].SetActive(true);

                //SoundKit.instance.playOneShot(nailSound, 1f);
                SoundKit.instance.playPitchedSound(nailSound, 0.4f);

                Camera.main.GetComponent<ScreenShake>().ShakeCamera(0.2f, System.TimeSpan.FromSeconds(0.2));
            }
        }
    }
}
