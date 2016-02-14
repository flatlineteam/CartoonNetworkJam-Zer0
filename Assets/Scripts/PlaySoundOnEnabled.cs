using UnityEngine;

namespace Assets.Scripts
{
    public class PlaySoundOnEnabled : MonoBehaviour
    {
        public AudioClip AudioClip;

        public void OnEnable()
        {
            SoundKit.instance.playOneShot(AudioClip);
        }
    }
}