using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaintGame.Core.Match
{
    public interface IGameOverChecker
    {
        bool IsGameOver { get; }
    }
}
