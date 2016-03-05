using UnityEngine;

namespace Assets.Scripts
{
    public class PlaySoundOnEnabled : MonoBehaviour
    {
        public AudioClip AudioClip;

        public bool SpeedUp;

        public void OnEnable()
        {
            if (SpeedUp)
            {
                SoundKit.instance.playPitchedSound(AudioClip, GameFlowController.Current.CurrentSpeed);
            }
            else
            {
                SoundKit.instance.playOneShot(AudioClip);
            }
        }
    }
}