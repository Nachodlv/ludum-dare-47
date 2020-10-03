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

    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PositionManager positionManager;

    [Header("Configuration")]
    [SerializeField] private int waitingAtStart = 3;
    [SerializeField] private int totalLaps = 3;
    public int TotalLaps => totalLaps;

    private EnemyAI[] _enemies;

    private void Awake()
    {
        waitTimeUI.OnFinishWaiting += StartRace;
        positionManager.OnPlayerFinishRace += FinishRace;
        playerMovement.Enabled = false;
        _enemies = FindObjectsOfType<EnemyAI>();
    }

    private void Start()
    {
        positionManager.StartCalculatingPositions(totalLaps);
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

    private void FinishRace(int playerPosition)
    {
        leaderBoardUI.ShowLeaderBoard(positionManager.GetRacersPositions());
    }
}
