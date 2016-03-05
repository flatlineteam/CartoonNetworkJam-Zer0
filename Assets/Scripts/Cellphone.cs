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
        public float RaiseTime = 0.25f;

        [Range(0, 2)]
        public float LowerTime = 0.5f;

        public float RaiseTo;
        public float LowerTo;

        public string[] RedMessages;

        public GameObject TextMessageContainerPrefab;
        public LayoutGroup TextMessageParent;

        public GameObject ScreenOnObj;

        public Transform Target;

        public Sprite RedAvatar;
        
        public void Awake()
        {
            Target.transform.localPosition = new Vector3(Target.transform.localPosition.x, LowerTo, 0);
        }

        public IEnumerator Raise()
        {
            ScreenOnObj.SetActive(false);
            var speedFactor = GameFlowController.Current.CurrentSpeed;
            var raiseTime = RaiseTime / speedFactor;

            Target.GetComponent<RectTransform>().DOAnchorPosY(RaiseTo, raiseTime);
            yield return new WaitForSeconds(raiseTime);
            ScreenOnObj.SetActive(true);
            yield return new WaitForSeconds(0.25f);
        }

        public IEnumerator ShowMessages()
        {
            var speedFactor = GameFlowController.Current.CurrentSpeed;

            var minigame = MinigameController.Current.NextMinigame;

            var senderInstance = Instantiate(TextMessageContainerPrefab);
            senderInstance.transform.SetParent(TextMessageParent.transform, false);

            SetSenderAndText(senderInstance, minigame.TextSentBy, minigame.TextMessageContents, minigame.TextMessageAvatar);

            yield return new WaitForSeconds(0.5f / speedFactor);

            var replyInstance = Instantiate(TextMessageContainerPrefab);
            replyInstance.transform.SetParent(TextMessageParent.transform, false);

            var redMessage = RedMessages[Random.Range(0, RedMessages.Length)];
            SetSenderAndText(replyInstance, "Red", redMessage, RedAvatar);
        }

        public float GetWaitSeconds()
        {
            var speedFactor = GameFlowController.Current.CurrentSpeed;
            return WaitSeconds / speedFactor;
        }

        private static void SetSenderAndText(GameObject senderInstance, string sender, string text, Sprite avatarSprite)
        {
            var avatar = senderInstance.transform.FindChild("Message").FindChild("Avatar").GetComponent<Image>();
            var senderTextObj = senderInstance.transform.FindChild("Message").FindChild("Sender").GetComponent<Text>();
            var textTextObj = senderInstance.transform.FindChild("Message").FindChild("Text").GetComponent<Text>();

            avatar.sprite = avatarSprite;
            senderTextObj.text = sender;
            textTextObj.text = text;
        }

        public IEnumerator Lower()
        {
            var speedFactor = GameFlowController.Current.CurrentSpeed;
            var lowerTime = LowerTime / speedFactor;

            Target.GetComponent<RectTransform>().DOAnchorPosY(LowerTo, lowerTime);
            yield return new WaitForSeconds(lowerTime);
        }
    }
}