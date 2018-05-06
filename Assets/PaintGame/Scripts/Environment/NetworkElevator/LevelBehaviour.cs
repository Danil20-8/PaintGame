using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PaintGame.Environment.NetworkElevator
{
    public class LevelBehaviour : MonoBehaviour
    {
        [SerializeField]
        Transform _rotationPoint;

        public Transform rotationPoint { get { return _rotationPoint; } }
    }
}
