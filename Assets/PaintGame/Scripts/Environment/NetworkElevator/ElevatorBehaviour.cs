using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace PaintGame.Environment.NetworkElevator
{
    public class ElevatorBehaviour : NetworkBehaviour
    {
        [SerializeField]
        Transform LevelsRoot;

        LevelBehaviour[] levels;

        Transform[] rotatePoints;

        [SerializeField]
        PlatformBehaviour platform;
        Transform platformTransform;

        [SerializeField]
        float transitionSpeed;
        float progressStep;

        [SerializeField]
        float pauseDuration;

        [SyncVar]
        short s_currentLevel;
        [SyncVar]
        short s_targetLevel;
        [SyncVar]
        float s_progress;

        short currentLevel { get { return _currentLevel; } set { _currentLevel = s_currentLevel = value; } }
        short targetLevel { get { return _targetLevel; } set { _targetLevel = s_targetLevel = value; } }
        float progress { get { return _progress; } set { _progress = s_progress = value; } }

        short _currentLevel;
        short _targetLevel;
        float _progress;

        Quaternion previewPoint;
        Quaternion targetPoint;

        float pauseTimer;
        int locks;

        List<int> signals;

        bool isSignalUp { get { return signals.Any(s => s >= Mathf.Max(currentLevel, targetLevel)) && !isSignalStay && targetLevel < levels.Length; } }
        bool isSignalDown { get { return !isSignalStay && targetLevel > -1; } }

        bool isSignalStay { get { return signals.Any(s => s == Mathf.Min(currentLevel, targetLevel)); } }

        public void SignalOnLevel(LevelBehaviour level)
        {
            var levelIndex = Array.IndexOf(levels, level);

            signals.Add(levelIndex);
        }

        public void SignalOutLevel(LevelBehaviour level)
        {
            var levelIndex = Array.IndexOf(levels, level);

            signals.Remove(levelIndex);
        }

        public void Lock()
        {
            ++locks;
        }
        public void UnLock()
        {
            --locks;
        }

        // Use this for initialization
        void Start()
        {
            levels = LevelsRoot.GetComponentsInChildren<LevelBehaviour>();
            rotatePoints = levels.Select(l => l.rotationPoint).ToArray();
            signals = new List<int>(levels.Length);

            currentLevel = 0;
            targetLevel = -1;
            progress = 1f;

            platformTransform = platform.GetEndPoint(currentLevel);

            InitializeTransition();
            UpdatePlatform();
        }

        Quaternion GetLevelRotation(int levelFrom, int levelTo)
        {
            if (levelTo < 0 || levelTo >= rotatePoints.Length)
                return rotatePoints[levelFrom].rotation;

            var from = rotatePoints[levelFrom].position;
            var to = rotatePoints[levelTo].position;

            return Quaternion.LookRotation(to - from);
        }

        void InitializeTransition()
        {
            previewPoint = GetLevelRotation(_currentLevel, _currentLevel + _currentLevel - _targetLevel);
            targetPoint = GetLevelRotation(_currentLevel, _targetLevel);

            platformTransform.position = rotatePoints[_currentLevel].position;

            progressStep = transitionSpeed / Quaternion.Angle(previewPoint, targetPoint);

            platformTransform.rotation = progress < 1f ? Quaternion.Slerp(previewPoint, targetPoint, progress) : targetPoint;
        }

        void FixedUpdate()
        {
            if (isServer && progress >= 1f && locks == 0)
            {
                if (isSignalUp)
                {
                    MoveTo(1);
                }
                else if (isSignalDown)
                {
                    if ((pauseTimer += Time.fixedDeltaTime) > pauseDuration)
                    {
                        MoveTo(-1);
                    }
                }
                else
                {
                    pauseTimer = 0f;
                }
            }
            else
            {
                if (isClient)
                    UpdateVars();
                if (progress < 1f)
                {
                    progress += progressStep;
                    UpdatePlatform();
                }
            }
        }
        int updateCount;
        void UpdateVars()
        {
            if (_currentLevel != s_currentLevel ||
                _targetLevel != s_targetLevel ||
                _progress != s_progress)
            {
                _currentLevel = s_currentLevel;
                _targetLevel = s_targetLevel;
                _progress = s_progress;

                InitializeTransition();
            }
        }

        void MoveTo(short dir)
        {
            progress = 0f;
            currentLevel = (short)(dir < 0 ? Mathf.Min(currentLevel, targetLevel) : Mathf.Max(currentLevel, targetLevel));
            targetLevel = (short)(currentLevel + dir);

            platformTransform = platform.GetEndPoint(currentLevel);

            InitializeTransition();
        }

        void UpdatePlatform()
        {
            if (progress >= 1)
            {
                platformTransform.rotation = targetPoint;
                TransitionCompleted();
            }
            else
            {
                platformTransform.rotation = Quaternion.Slerp(previewPoint, targetPoint, progress);
            }
        }

        void TransitionCompleted()
        {
            pauseTimer = 0;
        }
    }
}
