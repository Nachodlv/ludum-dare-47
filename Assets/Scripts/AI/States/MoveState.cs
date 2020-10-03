using UnityEngine;
using Utils;

namespace Entities.Enemy.Ai.States
{
    public class MoveState: IState
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly Stats _stats;
        private Vector2 _velocity;

        public MoveState(Rigidbody2D rigidbody2D, Stats stats)
        {
            _rigidbody2D = rigidbody2D;
            _stats = stats;
        }

        public void Tick()
        {
            var position = _rigidbody2D.position;
            var hit = Physics2D.Raycast(position, position.normalized);
            _velocity = hit.normal.Rotate90CW();
            Debug.DrawLine(position, position + _velocity, Color.red);
        }

        public void FixedTick()
        {
            if (_rigidbody2D.velocity.magnitude < _stats.MaxSpeed)
            {
                _rigidbody2D.AddForce(_velocity * _stats.AccelerationSpeed);
            }
        }

        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }



    }
}
