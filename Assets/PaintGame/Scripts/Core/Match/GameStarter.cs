using PaintGame.Core.Match.Messeges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace PaintGame.Core.Match
{
    public class GameStarter : MonoBehaviour, IGameStarter
    {

        [SerializeField]
        float timer;

        float start;

        void Awake()
        {
            enabled = false;
        }

        public void StartGame()
        {
            start = Time.time;
            enabled = true;
        }

        void Update()
        {
            float dt = Time.time - start;

            if (dt > timer)
            {
                var messenger = PGServiceLocator.Messenger;
                messenger.Push(new StartGameTimerMessege(true, 0f, timer));
                messenger.Push(new GameStartedMessege());
                enabled = false;
            }
            else
            {
                var messenger = PGServiceLocator.Messenger;
                messenger.Push(new StartGameTimerMessege(false, timer - dt, timer));
            }
        }
    }
}
