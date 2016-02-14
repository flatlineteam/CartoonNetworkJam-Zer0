using System.Collections;
using Prime31.StateKit;
using UnityEngine;

namespace Assets.Scripts.GameFlowStates
{
    public class ShowingCellphone : SKState<GameFlowController>
    {
        private readonly Cellphone cellphonePrefab;
        private readonly Canvas mainCanvas;

        private Cellphone cellphoneInstance;
        private bool finished;

        public ShowingCellphone(Cellphone cellphonePrefab, Canvas mainCanvas)
        {
            this.cellphonePrefab = cellphonePrefab;
            this.mainCanvas = mainCanvas;
        }

        public override void begin()
        {
            finished = false;
            cellphoneInstance = Object.Instantiate(cellphonePrefab).GetComponent<Cellphone>();
            cellphoneInstance.transform.SetParent(mainCanvas.transform, false);

            _context.StartCoroutine(Sequence());

            var audioSource = _context.GetComponent<AudioSource>();
            if (audioSource.isPlaying == false)
            {
                audioSource.Play();
            }
        }

        public IEnumerator Sequence()
        {
            yield return cellphoneInstance.Raise();
            yield return cellphoneInstance.ShowMessages();
            yield return new WaitForSeconds(cellphoneInstance.WaitSeconds);
            yield return cellphoneInstance.Lower();

            finished = true;
        }

        public override void reason()
        {
            if (finished)
            {
                _machine.changeState<InMinigame>();
            }
        }

        public override void end()
        {
            Object.Destroy(cellphoneInstance.gameObject);
        }

        public override void update(float deltaTime)
        {
        }
    }
}