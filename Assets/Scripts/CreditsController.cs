using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class CreditsController : MonoBehaviour
    {
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene(k.Scenes.TITLE);
            }                
        }
    }
}