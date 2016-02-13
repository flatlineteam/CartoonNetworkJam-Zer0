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

        public int NumToCompleteThenFinale = 10;

        public int NumCompleted { get; private set; }

        public void Start()
        {
            Current = this;

            stateMachine = new SKStateMachine<GameFlowController>(this, new StartingUp(StartAutomatically));

            stateMachine.addState(new ShowingCellphone(CellphonePrefab));
            stateMachine.addState(new InMinigame());
            stateMachine.addState(new MinigameFinished());
            stateMachine.addState(new DecidingNextMinigame(NumToCompleteThenFinale));
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
            if (stateMachine.currentState is MinigameFinished)
            {
                stateMachine.changeState<DecidingNextMinigame>();
            }
        }
    }
}