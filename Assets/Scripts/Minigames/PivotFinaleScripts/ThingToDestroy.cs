﻿using System;
using System.Diagnostics.CodeAnalysis;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.PivotFinaleScripts
{
    [SuppressMessage("ReSharper", "ConvertIfStatementToNullCoalescingExpression")]
    public class ThingToDestroy : MonoBehaviour
    {
        public event Action Destroyed;

        public event Action CollidedWithPlayer;

        public Transform Target { get; set; }

        [Range(0, 4)]
        public float Speed = 0.5f;

        private float actualSpeed;

        public void Start()
        {
            actualSpeed = Speed * MinigameController.Current.SpeedFactor;

            transform.DOMove(new Vector3(Target.position.x, Target.position.y, 0), 1.0f / actualSpeed).SetEase(Ease.Linear);
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            var otherCollider = collider.gameObject;

            var otherRigidbody = otherCollider.GetComponent<Rigidbody2D>();
            if (otherRigidbody == null)
            {
                otherRigidbody = otherCollider.GetComponentInParent<Rigidbody2D>();
            }

            if (otherRigidbody == null)
                return;

            if (otherRigidbody.name.Contains("SolidBeamLaser"))
            {
                OnDoDestroy();
            }
            else if (otherRigidbody.name.Contains("Player"))
            {
                if (CollidedWithPlayer != null)
                    CollidedWithPlayer();
            }
                
            //TODO explosion
        }

        public void OnDoDestroy()
        {
            LikeCounterController.Current.SpawnPoints(1, transform.position);

            Destroy(gameObject);

            if (Destroyed != null)
                Destroyed();
        }
    }
}