using UnityEngine;

namespace PaintGame.Menu.MainMenu
{
    public class StartBehaviour : MonoBehaviour
    {
        public void ClickStart()
        {
            GetComponentInParent<MainMenuBehaviour>().GoToStartMode();
        }
    }
}
