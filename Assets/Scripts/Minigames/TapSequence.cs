using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class TapSequence : MinigameScriptBase
    {
        public TapSequenceItem[] Items;

        private int currentIndex;
        private TapSequenceItem currentItem;

        protected override void OnUnityStart()
        {
            foreach (var item in Items)
            {
                var tapItem = item.GetComponent<TapSequenceItem>();
                tapItem.Tapped += () => ItemTapped(tapItem);

                item.gameObject.SetActive(false);
            }
        }

        private void ItemTapped(TapSequenceItem item)
        {
            if (item != currentItem)
                return;

            SetNextItem();
        }

        protected override void OnStartMinigame()
        {
            currentIndex = -1;
            SetNextItem();
        }

        private void SetNextItem()
        {
            if (currentItem != null)
                currentItem.gameObject.SetActive(false);

            currentIndex++;

            if (currentIndex >= Items.Length)
            {
                currentItem = null;
                MarkAsSuccess();
                return;
            }

            currentItem = Items[currentIndex];
            currentItem.gameObject.SetActive(true);
        }

        protected override void OnUnityUpdate()
        {
            if (currentItem == null)
                return;

            if (currentItem.isActiveAndEnabled == false)
                currentItem.gameObject.SetActive(true);
        }

        protected override void CancelAnyCoroutines()
        {
        }
    }
}