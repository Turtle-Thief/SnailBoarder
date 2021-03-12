using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TricksController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        playerMovement = this.GetComponent<PlayerMovement>();
    }

    public void OnWheelie()
    {
        if (playerMovement.isGrounded && !playerMovement.isDoingTrick)
        {
            StartCoroutine(PauseForTrick(2f));
            StartCoroutine(WheelieAnim());  //tmp while no animations
            Debug.Log("Wheelie!!!!");
            GameManager.instance.AddToScore(15);
        }
    }

    IEnumerator PauseForTrick(float pauseTime)
    {
        playerMovement.isDoingTrick = true;
        yield return new WaitForSeconds(pauseTime);
        playerMovement.isDoingTrick = false;
    }

    IEnumerator WheelieAnim()  //tmp
    {
        transform.Rotate(-10f, 0f, 0f);
        yield return new WaitForSeconds(2f);
        transform.Rotate(10f, 0f, 0f);
    }
}
