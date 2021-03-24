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

    private void Update()
    {
        // If the game is NOT paused...
        if (!GameManager.instance.gameIsPaused)
        {
            // Put everything in here!!!

        }
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

    public void OnKickflip()
    {
        if (playerMovement.isGrounded && !playerMovement.isDoingTrick)
        {
            StartCoroutine(PauseForTrick(2f));
            StartCoroutine(KickflipAnim());  //tmp while no animations
            Debug.Log("OnKickflip");
            GameManager.instance.AddToScore(25);
        }
    }
    
    public void OnPopShuvit()
    {
        if (playerMovement.isGrounded && !playerMovement.isDoingTrick)
        {
            StartCoroutine(PauseForTrick(2f));
            StartCoroutine(PopShuvitAnim());  //tmp while no animations
            Debug.Log("OnPopShuvit");
            GameManager.instance.AddToScore(30);
        }
    }

    public void OnHospitalFlip()
    {
        if (playerMovement.isGrounded && !playerMovement.isDoingTrick)
        {
            StartCoroutine(PauseForTrick(2f));
            StartCoroutine(HospitalFlipAnim());  //tmp while no animations
            Debug.Log("OnHospitalFlip");
            GameManager.instance.AddToScore(50);
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
        float jumpTime = 0.2f; // can change
        float rotationTime = 1.5f - jumpTime;
        float rotationAnimSpeed = 30f / rotationTime;

        playerMovement.Jump(1f);
        yield return new WaitForSeconds(jumpTime);

        playerRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX;
        while (rotationTime > 0)
        {
            transform.Rotate(Vector3.left, Time.deltaTime * rotationAnimSpeed);
            rotationTime -= Time.deltaTime;
            yield return null;
        }
        playerRigidbody.constraints = RigidbodyConstraints.None;
    }

    IEnumerator KickflipAnim()  //tmp
    {
        float jumpTime = 0.3f; // can change
        float rotationTime = 2.0f - jumpTime;
        float kickflipRotationAnimSpeed = 360f / rotationTime;

        playerMovement.Jump(1.0f);
        yield return new WaitForSeconds(jumpTime);

        playerRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        while (rotationTime > 0)
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * kickflipRotationAnimSpeed);
            rotationTime -= Time.deltaTime;
            yield return null;
        }
        playerRigidbody.constraints = RigidbodyConstraints.None;
    }

    IEnumerator PopShuvitAnim()  //tmp
    {
        float jumpTime = 0.4f; // can change
        float rotationTime = 2.0f - jumpTime;
        float rotationAnimSpeed = 360f / rotationTime;

        playerMovement.Jump(1.5f);
        yield return new WaitForSeconds(jumpTime);

        playerRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        while (rotationTime > 0)
        {
            transform.Rotate(Vector3.left, Time.deltaTime * rotationAnimSpeed);
            rotationTime -= Time.deltaTime;
            yield return null;
        }
        playerRigidbody.constraints = RigidbodyConstraints.None;
    }

    IEnumerator HospitalFlipAnim()  //tmp
    {
        float jumpTime = 0.4f; // can change
        float rotationTime = 2.0f - jumpTime;
        float rotationAnimSpeed = 360f / rotationTime;

        playerMovement.Jump(1.5f);
        yield return new WaitForSeconds(jumpTime);

        playerRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        while (rotationTime > 0)
        {
            transform.Rotate(Vector3.back + Vector3.left, Time.deltaTime * rotationAnimSpeed);
            //transform.Rotate(Vector3.left, Time.deltaTime * rotationAnimSpeed);
            rotationTime -= Time.deltaTime;
            yield return null;
        }
        playerRigidbody.constraints = RigidbodyConstraints.None;
    }
}
