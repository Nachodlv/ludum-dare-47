using System;
using System.Collections.Generic;
using Entities.Enemy.Ai;
using UI;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private LeaderBoardUI leaderBoardUI;
    [SerializeField] private WaitTimeUI waitTimeUI;

    [Header("Player")] [SerializeField] private PlayerMovement playerMovement;

    [Header("Configuration")]
    [SerializeField] private int waitingAtStart = 3;
    [SerializeField] private int totalLaps = 3;
    public int TotalLaps => totalLaps;

    private EnemyAI[] _enemies;

    private void Awake()
    {
        waitTimeUI.OnFinishWaiting += StartRace;
        playerMovement.Enabled = false;
        _enemies = FindObjectsOfType<EnemyAI>();
    }

    private void Start()
    {
        waitTimeUI.StartCountdown(waitingAtStart);
    }

    private void StartRace()
    {
        playerMovement.Enabled = true;
        foreach (var enemyAi in _enemies)
        {
            enemyAi.GameStarted = true;
        }
    }

    public void FinishRace(List<Racer> racers)
    {
        leaderBoardUI.ShowLeaderBoard(racers);
    }
}
