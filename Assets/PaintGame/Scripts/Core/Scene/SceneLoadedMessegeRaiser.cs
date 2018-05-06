using System.Collections;
using UnityEngine;

namespace PaintGame.Core.Scene
{
    public class SceneLoadedMessegeRaiser : MonoBehaviour
    {
        protected IEnumerator Start()
        {
            yield return null;

            PGServiceLocator.Messenger.Push(new SceneLoadedMessege());

            Destroy(gameObject);
        }
    }

    public struct SceneLoadedMessege
    {
    }
}