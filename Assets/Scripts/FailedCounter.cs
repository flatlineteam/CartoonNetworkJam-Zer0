using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class FailedCounter : MonoBehaviour
    {
        public LayoutGroup LayoutGroup;

        public RectTransform FailedItemPrefab;

        private Stack<GameObject> items;

        public void Start()
        {
            items = new Stack<GameObject>();
            GameFlowController.Current.NumFailedChanged += NumFailedChanged;        
        }

        private void NumFailedChanged(int i)
        {
            while (i < items.Count)
            {
                var item = items.Pop();
                Destroy(item);
            }
            
            while (i > items.Count)
                AddItem();
        }

        private void AddItem()
        {
            var instance = Instantiate(FailedItemPrefab);
            instance.transform.SetParent(LayoutGroup.transform, false);

            items.Push(instance.gameObject);
        }
    }
}