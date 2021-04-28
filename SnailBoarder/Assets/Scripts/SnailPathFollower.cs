using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class SnailPathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public PlayerVelocity playerVelocity;
        public float speed = 5;
        public float snailSpeed = 0.0f;
        float distanceTravelled;
        public bool doRailGrind;



        /* Rail Grind process:
            -> Collision trigger makes it possible
            -> Player gives input to start trick
            -> Lerp their position to the closest point on the rail
            -> Check the players direction vs the path direction, make sure theyre going in the right direction
            -> Move snail to end of path
            -> Release snail, add to score, reset momentum
         */

        void Start()
        {
            playerVelocity = gameObject.GetComponent<PlayerVelocity>();
            doRailGrind = false;
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            if (pathCreator != null && doRailGrind)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                if (pathCreator.path.GetPointAtTime(1, endOfPathInstruction).Equals(transform.position))
                {
                    pathCreator = null;
                    doRailGrind = false;
                    playerVelocity.currentSpeed = snailSpeed + 5.0f;
                    playerVelocity.Jump(1.5f);
                }
            }
        }


        public void StartRailGrind()
        {
            if (pathCreator != null)
            {
                speed = playerVelocity.currentSpeed;
                snailSpeed = speed;
                speed = Mathf.Clamp(speed, 10.0f, 25.0f);
                doRailGrind = true;
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}
