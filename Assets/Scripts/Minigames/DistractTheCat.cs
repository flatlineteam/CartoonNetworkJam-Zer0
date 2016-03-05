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

        public AudioClip LionCat;

        public float DistanceCatNeedsToMove;

        public float DistanceCatHasMoved;

        public Transform GunBarrel;

        public Transform PlayerToRotate;

        public GameObject LaserPrefab;

        private GameObject catInstance;

        private PressGesture pressGesture;
        private TransformGesture transformGesture;
        private ReleaseGesture releaseGesture;
        private SoundKit.SKSound CatMeow;
        private GameObject laserInstance;

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

            laserInstance = Instantiate(LaserPrefab) as GameObject;
            laserInstance.transform.SetParent(GunBarrel.transform, false);

            isHeld = true;

            if (CatMeow != null)
                CatMeow.stop();

            CatMeow = SoundKit.instance.playPitchedSound(LionCat, StartInfo.SpeedFactor);


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

            if(laserInstance != null)
                Destroy(laserInstance);

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

            //var playerToTouch = touchPosition - (Vector2)PlayerToRotate.transform.position;
            //var playerDirection = Mathf.Atan2(playerToTouch.y, playerToTouch.x);

            //PlayerToRotate.rotation = Quaternion.AngleAxis(playerDirection * Mathf.Rad2Deg, Vector3.forward);

            var laser = laserInstance.GetComponent<LineRenderer>();
            laser.SetPositions(new[]
            {
                new Vector3(GunBarrel.transform.position.x, GunBarrel.transform.position.y, 1),
                new Vector3(touchPosition.x, touchPosition.y, 1)
            });
        }

        protected override void CleanUp()
        {
            Destroy(catInstance);
            CatMeow.stop();
        }
    }
}