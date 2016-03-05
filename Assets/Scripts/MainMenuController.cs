using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TouchScript.Gestures;

namespace Assets.Scripts
{
    public class MainMenuController : MonoBehaviour
    {
        public TapGesture Go;
        public TapGesture CreditsButton;
        public TapGesture QuitButton;

        public GameObject FlashPrefab;
        public float FlashAt;
        public AudioClip PowerGuit;
        public AudioClip LoopGuit;
        public SoundKit.SKSound sound;

        private AsyncOperation aSync;

        public void Start()
        {
            StartCoroutine(FlashScreen());
            Screen.orientation = ScreenOrientation.Landscape;
            sound = SoundKit.instance.playSound(PowerGuit);

            aSync = SceneManager.LoadSceneAsync(k.Scenes.DEFAULT);
            aSync.allowSceneActivation = false;
            Go.Tapped += (o, e) => StartGame();
            CreditsButton.Tapped += (o, e) => Credits();
            QuitButton.Tapped += (o, e) => Quit();
        }

        public void StartGame()
        {
            aSync.allowSceneActivation = true;
            sound.stop();
        }

        public void Credits()
        {
            SceneManager.LoadScene(k.Scenes.CREDITS);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }            
        }

        public IEnumerator FlashScreen()
        {
            yield return new WaitForSeconds(FlashAt);
            Instantiate(FlashPrefab);
            yield return new WaitForSeconds(2);
            sound = SoundKit.instance.playSoundLooped(LoopGuit);
        }
    }
}