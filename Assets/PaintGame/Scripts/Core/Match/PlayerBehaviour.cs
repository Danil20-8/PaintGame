using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace PaintGame.Match
{
    public class PlayerBehaviour : NetworkBehaviour
    {
        bool _ready;

        [Command]
        void CmdReady(bool ready)
        {

        }

        [ClientRpc]
        void RpcStart()
        {
            if(isLocalPlayer)
            {

            }
        }

        public override void OnStartServer()
        {
            
        }

        public void ToggleReady()
        {
            _ready = !_ready;
            CmdReady(_ready);
        }
    }
}
