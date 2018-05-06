using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SpecialInput.Devices
{
    public interface ICharacterInput
        : IJump,
          IHorizontal, IVertical,
          IMoveStick, IViewStick,
          IFire
    {
    }

    public interface IFire
    {
        bool Fire();
    }

    public interface IJump
    {
        bool Jump();
    }

    public interface IHorizontal
    {
        float Horizontal();
    }
    public interface IVertical
    {
        float Vertical();
    }

    public interface IMoveStick
    {
        Vector2 MoveStick();
    }
    public interface IViewStick
    {
        Vector2 ViewStick();
    }
}
