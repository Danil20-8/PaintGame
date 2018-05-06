using UnityEngine;
using UnityEngine.UI;

namespace PaintGame.Menu.MainMenu
{
    public class JoinGameBehaviour : MainMenuPageBehaviour
    {
        [SerializeField]
        private Text addressText;

        [SerializeField]
        private Text addressTextValidationMessage;

        public void ClickJoin()
        {
            if(StartClient())
                MainMenu.GoToLobby();
        }

        private bool StartClient()
        {
            if(string.IsNullOrEmpty(addressText.text))
            {
                addressTextValidationMessage.text = "Введи адрес!";
                return false;
            }

            var lobby = GetComponentInParent<LobbyManager>();

            var hostData = addressText.text.Split(':');
            if (hostData.Length > 1)
            {
                int port;
                if (int.TryParse(hostData[1], out port))
                {
                    if(lobby.StartClient(hostData[0], port))
                    {
                        return true;
                    }
                }
                else
                {
                    addressTextValidationMessage.text = "Порт должен быть числом!";
                    return false;
                }
            }
            else
            {
                if (lobby.StartClient(hostData[0]))
                    return true;
            }

            addressTextValidationMessage.text = "Не удалось подключиться к серверу!";
            return false;
        }

        public void ClickBack()
        {
            MainMenu.GoToStartMode();
        }
    }
}
