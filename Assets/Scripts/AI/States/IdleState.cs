using UnityEngine;

namespace Entities.Enemy.Ai.States
{
    public class IdleState: IState
    {
        public void Tick()
        {
        }

        public void FixedTick()
        {
        }

        public void OnEnter()
        {
            Debug.Log("Idle");
        }

        public void OnExit()
        {
        }
    }
}
