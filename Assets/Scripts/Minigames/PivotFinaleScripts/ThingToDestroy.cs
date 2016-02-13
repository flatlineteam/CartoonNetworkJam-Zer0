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

        [Range(0, 4)]
        public float Speed = 2f;

        public void Start()
        {
            transform.DOMove(new Vector3(Target.position.x, Target.position.y, 0), Speed).SetEase(Ease.Linear);
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
                Destroy(gameObject);

                if (Destroyed != null)
                    Destroyed();
            }
            else if (otherRigidbody.name.Contains("Player"))
            {
                if (CollidedWithPlayer != null)
                    CollidedWithPlayer();
            }

            

            //TODO explosion
        }
    }
}