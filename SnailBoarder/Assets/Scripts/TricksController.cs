using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TricksController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    PlayerMovement playerMovement;

    public enum TrickName
    {
        NullTrick,
        Ollie,
        Wheelie,
        KickFlip,
        PopShuvit,
        HospitalFlip,
        NumOfTricks
    };

    public struct Trick
    {
        public TrickName mName;
        public int mTier;
        public int mPoints;
        public float mDuration;
        public bool mIsGroundTrick;

        public Trick(TrickName nameNew, int tierNew, int pointsNew, float durationNew, bool isGroundTrickNew)
        {
            this.mName = nameNew;
            this.mTier = tierNew;
            this.mPoints = pointsNew;
            this.mDuration = durationNew;
            this.mIsGroundTrick = isGroundTrickNew;
        }
    }

    [HideInInspector]
    public Trick[] Tricks = new Trick[(int)TrickName.NumOfTricks];

    [HideInInspector]
    public Trick currentTrick;
    float timeSinceLastTrickStart = 0;
    public float buttonComboDelayMax = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        playerMovement = this.GetComponent<PlayerMovement>();

        // Adding all tricks
        // IDEA: ?Make them editable in Unity inspector?
        Tricks[(int)TrickName.NullTrick] = new Trick(TrickName.NullTrick, 0, 0, 0f, false);
        Tricks[(int)TrickName.Ollie] = new Trick(TrickName.Ollie, 1, 10, 1f, true);
        Tricks[(int)TrickName.Wheelie] = new Trick(TrickName.Wheelie, 2, 15, 2f, true);
        Tricks[(int)TrickName.KickFlip] = new Trick(TrickName.KickFlip, 2, 25, 2f, true);
        Tricks[(int)TrickName.PopShuvit] = new Trick(TrickName.PopShuvit, 2, 30, 2f, true);
        Tricks[(int)TrickName.HospitalFlip] = new Trick(TrickName.HospitalFlip, 3, 50, 2f, true);

        currentTrick = Tricks[(int)TrickName.NullTrick];
        timeSinceLastTrickStart = 0;
    }

    private void Update()
    {
        // If the game is NOT paused...
        if (!GameManager.instance.gameIsPaused)
        {
            // Put everything in here!!!

            timeSinceLastTrickStart += Time.deltaTime;
        }
    }

    void TrickInputCall(Trick trickType)
    {
        if(currentTrick.mName == TrickName.NullTrick)  // If no trick are in progress
        {
            Debug.Log("Here1");

            if((trickType.mIsGroundTrick && playerMovement.isGrounded) ||
                (!trickType.mIsGroundTrick && !playerMovement.isGrounded))
                StartTrick(trickType);
        }
        else if (timeSinceLastTrickStart < buttonComboDelayMax && currentTrick.mTier < trickType.mTier && currentTrick.mIsGroundTrick == trickType.mIsGroundTrick) // If another trick is already in progress
        {
            Debug.Log("Here2");
            StopAllCoroutines();
            playerRigidbody.constraints = RigidbodyConstraints.None;

            StartTrick(trickType);
        }
    }

    void StartTrick(Trick trickType)
    {
        StartCoroutine(PauseForTrick(trickType.mDuration));

        switch (trickType.mName)
        {
            case TrickName.Ollie:
                OllieAnim();
                Debug.Log("!Ollie!");
                break;
            case TrickName.Wheelie:
                StartCoroutine(WheelieAnim()); //tmp
                Debug.Log("!Wheelie!");
                break;
            case TrickName.KickFlip:
                StartCoroutine(KickflipAnim()); //tmp
                Debug.Log("!OnKickflip!");
                break;
            case TrickName.PopShuvit:
                StartCoroutine(PopShuvitAnim()); //tmp
                Debug.Log("!OnPopShuvit!");
                break;
            case TrickName.HospitalFlip:
                StartCoroutine(HospitalFlipAnim()); //tmp
                Debug.Log("!OnHospitalFlip!");
                break;
        }

        timeSinceLastTrickStart = 0;
        currentTrick = trickType;
    }

    public void OnOllie()
    {
        Debug.Log("Input Ollie");
        TrickInputCall(Tricks[(int)TrickName.Ollie]);
    }

    public void OnWheelie()
    {
        Debug.Log("Input Wheelie");
        TrickInputCall(Tricks[(int)TrickName.Wheelie]);
    }

    public void OnKickflip()
    {
        Debug.Log("Input Kickflip");
        TrickInputCall(Tricks[(int)TrickName.KickFlip]);
    }
    
    public void OnPopShuvit()
    {
        Debug.Log("Input PopShuvit");
        TrickInputCall(Tricks[(int)TrickName.PopShuvit]);
    }

    public void OnHospitalFlip()
    {
        Debug.Log("Input HospitalFlip");
        TrickInputCall(Tricks[(int)TrickName.HospitalFlip]);
    }

    IEnumerator PauseForTrick(float pauseTime)
    {
        yield return new WaitForSeconds(pauseTime);
        currentTrick = Tricks[(int)TrickName.NullTrick];
    }

    void OllieAnim()
    {
        playerMovement.Jump(1.0f);
    }
    
    IEnumerator WheelieAnim()  //tmp
    {
        float jumpTime = 0.2f; // can change
        float rotationTime = 1.5f - jumpTime;
        float rotationAnimSpeed = 30f / rotationTime;

        //playerMovement.Jump(1f);
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

        //playerMovement.Jump(1.0f);
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

        playerMovement.Jump(0.7f);
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

        playerMovement.Jump(0.7f);
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
