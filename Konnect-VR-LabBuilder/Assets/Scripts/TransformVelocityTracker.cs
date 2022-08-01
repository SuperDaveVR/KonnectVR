using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KonnectVR
{
    public class TransformVelocityTracker : MonoBehaviour
    {
        [SerializeField,
         Tooltip("How many previous position frames to use in the velocity calculation.")]
        private int frameResolution = 10;
        private const int minFrameResolution = 2;

        private Vector3 velocity;
        public Vector3 Velocity
        {
            get
            {
                if (velocityDirty)
                    calculateVelocity();
                return velocity;
            }
        }

        private bool velocityDirty = true;

        private struct PositionFrame
        {
            public Vector3 position;
            public float deltaTime;
        }

        private Queue<PositionFrame> positionFrames = new Queue<PositionFrame>();

        private void OnValidate()
        {
            if (frameResolution < minFrameResolution)
                frameResolution = minFrameResolution;
        }

        private void Start()
        {
            //Create initial position frames
            for (int i = 0; i < frameResolution; i++)
            {
                PositionFrame frame = new PositionFrame();
                frame.position = Vector3.zero;
                frame.deltaTime = 0f;

                positionFrames.Enqueue(frame);
            }
        }

        private void calculateVelocity()
        {
            Vector3 velocitySum = Vector3.zero;

            PositionFrame[] frames = positionFrames.ToArray();

            int velocities = frames.Length - 1;
            for (int i = 0; i < velocities; i++)
            {
                velocitySum += (frames[i + 1].position - frames[i].position) / frames[i + 1].deltaTime;
            }

            velocity = velocitySum / velocities;

            velocityDirty = false;
        }

        private void recordPositions()
        {
            PositionFrame frame = positionFrames.Dequeue(); //Remove old frame

            //Updated frame values
            frame.position = transform.position;
            frame.deltaTime = Time.deltaTime;

            //Add as new frame
            positionFrames.Enqueue(frame);

            velocityDirty = true;
        }

        private void Update()
        {
            recordPositions();
        }
    }
}