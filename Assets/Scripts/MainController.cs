using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class MainController : MonoBehaviour
    {
        public Canvas MinigameCanvas;

        public static MainController Current { get; private set; }

        public void Start()
        {
            Current = this;

            Screen.orientation = ScreenOrientation.Landscape;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(k.Scenes.TITLE);
            }            
        }
    }
}