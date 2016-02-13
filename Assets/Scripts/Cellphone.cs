using System.Collections;
using DG.Tweening;
using UnityEngine;

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

        public void Start()
        {
            transform.localPosition = new Vector3(transform.localPosition.x, LowerTo, 0);
        }

        public IEnumerator Raise()
        {
            transform.DOLocalMoveY(RaiseTo, RaiseTime);
            yield return new WaitForSeconds(RaiseTime);
        }

        public void ShowScreen()
        {
            //todo
        }

        public IEnumerator Lower()
        {
            transform.DOLocalMoveY(LowerTo, LowerTime);
            yield return new WaitForSeconds(LowerTime);
        }
    }
}