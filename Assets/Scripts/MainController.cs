using UnityEngine;

namespace Assets.Scripts
{
    public class MainController : MonoBehaviour
    {
        public Canvas MinigameCanvas;

        public static MainController Current { get; private set; }

        public void Start()
        {
            Current = this;
        } 
    }
}