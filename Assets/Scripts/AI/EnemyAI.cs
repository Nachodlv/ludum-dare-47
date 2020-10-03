using System;
using UnityEngine;

namespace Entities.Enemy.Ai
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private Stats stats;

        private StateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new StateMachine();

        }
    }
}
