using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecialInput;
using SpecialInput.Devices;
using UnityEngine;

namespace PaintGame.Core.PGInput
{
    public class PGMKInputDevice : IPlayerInput
    {

        PGMKInputConfig config;

        public PGMKInputDevice(PGMKInputConfig config)
        {
            this.config = config;
        }

        public float Fire()
        {
            return Input.GetMouseButton(0) ? 1f : 0f;
        }

        public float Jump()
        {
            return Input.GetKeyDown(config.jump) ? 1f : 0f;
        }

        public Vector2 Look()
        {
            return new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
        }

        public Vector2 Move()
        {
            var x = Input.GetKey(config.right) ? 1f : 0f;
            x -= Input.GetKey(config.left) ? 1f : 0f;

            var y = Input.GetKey(config.forward) ? 1f : 0f;
            y -= Input.GetKey(config.back) ? 1f : 0f;

            return new Vector2(x, y);
        }

        public void SetConfig(PGMKInputConfig config)
        {
            this.config = config;
        }
    }

    public class PGMKInputConfig : DeviceConfigBase
    {
        public KeyCode jump;
        public KeyCode forward;
        public KeyCode back;
        public KeyCode left;
        public KeyCode right;
    }
}
