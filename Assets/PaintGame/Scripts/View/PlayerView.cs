using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace PaintGame.View
{
    public class PlayerView : MonoBehaviour
    {
        public Text scoreText;

        public void SetScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}
