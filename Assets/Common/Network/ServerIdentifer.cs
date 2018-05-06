using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Common.Network
{
    public class ServerIdentifer : NetworkBehaviour
    {
        static List<Action<bool>> serverAwaiters = new List<Action<bool>>();
        static List<Action<bool>> clientAwaiters = new List<Action<bool>>();

        bool started;

        public static void IsServer(Action<bool> isServer)
        {
            var network = GetNetwork();
            if (network != null && network.started)
                isServer(network.isServer);
            else
                serverAwaiters.Add(isServer);
        }

        public static void IsClient(Action<bool> isClient)
        {
            var network = GetNetwork();
            if (network!= null && network.started)
                isClient(network.isClient);
            else
                clientAwaiters.Add(isClient);
        }

        public override void OnStartServer()
        {
            started = true;
            CallAwaiters();
        }

        public override void OnStartClient()
        {
            if (started)
                return;
            started = true;

            CallAwaiters();
        }

        void CallAwaiters()
        {
            foreach (var a in serverAwaiters)
                a(isServer);
            foreach (var a in clientAwaiters)
                a(isClient);

            serverAwaiters.Clear();

            clientAwaiters.Clear();
        }

        static ServerIdentifer GetNetwork()
        {
            return network ?? (FindObjectOfType<ServerIdentifer>());
        }

        static ServerIdentifer network;
    }
}
