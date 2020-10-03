using UnityEngine;

namespace Entities.Enemy.Ai.States
{
    public class MoveState: IState
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly Stats _stats;

        public MoveState(Rigidbody2D rigidbody2D, Stats stats)
        {
            _rigidbody2D = rigidbody2D;
            _stats = stats;
        }

        public void Tick()
        {
        }

        public void FixedTick()
        {
            _rigidbody2D.AddForceAtPosition(Rotate90CW(_rigidbody2D.position) * _stats.Speed, Vector2.zero);
        }

        public void OnEnter()
        {
            Debug.Log("Start moving");
        }

        public void OnExit()
        {
            Debug.Log("Stop moving");
        }

        // clockwise
        Vector3 Rotate90CW(Vector3 aDir)
        {
            return new Vector3(aDir.z, 0, -aDir.x);
        }

    }
}
