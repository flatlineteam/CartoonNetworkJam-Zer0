using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Cellphone : MonoBehaviour
    {
        [Range(0, 5)]
        public float WaitSeconds = 2;

        [Range(0, 2)]
        public float RaiseTime = 1;

        [Range(0, 2)]
        public float LowerTime = 0.5f;

        public float RaiseTo;
        public float LowerTo;

        public string[] RedMessages;

        public GameObject TextMessageContainerPrefab;
        public LayoutGroup TextMessageParent;
        
        public void Awake()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, LowerTo, 0);
        }

        public IEnumerator Raise()
        {
            var speedFactor = GameFlowController.Current.CurrentSpeed;
            var raiseTime = RaiseTime / speedFactor;

            GetComponent<RectTransform>().DOAnchorPosY(RaiseTo, raiseTime);
            yield return new WaitForSeconds(raiseTime);
        }

        public IEnumerator ShowMessages()
        {
            var speedFactor = GameFlowController.Current.CurrentSpeed;

            var minigame = MinigameController.Current.NextMinigame;

            var senderInstance = Instantiate(TextMessageContainerPrefab);
            senderInstance.transform.SetParent(TextMessageParent.transform, false);

            SetSenderAndText(senderInstance, minigame.TextSentBy, minigame.TextMessageContents);

            yield return new WaitForSeconds(0.5f / speedFactor);

            var replyInstance = Instantiate(TextMessageContainerPrefab);
            replyInstance.transform.SetParent(TextMessageParent.transform, false);

            SetSenderAndText(replyInstance, "Red", RedMessages[Random.Range(0, RedMessages.Length)]);
        }

        public float GetWaitSeconds()
        {
            var speedFactor = GameFlowController.Current.CurrentSpeed;
            return WaitSeconds / speedFactor;
        }

        private static void SetSenderAndText(GameObject senderInstance, string sender, string text)
        {
            var avatar = senderInstance.transform.FindChild("Message").FindChild("Avatar");
            var senderTextObj = senderInstance.transform.FindChild("Message").FindChild("Sender").GetComponent<Text>();
            var textTextObj = senderInstance.transform.FindChild("Message").FindChild("Text").GetComponent<Text>();

            senderTextObj.text = sender;
            textTextObj.text = text;
        }

        public IEnumerator Lower()
        {
            var speedFactor = GameFlowController.Current.CurrentSpeed;
            var lowerTime = LowerTime / speedFactor;

            GetComponent<RectTransform>().DOAnchorPosY(LowerTo, lowerTime);
            yield return new WaitForSeconds(lowerTime);
        }
    }
}