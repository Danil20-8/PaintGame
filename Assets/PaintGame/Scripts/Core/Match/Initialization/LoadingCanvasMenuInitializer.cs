using PaintGame.Core.Match.Messeges;
using System.Collections;
using UnityEngine;

namespace PaintGame.Core.Match.Initialization
{
    public class LoadingCanvasMenuInitializer : MonoBehaviour
    {
        void Start()
        {
            PGServiceLocator.Messenger.Subscribe<MatchInitializationStartedMessege>(StartInitialization);
        }

        void StartInitialization(MatchInitializationStartedMessege mism)
        {
            PGServiceLocator.MatchInitializationStatus.WaitMe(this);
            StartCoroutine(StartInitializationCoroutine(mism));
        }

        IEnumerator StartInitializationCoroutine(MatchInitializationStartedMessege mism)
        {
            var canvas = FindObjectOfType<Canvas>();
            PGServiceLocator.MenuManager.SetCanvas(canvas);

            yield return null;

            PGServiceLocator.MatchInitializationStatus.Ready(this);
        }
    }
}
