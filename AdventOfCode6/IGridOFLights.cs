using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode6
{
    public interface IGridOfLights
    {
        void TurnOn(Point start, Point end);
        void TurnOff(Point start, Point end);
        void Toggle(Point start, Point end);
        long LitLightsCount();
    }
}
