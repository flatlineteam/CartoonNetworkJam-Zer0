using Assets.Scripts.GameFlowStates;
using Prime31.StateKit;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameFlowController : MonoBehaviour
    {
        private SKStateMachine<GameFlowController> stateMachine; 

        public static GameFlowController Current { get; private set; }

        public Cellphone CellphonePrefab;

        public void Start()
        {
            Current = this;

            stateMachine = new SKStateMachine<GameFlowController>(this, new StartingUp());

            stateMachine.addState(new ShowingCellphone(CellphonePrefab));
            stateMachine.addState(new InMinigame());
            stateMachine.addState(new MinigameFinished());
        }

        public void Update()
        {
            stateMachine.update(Time.deltaTime);
        }

        public void MarkMinigameAsFinished()
        {
            if (stateMachine.currentState is InMinigame)
            {
                stateMachine.changeState<MinigameFinished>();
            }
        }

        public void MarkMinigameAsCompletelyFinished()
        {
            if (stateMachine.currentState is MinigameFinished)
            {
                stateMachine.changeState<ShowingCellphone>();
            }
        }
    }
}