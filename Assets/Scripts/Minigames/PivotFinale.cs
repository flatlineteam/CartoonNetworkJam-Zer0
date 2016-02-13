﻿using System;
using System.Linq;
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

        private TransformGesture transformGesture;
        private PressGesture pressGesture;
        private ReleaseGesture releaseGesture;

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
                Destroy(laserInstance.gameObject);
            }
        }

        private void ReorientLaser()
        {
            var diff = touchPosition - transform.position;
            var direction = Mathf.Rad2Deg * Mathf.Atan2(diff.y, diff.x);

            if (laserInstance == null)
                return;

            laserInstance.transform.rotation = Quaternion.AngleAxis(direction, Vector3.forward);
        }

        private void SpawnLaser(Vector3 startPosition)
        {
            startLaserPosition = startPosition;
            touchPosition = startLaserPosition;

            laserInstance = Instantiate(LaserPrefab);
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

        protected override void OnTimeElapsed()
        {
        }
    }
}