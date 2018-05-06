using UnityEngine;

namespace PaintGame.Core.MenuManager
{
    public interface IMenuManager
    {
        Canvas CurrentCanvas { get; }
        void SetCanvas(string canvasName);
        void SetCanvas(Canvas canvas);
    }
}
