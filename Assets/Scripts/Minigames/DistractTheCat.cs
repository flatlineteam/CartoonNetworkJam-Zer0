using System;
using TouchScript.Gestures;
using UnityEngine;

namespace Assets.Scripts
{
    public class DistractTheCat : MinigameScriptBase
    {
        public GameObject CatPrefab;

        public Transform CatSpawnLocation;

        public float CatMoveSpeed;

        public Collider2D TapArea;

        public float DistanceCatNeedsToMove;

        public float DistanceCatHasMoved;
        private GameObject catInstance;

        private PressGesture pressGesture;
        private TransformGesture transformGesture;
        private ReleaseGesture releaseGesture;

        [Range(0, 20)]
        public float StopDistance = 4;

        private bool isHeld;
        private Vector2 touchPosition;

        protected override void OnUnityStart()
        {
            pressGesture = TapArea.GetComponent<PressGesture>();
            transformGesture = TapArea.GetComponent<TransformGesture>();
            releaseGesture = TapArea.GetComponent<ReleaseGesture>();

            pressGesture.Pressed += Pressed;
            transformGesture.StateChanged += (o, e) => { if (e.State == Gesture.GestureState.Changed) Transformed(); };
            releaseGesture.Released += Released;
        }

        private void Pressed(object sender, EventArgs eventArgs)
        {
            if (Stopped)
                return;

            isHeld = true;

            touchPosition = pressGesture.ActiveTouches[0].Hit.Point;
        }

        private void Transformed()
        {
            if (Stopped)
                return;

            touchPosition += (Vector2)transformGesture.LocalDeltaPosition;
        }

        private void Released(object sender, EventArgs eventArgs)
        {
            if (Stopped)
                return;

            isHeld = false;
        }

        protected override void OnStartMinigame()
        {
            catInstance = (GameObject)Instantiate(CatPrefab, CatSpawnLocation.position, Quaternion.identity);
        }

        protected override void OnUnityUpdate()
        {
            if (!isHeld)
                return;

            var catToTouch = touchPosition - (Vector2)catInstance.transform.position;
            var direction = Mathf.Atan2(catToTouch.y, catToTouch.x);

            if (catToTouch.magnitude < StopDistance)
                return;

            catInstance.transform.rotation = Quaternion.AngleAxis(direction * Mathf.Rad2Deg, Vector3.forward);
            
            var movement = new Vector2(Mathf.Cos(direction), Mathf.Sin(direction)) * 
                (CatMoveSpeed * StartInfo.SpeedFactor * Time.deltaTime);

            catInstance.transform.position += (Vector3)movement;

            DistanceCatHasMoved += movement.magnitude;

            if (DistanceCatHasMoved >= DistanceCatNeedsToMove)
            {
                MarkAsSuccess();
            }
        }

        protected override void CleanUp()
        {
            Destroy(catInstance);
        }
    }
}