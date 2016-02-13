using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.PivotFinaleScripts
{
    public class ArmySpawner : MonoBehaviour
    {
        public event Action AllDestroyed;

        public event Action CollidedWithPlayer;

        public GameObject ThingToDestroyPrefab;

        [Range(0, 1000)]
        public int Count;

        [Range(0, 30)]
        public float Radius;

        public Transform Target;

        private readonly IList<GameObject> army;

        private int numDestroyed;

        public ArmySpawner()
        {
            army = new List<GameObject>();
        }

        public void Start()
        {
            for (var i = 0; i < Count; i++)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            var instance = Instantiate(ThingToDestroyPrefab);

            var thingToDestroy = instance.GetComponent<ThingToDestroy>();

            thingToDestroy.Destroyed += HandleDestroyed;
            thingToDestroy.Target = Target;
            thingToDestroy.CollidedWithPlayer += HandlePlayerCollision;

            var position = Random.insideUnitCircle.normalized * Random.Range(0.9f, 1.1f) * Radius;

            army.Add(instance);

            instance.transform.position = position;
        }

        private void HandlePlayerCollision()
        {
            if (CollidedWithPlayer != null)
                CollidedWithPlayer();

            foreach (var guy in army)
            {
                if(guy != null)
                    Destroy(guy);
            }
        }

        private void HandleDestroyed()
        {
            numDestroyed++;

            if (numDestroyed == Count)
            {
                if(AllDestroyed != null)
                    AllDestroyed();
            }
        }
    }
}