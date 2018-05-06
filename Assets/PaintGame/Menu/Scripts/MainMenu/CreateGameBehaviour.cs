using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace PaintGame.Menu.MainMenu
{
    public class CreateGameBehaviour : MainMenuPageBehaviour
    {
        public void CreateLobby()
        {
            GetComponentInParent<LobbyManager>().StartHost();

            MainMenu.GoToLobby();
        }
    }
}