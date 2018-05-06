using PaintGame.Character;
using PaintGame.Core.Match.Messeges;
using PaintGame.Events;
using PaintGame.Events.Network.Play.PlayerList;
using PaintGame.Network.Play;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PaintGame.Core.Match
{
    public class GameOverChecker : MonoBehaviour, IGameOverChecker
    {
        public bool IsGameOver { get; private set; }

        [SerializeField]
        float timeOut = 1;

        float start;

        void Awake()
        {
            timeOut *= 60;
        }

        void Start()
        {
            GlobalEventListener.Listen<AllPlayersReadyEvent>(StartTimer);
            enabled = false;
        }

        void StartTimer(AllPlayersReadyEvent e)
        {
            start = Time.time;
            enabled = true;
        }

        void Update()
        {
            var players = PGServiceLocator.PlayerList.Players;
            int hitsToWin = CombatController.maxHits * players.Length;
            Debug.Log(hitsToWin);
            foreach(var player in players)
            {
                var hits = player.character.hits;
                if(hits > 0)
                {
                    if(hits == hitsToWin)
                        GameOver(player, false);
                    break;
                }
            }

            if(Time.time - start > timeOut)
            {
                var winner = players.OrderByDescending(p => p.character.hits).First();
                GameOver(winner, true);
            }
        }

        void GameOver(PlayerController winner, bool timeOut)
        {
            PGServiceLocator.Messenger.Push(new GameOverMessege(this, winner, timeOut));
            enabled = false;
        }
    }
}
