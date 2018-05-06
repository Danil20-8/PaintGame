using UnityEngine;

namespace PaintGame.Menu.MainMenu
{
    public class StartModeBehaviour : MonoBehaviour
    {
        public void ClickCreate()
        {
            MainMenu.GoToCreateGame();
        }

        public void ClickJoin()
        {
            MainMenu.GoToJoinGame();
        }

        MainMenuBehaviour MainMenu { get { return GetComponentInParent<MainMenuBehaviour>(); } }
    }
}
