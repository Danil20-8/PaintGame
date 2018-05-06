using UnityEngine;
using UnityEngine.UI;

namespace PaintGame.Menu.MainMenu.Lobby
{
    public class PlayerListItemBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Text playerNameText;
        [SerializeField]
        private Text readyText;

        [SerializeField]
        private Color readyColor;
        [SerializeField]
        private Color notReadyColor;

        public void SetReady(bool ready)
        {
            if(ready)
            {
                readyText.text = "Готов";
                readyText.color = readyColor;
            }
            else
            {
                readyText.text = string.Empty;
                readyText.color = notReadyColor;
            }
        }
        public void SetPlayerName(string name)
        {
            playerNameText.text = name;
        }
    }
}