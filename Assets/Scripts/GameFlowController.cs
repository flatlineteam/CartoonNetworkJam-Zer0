using Assets.Scripts.GameFlowStates;
using Prime31.StateKit;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameFlowController : MonoBehaviour
    {
        private SKStateMachine<GameFlowController> stateMachine; 

        public static GameFlowController Current { get; private set; }

        public bool StartAutomatically;

        public Cellphone CellphonePrefab;

        public Canvas MainCanvas;

        public int NumToCompleteThenFinale = 10;

        public int NumCompleted { get; private set; }

        public void Start()
        {
            Current = this;

            stateMachine = new SKStateMachine<GameFlowController>(this, new StartingUp(StartAutomatically));

            stateMachine.addState(new ShowingCellphone(CellphonePrefab, MainCanvas));
            stateMachine.addState(new InMinigame());
            stateMachine.addState(new MinigameFinished());
            stateMachine.addState(new DecidingNextMinigame());
            stateMachine.addState(new SpeedingUp());
        }

        public void Update()
        {
            stateMachine.update(Time.deltaTime);
        }

        public void MarkMinigameAsFinished()
        {
            if (stateMachine.currentState is InMinigame)
            {
                NumCompleted++;
                stateMachine.changeState<MinigameFinished>();
            }
        }

        public void MarkMinigameAsCompletelyFinished()
        {
            var state = stateMachine.currentState as MinigameFinished;
            if (state != null)
            {
                state.MinigameIsCompletelyFinished();
            }
        }

        public void ResetNumCompleted()
        {
            NumCompleted = 0;
        }
    }
}