using UnityEngine;

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
            _velocity = Rotate90CW(hit.normal);
        }

        public void FixedTick()
        {
            _rigidbody2D.velocity = _velocity * _stats.Speed;
        }

        public void OnEnter()
        {
            Debug.Log("Start moving");
        }

        public void OnExit()
        {
            Debug.Log("Stop moving");
        }

        Vector2 Rotate90CW(Vector2 aDir)
        {
            return new Vector2(aDir.y, -aDir.x);
        }

        Vector2 Rotate90CCW(Vector2 aDir)
        {
            return new Vector2(-aDir.y, aDir.x);
        }

    }
}
