﻿using System.Collections;
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
        float distanceTravelled, time;
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

        void FixedUpdate()
        {
            if (pathCreator != null && doRailGrind)
            {
                distanceTravelled += speed * Time.deltaTime;
                time = distanceTravelled / pathCreator.path.length;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                if (time >= 1.0f)
                {
                    EndRailGrind();
                }
            }
        }


        public void StartRailGrind()
        {
            if (pathCreator != null)
            {
                speed = playerVelocity.currentSpeed;
                snailSpeed = speed;
                speed = Mathf.Clamp(speed, 10.0f, 30.0f);
                doRailGrind = true;
                playerVelocity.rigidbody.velocity = Vector3.zero;

                // check snails forward vector
                //pathCreator.path.GetClosestPointOnPath(gameObject.transform.position);


                // lerp snail to closest point on path
                //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, pathCreator.path.GetClosestPointOnPath(gameObject.transform.position), speed * Time.deltaTime);
                //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);

                // set distanceTravelled and time to the closest point distance
                time = pathCreator.path.GetClosestTimeOnPath(gameObject.transform.position);
                distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(gameObject.transform.position);
                
                if (Vector3.Angle(gameObject.transform.forward, pathCreator.path.GetDirectionAtDistance(distanceTravelled, endOfPathInstruction)) >= 90.0f)
                {
                    distanceTravelled = pathCreator.path.length + (pathCreator.path.length - distanceTravelled);
                }
            }
        }


        public void EndRailGrind()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(gameObject.transform.position);
            playerVelocity.rigidbody.velocity = pathCreator.path.GetDirectionAtDistance(distanceTravelled).normalized * speed;
            pathCreator = null;
            doRailGrind = false;
            playerVelocity.currentSpeed = snailSpeed + 5.0f;
            playerVelocity.Jump(1.0f);
            distanceTravelled = 0.0f;
            time = 0.0f;
            
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}
