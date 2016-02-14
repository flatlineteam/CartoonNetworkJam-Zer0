using System;
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

        [Range(0, 10)]
        public float Speed = 0.5f;

        private float actualSpeed;

        public void Start()
        {
            actualSpeed = Speed * MinigameController.Current.SpeedFactor;
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

        public void Update()
        {
            var toTarget = (Target.position - transform.position).normalized;
            var angle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg;
            
            transform.position += toTarget * actualSpeed * Time.deltaTime;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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