using UnityEngine;

namespace PaintGame.Menu.MainMenu
{
    public class MainMenuPageBehaviour : MonoBehaviour
    {
        protected MainMenuBehaviour MainMenu { get { return GetComponentInParent<MainMenuBehaviour>(); } }
    }
}
