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
        [SerializeField] private float waitingTimeBeforeReset;

        private StateMachine _stateMachine;
        private CircleCollider2D _collider;
        private static LayerMask _mask;
        private float _timeStuck;
        private float _lastAngle;
        [SerializeField] private float angleThreshold;

        public bool GameStarted { get; set; }

        private void Awake()
        {
            _collider = GetComponent<CircleCollider2D>();

            _stateMachine = new StateMachine();
            var waitingToStartState = new IdleState();
            var moveState = new MoveState(GetComponent<Rigidbody2D>(), stats, _collider.radius);
            var idleState = new IdleState();
            var resetState = new ResetState(GetComponent<Racer>());

            _stateMachine.AddTransition(waitingToStartState, idleState, () => GameStarted);
            _stateMachine.AddTransition(idleState, moveState, () => GetNearTerrain(transform.position, _collider.radius));
            _stateMachine.AddTransition(moveState, idleState, () => !GetNearTerrain(transform.position, _collider.radius));

            _stateMachine.AddTransition(idleState, resetState, IsStuck);
            _stateMachine.AddTransition(moveState, resetState, IsStuck);
            _stateMachine.AddTransition(resetState, idleState, () =>true);

            _stateMachine.SetState(waitingToStartState);
            _mask = (1 << LayerMask.NameToLayer("Terrain")) | (1 << LayerMask.NameToLayer("Player"));
        }

        private void Update()
        {
            _stateMachine.Tick();
        }

        private void FixedUpdate()
        {
            _stateMachine.FixedTick();
        }

        public static RaycastHit2D GetNearTerrain(Vector2 position, float colliderRadius)
        {
            var distanceCheck = 2f;
            Vector2 startLineRight = position + position.normalized.Rotate90CCW() * colliderRadius;
            Vector2 endLineRight = startLineRight + position.normalized.Rotate90CCW() * distanceCheck;
            var hit = LineCast(startLineRight, endLineRight);
            if (hit) return hit;
            Vector2 startLineDown = position + position.normalized * colliderRadius;
            Vector2 endLineDown = startLineDown + position.normalized * distanceCheck;
            return  LineCast(startLineDown, endLineDown);
        }

        private static RaycastHit2D LineCast(Vector2 startLine, Vector2 endLine)
        {
            Debug.DrawLine(startLine, endLine);

            return Physics2D.Linecast(startLine, endLine, _mask);
        }

        private bool IsStuck()
        {
            // It would be better to calculate this with the angle, to know if it is actually moving.
            float angle = CalculateAngle(transform.position);
            if (Math.Abs(angle - _lastAngle) > angleThreshold)
            {
                _lastAngle = angle;
                _timeStuck = 0;
                return false;
            }
            _timeStuck += Time.deltaTime;
            if (_timeStuck < waitingTimeBeforeReset) return false;
            _timeStuck = 0;
            return true;
        }
        
        private float CalculateAngle(Vector2 angle)
        {
            float value = Vector2.SignedAngle(Vector2.down, angle);
            return value < 0 ? 360 + value : value;
        }
    }
}
