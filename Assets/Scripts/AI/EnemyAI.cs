using System;
using Entities.Enemy.Ai.States;
using UnityEngine;

namespace Entities.Enemy.Ai
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private Stats stats;

        private StateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            var moveState = new MoveState(GetComponent<Rigidbody2D>(), stats);
            _stateMachine.SetState(moveState);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedTick();
        }
    }
}
