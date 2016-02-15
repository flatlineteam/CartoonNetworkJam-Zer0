using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class CreditsController : MonoBehaviour
    {
        public void Back()
        {
            SceneManager.LoadScene(k.Scenes.TITLE);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Back();
            }                
        }
    }
}