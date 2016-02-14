using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>Simple minigame where you have to tap on multiple target objects.</summary>
    public class TapOnItems : MinigameScriptBase
    {
        public itemTriggerScript ItemPrefab;

        public Collider2D SpawnArea;

        public int SpawnCount = 4;

        private IList<itemTriggerScript> spawned;
        private Rect spawnRect;

        public void Awake()
        {
            spawnRect = new Rect(SpawnArea.bounds.min, SpawnArea.bounds.size);
            Destroy(SpawnArea);
            spawned = new List<itemTriggerScript>();
        }

        protected override void OnUnityStart()
        {
            
        }

        protected override void OnStartMinigame()
        {
            spawned = new List<itemTriggerScript>();

            for (var i = 0; i < SpawnCount; i++)
            {
                Spawn();
            }
        }

        protected override void OnUnityUpdate()
        {
            if (spawned.Count == 0)
                return;

            foreach (var item in spawned)
            {
                if (item.IsSelected == false)
                    return;
            }

            MarkAsSuccess();
        }

        public void Spawn()
        {
            var position = new Vector2(Random.Range(spawnRect.xMin, spawnRect.xMax), Random.Range(spawnRect.yMin, spawnRect.yMax));

            var instance = (GameObject)Instantiate(ItemPrefab.gameObject, position, Quaternion.identity);

            var script = instance.GetComponent<itemTriggerScript>();
            script.Parent = this;

            spawned.Add(script);
        }

        protected override void CleanUp()
        {
            foreach (var obj in spawned)
            {
                if (obj != null && obj.gameObject != null)
                    Destroy(obj.gameObject);
            }
        }
    }
}