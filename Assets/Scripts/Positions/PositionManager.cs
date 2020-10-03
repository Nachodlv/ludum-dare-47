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
        foreach (var p in _positions)
        {
            float newAngle = CalculateAngle(p.Racer.transform.position.normalized);
            if(p.Angle - newAngle > _angleThreshold) LapCompleted(p.Racer, true);
            else if (p.Angle - newAngle < -_angleThreshold) LapCompleted(p.Racer, false);
            p.Angle = newAngle;
        }

        _positions.Sort(); // Sort the positions, when should this be done? (And how often?)
        int newPosition = CalculatePlayerPosition();

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

    private void LapCompleted(Racer racer, bool increment)
    {
        for (int i = 0; i < _positions.Count; i++)
        {
            Position p = _positions[i];
            if (p.Racer == racer)
            {
                if (increment) p.Laps++;
                else p.Laps--;
                if (p.Racer.IsPlayer)
                {
                    if (p.Laps == gameMode.TotalLaps) OnPlayerFinishRace?.Invoke(i);
                    else OnPlayerFinishLap?.Invoke(p.Laps);
                }
                break;
            }
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

    private int CalculatePlayerPosition()
    {
        int position = 0;
        for (int i = 0; i < _positions.Count; i++)
        {
            if (_positions[i].Racer.IsPlayer)
            {
                position = i;
                break;
            }
        }
        return position;
    }

    private class Position : IComparable<Position>
    {
        public Racer Racer { get; private set; }
        public int Laps { get; set; }
        public float Angle { get; set; }

        public Position(Racer racer)
        {
            Racer = racer;
            Laps = 0;
            Angle = 0;
        }

        /**
         * Compares two Positions.
         * The Position that is smaller is the one is ahead in the race.
         */
        public int CompareTo(Position other)
        {
            // Can this be improved?
            // Lap is counted before the angle = 0, so the order can sometimes not be correct.
            if (other == null) return 1;
            if (Laps > other.Laps) return -1;
            if (Laps < other.Laps) return 1;
            if (Angle > other.Angle) return -1; // Should we add a threshold here?
            if (Angle < other.Angle) return 1;
            return 0;
        }
    }
}
