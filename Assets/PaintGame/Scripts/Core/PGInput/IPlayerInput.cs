using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PaintGame.Core.PGInput
{
    public interface IPlayerInput :
        IFire, IJump, IMove, ILook
    {

    }

    public interface IFire
    {
        float Fire();
    }

    public interface IJump
    {
        float Jump();
    }

    public interface IMove
    {
        Vector2 Move();
    }

    public interface ILook
    {
        Vector2 Look();
    }
}
