using System;
using Entities.Enemy.Ai.States;
using UnityEngine;
using Utils;

namespace Entities.Enemy.Ai
{
    [RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private Stats stats;

        private StateMachine _stateMachine;
        private CircleCollider2D _collider;
        public bool GameStarted { get; set; }

        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();

            _stateMachine = new StateMachine();
            var waitingToStartState = new IdleState();
            var moveState = new MoveState(GetComponent<Rigidbody2D>(), stats);
            var idleState = new IdleState();

            _stateMachine.AddTransition(waitingToStartState, idleState, () => GameStarted);
            _stateMachine.AddTransition(idleState, moveState, IsGrounded);
            _stateMachine.AddTransition(moveState, idleState, () => !IsGrounded());

            _stateMachine.SetState(waitingToStartState);
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
            Vector2 position = transform.position;
            Vector2 startLineDown = position + position.normalized * _collider.radius;
            Vector2 endLineDown = startLineDown + position.normalized * distanceCheck;
            Vector2 startLineRight = position + position.normalized.Rotate90CCW() * _collider.radius;
            Vector2 endLineRight = startLineRight + position.normalized.Rotate90CCW() * distanceCheck;
            return LineCast(startLineRight, endLineRight) || LineCast(startLineDown, endLineDown);
        }

        private bool LineCast(Vector2 startLine, Vector2 endLine)
        {
            Debug.DrawLine(startLine, endLine);
            return Physics2D.Linecast(startLine, endLine, 1 << LayerMask.NameToLayer("Terrain"));
        }
    }
}
