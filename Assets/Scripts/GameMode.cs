using System.Collections.Generic;
using UI;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] private LeaderBoardUI leaderBoardUI;
    public int TotalLaps => 3;

    public void FinishRace(List<Racer> racers)
    {
        leaderBoardUI.ShowLeaderBoard(racers);
    }
}
