using System.Collections.Generic;
using DefaultNamespace;

namespace Positions
{
    public delegate void PositionCallback(int position);
    public delegate void LapCallback(int lapFinished);

    public delegate void WrongDirectionCallback();

    public interface IPositionManager
    {
        event PositionCallback OnPlayerPositionChange;
        event PositionCallback OnPlayerFinishRace;
        event LapCallback OnPlayerFinishLap;

        event WrongDirectionCallback OnWrongDirection;

        List<Racer> GetRacersPositions();
    }
}
