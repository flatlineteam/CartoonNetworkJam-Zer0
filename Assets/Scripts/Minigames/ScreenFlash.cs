using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Canvas))]
    public class ScreenFlash : MonoBehaviour
    {
        public Image Image;

        [Range(0, 3)]
        public float Duration = 1;

        public Ease Easing = Ease.InQuad;

        public void Start()
        {
            Image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            DOTween.To(() => Image.color.a, a => Image.color = new Color(1.0f, 1.0f, 1.0f, a), 0, Duration)
                .SetEase(Easing).OnComplete(() =>
                {
                    Destroy(gameObject);
                });
        }
    }
}