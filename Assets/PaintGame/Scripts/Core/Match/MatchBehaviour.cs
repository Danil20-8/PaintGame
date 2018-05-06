using PaintGame.Core.Match.Messeges;
using PaintGame.Core.Scene;
using PaintGame.Events;
using PaintGame.Events.Network.Play.PlayerCharacter;
using PaintGame.Menu;
using PaintGame.Network.Play;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace PaintGame.Core.Match
{
    public class MatchBehaviour : NetworkBehaviour
    {
        [SerializeField]
        float startTimer = 1;

        [SerializeField]
        Transform[] playerPositions;

        Dictionary<Network.Play.PlayerController, Transform> playerTransformMap;

        void Start()
        {
            PGServiceLocator.Messenger.Subscribe<SceneLoadedMessege>(StartInitialization);
            PGServiceLocator.Messenger.Subscribe<GameStartedMessege>(GameStartedMessegeListener);
            PGServiceLocator.Messenger.Subscribe<GameOverMessege>(m => Debug.Log("GameOver!"));

            GlobalEventListener.Listen<AwakeLocalCharacterEvent>(e => PGServiceLocator.MenuManager.SetCanvas("MatchCanvas"));

            var istatus = PGServiceLocator.MatchInitializationStatus;
            istatus.Reset();
            StartCoroutine(StartGameCoroutine(new WaitUntil(istatus.Check)));
        }

        void StartInitialization(SceneLoadedMessege slm)
        {

        }

        IEnumerator StartGameCoroutine(IEnumerator enumerator)
        {
            yield return enumerator;

            StartGame();
        }

        void StartGame()
        {
            PGServiceLocator.MenuManager.SetCanvas("MatchWaitingCanvas");
            PGServiceLocator.GameStarter.StartGame();
        }

        void GameStartedMessegeListener(GameStartedMessege sgtm)
        {
            if (isServer)
            {
                SpawnPlayers();
            }
        }

        void SpawnPlayers()
        {
            var players = PGServiceLocator.PlayerList.Players;

            playerTransformMap = new Dictionary<Network.Play.PlayerController, Transform>(players.Length);

            int i = 0;

            foreach (var pc in players)
            {
                var t = playerPositions[i];
                playerTransformMap[pc] = t;
                pc.Spawn(t);
            }
        }

    }
}
