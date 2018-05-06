using System;
using System.IO;
using UnityEngine;

namespace PaintGame.Core.MenuManager
{
    public class MenuManager : MonoBehaviour, IMenuManager
    {
        [SerializeField]
        private string menuFolder = "";

        public Canvas CurrentCanvas { get; private set; }

        public void SetCanvas(string canvasName)
        {
            var canvas = Resources.Load<Canvas>(Path.Combine(menuFolder, canvasName));
            if (canvas == null)
                throw new InvalidOperationException(string.Format("Canvas named {0} is not found", canvasName));

            SetCanvas(Instantiate(canvas));
        }

        public void SetCanvas(Canvas canvas)
        {
            if (CurrentCanvas != null)
                Destroy(CurrentCanvas.gameObject);

            CurrentCanvas = canvas;
        }

        protected void Start()
        {
            CurrentCanvas = FindObjectOfType<Canvas>();
        }
    }
}
