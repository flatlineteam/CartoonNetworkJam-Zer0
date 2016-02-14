using UnityEngine;

namespace Assets.Scripts
{
    public class SpawnOnDestroy : MonoBehaviour
    {
        public GameObject Prefab;

        public void OnDestroy()
        {
            Instantiate(Prefab, transform.position, Quaternion.identity);
        }
    }
}