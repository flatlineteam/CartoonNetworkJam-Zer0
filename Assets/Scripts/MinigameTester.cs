using UnityEngine;

namespace Assets.Scripts
{
    public class MinigameTester : MonoBehaviour
    {
        public void TestMinigame()
        {
            MinigameController.Current.DoTestMinigame();
        } 
    }
}