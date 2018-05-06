using System.IO;
using UnityEngine;

namespace PaintGame.Menu.MainMenu
{
    public class MainMenuBehaviour : MonoBehaviour
    {
        [SerializeField]
        Transform content;

        public void GoToStart()
        {
            GoTo("Start");
        }

        public void GoToStartMode()
        {
            GoTo("StartMode");
        }

        public void GoToCreateGame()
        {
            GoTo("CreateGame");
        }

        public void GoToJoinGame()
        {
            GoTo("JoinGame");
        }

        public void GoToLobby()
        {
            GoTo("Lobby");
        }

        public void GoTo(string pageName)
        {
            for(var i = 0; i < content.childCount; ++i)
            {
                Destroy(content.GetChild(i).gameObject);
            }

            var pageRes = Resources.Load<Transform>(Path.Combine(Path.Combine("Menu", "MainMenu"), pageName));

            Instantiate(pageRes, content);
        }

        protected void Start()
        {
            GoToStart();
        }
    }
}
