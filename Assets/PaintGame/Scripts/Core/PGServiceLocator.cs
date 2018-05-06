using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SpecialInput;
using PaintGame.Core.ServiceFactories;
using PaintGame.Core.PGInput;
using PaintGame.Core.Spawn;
using PaintGame.Core.Match;
using Messenger;
using System.Reflection;
using PaintGame.Core.MenuManager;

namespace PaintGame
{
    public class PGServiceLocator : MonoBehaviour
    {
        public static PGServiceLocator instance { get; private set; }

        IMessenger messenger;
        public static IMessenger Messenger { get { return instance.messenger; } }

        ISpawnManager<SpawnPrefabs> spawnManager;
        public static ISpawnManager<SpawnPrefabs> SpawnManager { get { return instance.spawnManager; } }

        IGameOverChecker gameOverChecker;
        public static IGameOverChecker GameOverChecker { get { return instance.gameOverChecker; } }

        IGameStarter gameStarter;
        public static IGameStarter GameStarter { get { return instance.gameStarter; } }

        IPlayerList playerList;
        public static IPlayerList PlayerList { get { return instance.playerList; } }

        IMatchInitializationStatus matchInitializationStatus;
        public static IMatchInitializationStatus MatchInitializationStatus { get { return instance.matchInitializationStatus; } }

        IMenuManager menuManager;
        public static IMenuManager MenuManager { get { return instance.menuManager; } }

        public IPlayerInput GetPlayerInput()
        {
            return new InputDeviceFactory().GetPlayerInput();
        }


        void Awake()
        {
            if (instance != null)
                Destroy(gameObject);

            instance = this;
            DontDestroyOnLoad(this);

            foreach (var s in GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                object service = GetService(s.FieldType);
                if(service == null)
                {
                    throw new ServiceIsNotFoundException(s.FieldType);
                }

                s.SetValue(this, service);
            }

            /*messenger = GetService<IMessenger>();

            spawnManager = GetService<ISpawnManager<SpawnPrefabs>>();

            gameOverChecker = GetService<IGameOverChecker>();

            gameStarter = GetService<IGameStarter>();*/
        }

        T GetService<T>()
        {
            var component = GetComponentInChildren<T>();
            if (component == null)
                throw new ServiceIsNotFoundException(typeof(T));
            return component;
        }

        object GetService(Type serviceType)
        {
            return GetComponentInChildren(serviceType);
        }
    }

    public class ServiceLocatotException : Exception
    {
        public ServiceLocatotException(string message, Exception innerException)
            : base(message)
        {

        }
    }

    public class ServiceIsNotFoundException: ServiceLocatotException
    {
        public ServiceIsNotFoundException(Type service)
            :base(string.Format("Service {0} is not found. Please add the service component to the service locator as component or as child object", service.Name), null)
        {
        }
    }
}
