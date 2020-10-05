using System;
using System.Collections.Generic;
using Cinemachine;
using Entities.Enemy.Ai;
using Sound;
using UI;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [Header("Audio")] [SerializeField] private SoundWithSettings gameMusic;

    [Header("UI")]
    [SerializeField] private LeaderBoardUI leaderBoardUI;
    [SerializeField] private WaitTimeUI waitTimeUI;
    [SerializeField] private RectTransform playerCanvas;

    [Header("References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PositionManager positionManager;
    [SerializeField] private CinemachineVirtualCamera playerVirtualCamera;

    [Header("Configuration")]
    [SerializeField] private int waitingAtStart = 3;
    [SerializeField] private int totalLaps = 3;
    public int TotalLaps => totalLaps;

    private EnemyAI[] _enemies;

    private void Awake()
    {
        waitTimeUI.OnFinishWaiting += CooldownFinished;
        positionManager.OnPlayerFinishRace += FinishRace;
        playerMovement.Enabled = false;
        _enemies = FindObjectsOfType<EnemyAI>();
        playerCanvas.gameObject.SetActive(false);
    }

    public void StartRace()
    {
        playerCanvas.gameObject.SetActive(true);
        playerVirtualCamera.Priority = 200;
        positionManager.StartCalculatingPositions(totalLaps);
        waitTimeUI.StartCountdown(waitingAtStart);
        AudioManager.instance.SetMusicSource(gameMusic);
    }

    private void CooldownFinished()
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
