using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using DefaultNamespace;
using Positions;
using UnityEngine;

public class PositionManager : MonoBehaviour, IPositionManager
{
    
    [SerializeField] private GameMode gameMode;
    
    private List<Position> _positions;

    private int _previousPosition;
    
    public event PositionCallback OnPlayerPositionChange;
    public event PositionCallback OnPlayerFinishRace;
    public event LapCallback OnPlayerFinishLap;

    private int _angleThreshold = 180; // This is an angle that can never be traveled in a single update.

    private void Awake()
    {
        _positions = FindObjectsOfType<Racer>().Select(r => new Position(r)).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _positions.Count; i++)
        {
            Position p = _positions[i];
            float newAngle = CalculateAngle(p.Racer.transform.position.normalized);
            if(p.Angle - newAngle > _angleThreshold) LapCompleted(i, true);
            else if (p.Angle - newAngle < -_angleThreshold) LapCompleted(i, false);
            p.Angle = newAngle;
        }
        
        _positions.Sort(); // Sort the positions, when should this be done? (And how often?)
        int newPosition = FindPlayerPosition();

        if (newPosition != _previousPosition)
        {
            OnPlayerPositionChange?.Invoke(newPosition);
            _previousPosition = newPosition;
        }
    }

    public List<Racer> GetRacersPositions()
    {
        return _positions.Select(p => p.Racer).ToList();
    }

    private void LapCompleted(int racerIndex, bool increment)
    {
        Position p = _positions[racerIndex];
        if (increment) p.Laps++;
        else p.Laps--;
        if (p.Racer.IsPlayer)
        {
            if (p.Laps == gameMode.TotalLaps) OnPlayerFinishRace?.Invoke(racerIndex);
            else OnPlayerFinishLap?.Invoke(p.Laps);
        }
    }

    /**
     * Calculates the Angle between the vector provided and a Vector2 pointing down.
     */
    private float CalculateAngle(Vector2 angle)
    {
        float value = Vector2.SignedAngle(Vector2.down, angle);
        return value < 0 ? 360 + value : value;
    }

    private int FindPlayerPosition()
    {
        return _positions.FindIndex(p => p.Racer.IsPlayer);
    }
    
    private class Position : IComparable<Position>
    {
        public Racer Racer { get; private set; }
        public int Laps { get; set; }
        public float Angle { get; set; }

        public Position(Racer racer)
        {
            Racer = racer;
            Laps = -1; // Race starts before the finish line.
            Angle = 0;
        }

        /**
         * Compares two Positions.
         * The Position that is smaller is the one is ahead in the race.
         */
        public int CompareTo(Position other)
        {
            // Can this be improved?
            if (other == null) return 1;
            if (Laps > other.Laps) return -1;
            if (Laps < other.Laps) return 1;
            if (Angle > other.Angle) return -1;
            if (Angle < other.Angle) return 1;
            return 0;
        }
    }
}
