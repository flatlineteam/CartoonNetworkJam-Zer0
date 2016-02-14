using UnityEngine;

namespace Assets.Scripts
{
    public class PlaySoundOnEnabled : MonoBehaviour
    {
        public AudioClip AudioClip;

        public void OnEnabled()
        {
            SoundKit.instance.playOneShot(AudioClip);
        }
    }
}