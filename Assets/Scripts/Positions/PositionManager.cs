using System;
using System.Collections.Generic;
using System.Linq;
using Positions;
using UnityEngine;

public class PositionManager : MonoBehaviour, IPositionManager
{
    private List<Position> _positions;
    private int _previousPosition;
    private int _totalLaps;
    public event PositionCallback OnPlayerPositionChange;
    public event PositionCallback OnPlayerFinishRace;
    public event LapCallback OnPlayerFinishLap;

    public event WrongDirectionCallback OnWrongDirection;

    private int _angleThreshold = 180; // This is an angle that can never be traveled in a single update.

    [SerializeField] private float wrongDirectionAngle;
    private float _playerAngle;
    private void Awake()
    {
        _positions = FindObjectsOfType<Racer>().Select(r => new Position(r)).ToList();
        enabled = false;
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

            if (p.Racer.IsPlayer)
            {
                float fullAngle = newAngle + p.Laps * 360;
                if (_playerAngle > fullAngle + wrongDirectionAngle)
                {
                    _playerAngle = fullAngle;
                    OnWrongDirection?.Invoke();
                } else if (_playerAngle < fullAngle) _playerAngle = fullAngle;
            }
        }

        _positions.Sort(); // Sort the positions, when should this be done? (And how often?)
        int newPosition = FindPlayerPosition();

        if (newPosition != _previousPosition)
        {
            OnPlayerPositionChange?.Invoke(newPosition);
            _previousPosition = newPosition;
        }
        
        // Save player angle
        // If angle is smaller, check if the difference is bigger that the threshold
        // If it is larger, save the angle as the new value
    }
    public List<Racer> GetRacersPositions()
    {
        return _positions.Select(p => p.Racer).ToList();
    }

    public void StartCalculatingPositions(int totalLaps)
    {
        _totalLaps = totalLaps;
        enabled = true;
    }

    private void LapCompleted(int racerIndex, bool increment)
    {
        Position p = _positions[racerIndex];
        if (increment) p.Laps++;
        else p.Laps--;
        if (p.Racer.IsPlayer)
        {
            OnPlayerFinishLap?.Invoke(p.Laps);
            if (p.Laps == _totalLaps) OnPlayerFinishRace?.Invoke(racerIndex);
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
