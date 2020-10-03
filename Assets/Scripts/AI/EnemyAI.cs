using System;
using Entities.Enemy.Ai.States;
using UnityEngine;

namespace Entities.Enemy.Ai
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private Stats stats;

        private StateMachine _stateMachine;
        private CircleCollider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();

            _stateMachine = new StateMachine();
            var moveState = new MoveState(GetComponent<Rigidbody2D>(), stats);
            var idleState = new IdleState();

            _stateMachine.AddTransition(idleState, moveState, IsGrounded);
            _stateMachine.AddTransition(moveState, idleState, () => !IsGrounded());

            _stateMachine.SetState(idleState);
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedTick();
        }

        private bool IsGrounded()
        {
            var distanceCheck = 1f;
            var position = transform.position;
            var startLine = position + position.normalized * _collider.radius;
            var endLine = startLine + position.normalized * distanceCheck;
            Debug.DrawLine(startLine, endLine);
            return Physics2D.Linecast(startLine, endLine);
        }
    }
}
