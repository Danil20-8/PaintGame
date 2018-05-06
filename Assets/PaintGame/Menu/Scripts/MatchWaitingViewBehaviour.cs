using PaintGame.Core.Match.Messeges;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PaintGame.Menu
{
    public class MatchWaitingViewBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Text timerText;

        public void SetTimerText(float time)
        {
            timerText.text = time.ToString("0.00");
        }

        protected void Start()
        {
            PGServiceLocator.Messenger.Subscribe<StartGameTimerMessege>(StartGameTimerListener);
        }

        protected void OnDestroy()
        {
            PGServiceLocator.Messenger.UnSubscribe<StartGameTimerMessege>(StartGameTimerListener);
        }

        void StartGameTimerListener(StartGameTimerMessege m)
        {
            SetTimerText(m.Rest);
        }
    }
}