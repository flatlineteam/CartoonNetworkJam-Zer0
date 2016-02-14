using System;
using Assets.Scripts.PivotFinaleScripts;
using TouchScript.Gestures;
using UnityEngine;

namespace Assets.Scripts
{
    public class PivotFinale : MinigameScriptBase
    {
        public Collider2D TapArea;

        public GameObject LaserPrefab;

        public ArmySpawner ArmySpawner;

        public AudioClip BlasterHeld;

        public Transform LaserParent;

        public Transform Rotator;

        private TransformGesture transformGesture;
        private PressGesture pressGesture;
        private ReleaseGesture releaseGesture;
        private SoundKit.SKSound laserSound;

        private Vector3 startLaserPosition;
        private Vector3 touchPosition;

        private GameObject laserInstance;

        protected override void OnUnityStart()
        {
            transformGesture = TapArea.GetComponent<TransformGesture>();
            pressGesture = TapArea.GetComponent<PressGesture>();
            releaseGesture = TapArea.GetComponent<ReleaseGesture>();

            transformGesture.StateChanged += TransformStateChanged;
            pressGesture.Pressed += Pressed;

            releaseGesture.Released += Released;

            ArmySpawner.AllDestroyed += ArmySpawnerOnAllDestroyed;
            ArmySpawner.CollidedWithPlayer += ArmySpawnerOnCollidedWithPlayer;
        }

        private void ArmySpawnerOnCollidedWithPlayer()
        {
            MarkAsFailed();
        }

        private void ArmySpawnerOnAllDestroyed()
        {
            MarkAsSuccess();
        }

        private void Released(object sender, EventArgs eventArgs)
        {
            if(laserInstance != null)
                Destroy(laserInstance.gameObject);
            laserSound.stop();
        }

        private void Pressed(object sender, EventArgs eventArgs)
        {
            SpawnLaser(pressGesture.ActiveTouches[0].Hit.Point);
            ReorientLaser();
        }

        private void TransformStateChanged(object sender, GestureStateChangeEventArgs e)
        {
            if (e.State == Gesture.GestureState.Changed)
            {
                touchPosition += transformGesture.LocalDeltaPosition;
                ReorientLaser();
            }
            else if (e.State == Gesture.GestureState.Ended)
            {
                StopLaser();
            }
        }

        private void StopLaser()
        {
            Destroy(laserInstance.gameObject);
            Camera.main.GetComponent<ScreenShake>().StopShaking();
        }

        private void ReorientLaser()
        {
            var diff = touchPosition - transform.position;
            var direction = Mathf.Rad2Deg * Mathf.Atan2(diff.y, diff.x);

            if (laserInstance == null)
                return;
            
            Rotator.rotation = Quaternion.AngleAxis(direction, Vector3.forward);
        }

        private void SpawnLaser(Vector3 startPosition)
        {
            startLaserPosition = startPosition;
            touchPosition = startLaserPosition;
            laserInstance = Instantiate(LaserPrefab);
            laserInstance.transform.SetParent(LaserParent, false);
            laserSound = SoundKit.instance.playSound(BlasterHeld);

            Camera.main.GetComponent<ScreenShake>().ShakeCamera(0.5f, TimeSpan.MaxValue);
        }          

        public override float PercentComplete()
        {
            return 0.0f;
        }

        protected override void OnStartMinigame()
        {
        }

        protected override void OnUnityUpdate()
        {
        }

        protected override void CancelAnyCoroutines()
        {
            Camera.main.GetComponent<ScreenShake>().StopShaking();
        }

        protected override void OnTimeElapsed()
        {
        }
    }
}