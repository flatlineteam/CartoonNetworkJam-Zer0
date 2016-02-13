using UnityEngine;
using UnityEngine.UI;
using TouchScript.Gestures;
using System.Collections;

namespace Assets.Scripts
{
    public class RepeatedTap : MinigameScriptBase {
        public Collider2D repeatedTapCollider;
        public int numberOfTapsToSuccess;
        public Slider slider;

        private int RepeatedTapCount = 0;
        protected override void OnUnityStart()
        {            
            repeatedTapCollider.GetComponent<TapGesture>().Tapped += OnTapped;
            if(slider == null)
                slider = GetComponentInChildren<Slider>();
            slider.maxValue = numberOfTapsToSuccess;
        }

        void OnTapped (object sender, System.EventArgs e)
        {
            if (Stopped)
                return;
            
            RepeatedTapCount++;
            slider.value = RepeatedTapCount;

            if (RepeatedTapCount >= numberOfTapsToSuccess)
            {
                MarkAsSuccess();
            }
        }         

        protected override void OnStartMinigame()
        {            
        }

        protected override void OnUnityUpdate()
        {                                    
        }           

        protected override void CancelAnyCoroutines()
        {            
        }
    }
}
