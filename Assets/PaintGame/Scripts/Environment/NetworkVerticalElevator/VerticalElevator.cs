using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace PaintGame.Environment.NetworkVerticalElevator
{
    public class VerticalElevator : NetworkBehaviour
    {

        [SerializeField]
        float time;

        [SerializeField]
        float startTime;

        [SerializeField]
        Rigidbody platform;

        [SerializeField]
        Transform max;

        [SerializeField]
        Transform min;

        [SyncVar]
        float progress;
        [SyncVar]
        int dir;

        float step;

        int signals;

        float startWaiting;

        bool running;

        void Awake()
        {
            step = Time.fixedDeltaTime / time;
            dir = -1;
            progress = 1f;
        }

        public void SignalUp()
        {
            ++signals;
        }

        public void SignalDown()
        {
            --signals;
        }

        void FixedUpdate()
        {
            if (isServer && progress >= 1f)
            {
                if (!running)
                {
                    if (signals > 0)
                    {
                        if (dir == -1)
                        {
                            dir = 1;
                            startWaiting = 0;
                            running = true;
                        }
                    }
                    else if (dir == 1)
                    {
                        dir = -1;
                        startWaiting = 0;
                        running = true;
                    }
                }
                else
                {
                    if(dir == -1 && signals > 0)
                    {
                        dir = 1;
                        running = false;
                    }
                    else if(dir == 1 && signals == 0)
                    {
                        dir = -1;
                        running = false;
                    }
                }
                if (running && (startWaiting += Time.fixedDeltaTime) >= startTime)
                {
                    progress = 0;
                    running = false;
                }
            }
            else if (progress < 1f)
            {
                SetDirtyBit(1);

                progress += step;
                if (progress >= 1f)
                {
                    platform.position = dir > 0 ? max.transform.position : min.transform.position;
                    //platform.transform.position = dir > 0 ? max.transform.position : min.transform.position;
                }
                else
                {
                    if(dir > 0)
                    {
                        platform.MovePosition(Vector3.Lerp(min.transform.position, max.transform.position, progress));
                        //platform.transform.position = min.transform.position + (max.transform.position - min.transform.position) * progress;
                    }
                    else
                    {
                        platform.MovePosition(Vector3.Lerp(max.transform.position, min.transform.position, progress));
                        //platform.transform.position = max.transform.position - (max.transform.position - min.transform.position) * progress;
                    }

                }
            }

        }


        public override bool OnSerialize(NetworkWriter writer, bool initialState)
        {
            writer.Write((short)dir);
            writer.Write(progress);

            return true;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            dir = reader.ReadInt16();
            progress = reader.ReadSingle();
        }
    }
}