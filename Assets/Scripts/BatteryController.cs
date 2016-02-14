using UnityEngine;
using System.Collections;
using Assets.Scripts.GameFlowStates;

namespace Assets.Scripts
{
    public class BatteryController : MonoBehaviour 
    {
        public float startBottomValue = 16.0f;

        public float endBottomValue = 80.0f;

        public RectTransform trans;

        public MinigameScriptBase MiniScript;

        private void Start()
        {
            if (trans == null)
                trans = GetComponentInChildren<RectTransform>();

            GameFlowController.Current.StateChanged += GameFlowController_Current_StateChanged;

            MiniScript = null;
        }

        void GameFlowController_Current_StateChanged (Prime31.StateKit.SKState<GameFlowController> obj)
        {
            if(obj is InMinigame)
                MiniScript = MinigameController.Current.CurrentMinigame.MinigameScript;

            if(obj is MinigameFinished)
                MiniScript = null;
        }

        private void Update()
        {
            
            SetBatteryValue();
        }

        private float UpdatePercent()
        {           
            if(MiniScript == null)
                return 1.0f;
            
            var t = MiniScript.PercentComplete();

            return Mathf.Lerp(startBottomValue, endBottomValue, t);
        }

        private void SetBatteryValue()
        {
            trans.offsetMin = new Vector2( trans.offsetMin.x, UpdatePercent() );
        }
    }
}