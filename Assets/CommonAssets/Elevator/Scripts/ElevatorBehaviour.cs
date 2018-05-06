using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace Assets.Elevator {
    public class ElevatorBehaviour : MonoBehaviour {

        LevelBehaviour[] levels;

        [SerializeField]
        GameObject platform;

        [SerializeField]
        float transitionSpeed;

        [SerializeField]
        float pauseDuration;

        LevelBehaviour currentLevel;

        LevelBehaviour queredLevel;
        int queredDir;

        bool progressing;
        float completedTime;
        int locks;

        public void SignalLevel(LevelBehaviour level, int dir)
        {
            if (progressing)
            {
                queredLevel = level;
                queredDir = dir;
                return;
            }
            else
            {
                SetLevel(level, dir);
            }

        }

        public void Lock()
        {
            ++locks;
        }
        public void UnLock()
        {
            --locks;
        }

        void SetLevel(LevelBehaviour level, int dir)
        {
            if (level != currentLevel)
                return;

            progressing = true;

            var levelIndex = Array.IndexOf(levels, level);
            if((levelIndex == 0 & dir < 0) || (levelIndex == levels.Length - 1 && dir > 0))
            {
                currentLevel.TransitToIdentity(transitionSpeed, TransitionCompleted);
            }
            else
            {
                var nextLevelIndex = dir < 0 ? levelIndex - 1 : levelIndex + 1;
                var nextLevel = levels[nextLevelIndex];

                currentLevel.TransitTo(nextLevel, transitionSpeed, TransitionCompleted);

                currentLevel = nextLevel;
            }
        }
        void TransitionCompleted()
        {
            progressing = false;
            currentLevel.SetPlatform(platform.transform);
            completedTime = Time.fixedTime;
        }

        // Use this for initialization
        void Start() {
            levels = transform.GetComponentsInChildren<LevelBehaviour>();
            LevelBehaviour preview = levels[0];
            foreach(var level in levels.Skip(1))
            {
                level.SetRotatorFor(preview);
                preview = level;
            }
            currentLevel = levels[0];
            currentLevel.SetPlatform(platform.transform);
        }

        void Update()
        {
            if (!progressing && locks == 0)
            {
                if (queredLevel != null)
                {
                    SetLevel(queredLevel, queredDir);
                    queredLevel = null;
                }
                else
                {
                    if (Time.fixedTime - completedTime > pauseDuration)
                    {
                        SetLevel(currentLevel, -1);
                    }
                }
            }
        }
    }
}