using System.Collections.Generic;
using DefaultNamespace;

namespace Positions
{
    public delegate void PositionCallback(int position);

    public interface IPositionManager
    {
        event PositionCallback OnPlayerPositionChange;
        event PositionCallback OnPlayerFinishRace;

        List<Racer> GetRacersPositions();
    }
}
