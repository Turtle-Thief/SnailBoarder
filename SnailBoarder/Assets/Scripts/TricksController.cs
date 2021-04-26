using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TricksController : MonoBehaviour
{
    Rigidbody playerRigidbody;
    GroundCheck groundCheck;
    PlayerVelocity playerMovement;
    AnimationController animator;

    public enum TrickName
    {
        NullTrick,
        Ollie,
        Wheelie,
        KickFlip,
        PopShuvit,
        HospitalFlip,
        Heelflip,
        McTwist,
        AirKickflip,
        Railgrind,
        NumOfTricks
    };

    [System.Serializable]
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

    public bool readyToGetIntoAir;
    public LayerMask airTrickTriggerLayer;

    bool wasOnRamp = false;

    public Trick currentTrick;
    float timeSinceLastTrickStart = 0;
    public float buttonComboDelayMax = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        playerMovement = this.GetComponent<PlayerVelocity>();
        groundCheck = this.GetComponent<GroundCheck>();
        animator = this.GetComponent<AnimationController>();

        // Adding all tricks
        // IDEA: ?Make them editable in Unity inspector?
        Tricks[(int)TrickName.NullTrick] = new Trick(TrickName.NullTrick, 0, 0, 0f, false);
        Tricks[(int)TrickName.Ollie] = new Trick(TrickName.Ollie, 1, 10, 1f, true);
        Tricks[(int)TrickName.Wheelie] = new Trick(TrickName.Wheelie, 2, 15, 2f, true);
        Tricks[(int)TrickName.KickFlip] = new Trick(TrickName.KickFlip, 2, 25, 2f, true);
        Tricks[(int)TrickName.PopShuvit] = new Trick(TrickName.PopShuvit, 2, 30, 2f, true);
        Tricks[(int)TrickName.HospitalFlip] = new Trick(TrickName.HospitalFlip, 3, 50, 2f, true);
        Tricks[(int)TrickName.Heelflip] = new Trick(TrickName.Heelflip, 3, 75, 2f, false);
        Tricks[(int)TrickName.McTwist] = new Trick(TrickName.McTwist, 4, 75, 2f, false);
        Tricks[(int)TrickName.AirKickflip] = new Trick(TrickName.AirKickflip, 4, 75, 2f, false);
    //    Tricks[(int)TrickName.Railgrind] = new Trick(TrickName.Railgrind, 4, 35, 3f, true);

        currentTrick = Tricks[(int)TrickName.NullTrick];
        timeSinceLastTrickStart = 0;
        readyToGetIntoAir = true;
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
            //Debug.Log("Here1 " + trickType.mName.ToString());

            if((trickType.mIsGroundTrick && groundCheck.isGrounded) ||
                (!trickType.mIsGroundTrick && !groundCheck.isGrounded))
                StartTrick(trickType);
        }
        else if (timeSinceLastTrickStart < buttonComboDelayMax && currentTrick.mTier < trickType.mTier && currentTrick.mIsGroundTrick == trickType.mIsGroundTrick) // If another trick is already in progress
        {
            Debug.Log("Here2 " + trickType.mName.ToString());
            StopAllCoroutines();
            //playerRigidbody.constraints = RigidbodyConstraints.None;

            StartTrick(trickType);
        }
    }

    public void StartTrick(Trick trickType)
    {
        wasOnRamp = groundCheck.isOnRamp;
        StartCoroutine(PauseForTrick(trickType.mDuration));

        switch (trickType.mName)
        {
            case TrickName.Ollie:  // A(X)
                //OllieAnim();
                //StartCoroutine(OllieJump(1f));
                animator.StartOllieSkateAnim();
                animator.StartOllieSnailAnim();
                //Debug.Log("!Ollie!");
                break;
            case TrickName.Wheelie:  // A+X (X+Square)
                //StartCoroutine(WheelieAnim()); //tmp
                animator.StartWheelieSkateAnim();
                animator.StartWheelieSnailAnim();
                //snailAnimation.StartRailgringAnim(); //tmp
                //snailAnimation.StartRailGrindBakedAnim(); //tmp
                //Debug.Log("!Wheelie!");
                break;
            case TrickName.KickFlip:  // A+B (X+O)
                //StartCoroutine(KickflipAnim()); //tmp
                //playerMovement.Jump(1.0f);
                animator.StartKickflipSkateAnim();
                animator.StartKickflipSnailAnim();
                //Debug.Log("!OnKickflip!");
                break;
            case TrickName.PopShuvit:  // A+Y (A+Triangle)
                //StartCoroutine(PopShuvitAnim()); //tmp
                //playerMovement.Jump(1.0f);
                animator.StartPopShoveitSkateAnim();
                animator.StartPopShoveitSnailAnim();
                //Debug.Log("!OnPopShuvit!");
                break;
            case TrickName.HospitalFlip:  // A+X+B (A+Square+O)
                //StartCoroutine(HospitalFlipAnim()); //tmp
                //playerMovement.Jump(1.0f);
                animator.StartHospitalFlipSkateAnim();
                animator.StartHospitalFlipSnailAnim();
                //Debug.Log("!OnHospitalFlip!");
                break;
            case TrickName.Heelflip:  // A(X) in the air
                wasOnRamp = true;
                gameObject.GetComponent<PlayerRotation>().refreshTime = 4f;
                animator.StartHeelflipSkateAnim();
                animator.StartHeelflipSnailAnim();
                //StartCoroutine(AirTrickAnim()); //tmp
                //Debug.Log("!OnHeelflipFlip!");
                break;
            case TrickName.McTwist:  // Y(Triangle) in the air
                wasOnRamp = true;
                gameObject.GetComponent<PlayerRotation>().refreshTime = 4f;
                animator.StartVarialMcTwistSkateAnim();
                animator.StartVarialMcTwistSnailAnim();
                //StartCoroutine(AirTrickAnim()); //tmp
                //Debug.Log("!OnMcTwistFlip!");
                break;
            case TrickName.AirKickflip:  // B(O) in the air
                wasOnRamp = true;
                gameObject.GetComponent<PlayerRotation>().refreshTime = 4f;
                animator.StartAirKickflipSkateAnim();
                animator.StartAirKickflipSnailAnim();
                //StartCoroutine(AirTrickAnim()); //tmp
                //Debug.Log("!OnAirKickflip!");
                break;
            case TrickName.Railgrind:
                wasOnRamp = true;
                animator.StartRailGrindSkateAnim();
                animator.StartRailGrindSnailAnim();
                //StartCoroutine(AirTrickAnim()); //tmp
                //Debug.Log("!OnAirKickflip!");
                break;
        }

        if (trickType.mName == TrickName.Heelflip)
            timeSinceLastTrickStart = -0.5f;
        else
            timeSinceLastTrickStart = 0;

        currentTrick = trickType;
    }

    public void OnOllie()
    {
        //Debug.Log("Input Ollie");
        TrickInputCall(Tricks[(int)TrickName.Ollie]);
    }

    public void OnWheelie()
    {
        //Debug.Log("Input Wheelie");
        TrickInputCall(Tricks[(int)TrickName.Wheelie]);
    }

    public void OnKickflip()
    {
        //Debug.Log("Input Kickflip");
        TrickInputCall(Tricks[(int)TrickName.KickFlip]);
    }
    
    public void OnPopShuvit()
    {
        //Debug.Log("Input PopShuvit");
        TrickInputCall(Tricks[(int)TrickName.PopShuvit]);
    }

    public void OnHospitalFlip()
    {
        //Debug.Log("Input HospitalFlip");
        TrickInputCall(Tricks[(int)TrickName.HospitalFlip]);
    }

    public void OnHeelflip()
    {
        Debug.Log("Input Heelflip");
        TrickInputCall(Tricks[(int)TrickName.Heelflip]);
    }

    public void AskForHeelflip()
    {
        if (currentTrick.mName == Tricks[(int)TrickName.NullTrick].mName)
        {
            //Debug.Log("Ask for Heelflip");
            TrickInputCall(Tricks[(int)TrickName.Heelflip]);
        }
    }

    public void OnMcTwist()
    {
        Debug.Log("Input McTwist");
        TrickInputCall(Tricks[(int)TrickName.McTwist]);
    }

    public void OnAirKickflip()
    {
        Debug.Log("Input AirKickflip");
        TrickInputCall(Tricks[(int)TrickName.AirKickflip]);
    }

    IEnumerator PauseForTrick(float pauseTime)
    {
        // Waits until trick is finished
        yield return new WaitForSeconds(pauseTime);

        Trick tmpTrick = Tricks[(int)currentTrick.mName];
        currentTrick = Tricks[(int)TrickName.NullTrick];

        bool shouldMultiply = (GameManager.instance.IsMultipliedByJudjes() && wasOnRamp);
        //Debug.Log(wasOnRamp);
        //Debug.Log(GameManager.instance.IsMultipliedByJudjes());
        UIManager.instance.TrickFinishedHUD(tmpTrick, shouldMultiply);
    }

    IEnumerator OllieJump(float time)
    {
        yield return new WaitForSeconds(time);
        playerMovement.Jump(1.0f);
    }

    public void AirTriggerEnter()
    {
    //    Debug.Log("Air Trigger Enter " + readyToGetIntoAir);

        if (readyToGetIntoAir)
        {
            readyToGetIntoAir = false;
            GetComponent<PlayerRotation>().airCheck = true;

            //     playerRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
            //                                   RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            StartCoroutine(StopAirInTime(4f));

            StartCoroutine(RemoveConstraintsInTime(0.8f));

            
            playerMovement.Jump(1.5f);
        }
        else
        {
            //    if (playerRigidbody.constraints != RigidbodyConstraints.None)
            //        playerRigidbody.constraints = RigidbodyConstraints.None;
       //     GetComponent<PlayerRotation>().airCheck = false;
            readyToGetIntoAir = true;
            StopCoroutine(StopAirInTime(3f));
            StopCoroutine(RemoveConstraintsInTime(0.8f));
        }
        //Debug.Log("Trigger");
    }

    IEnumerator RemoveConstraintsInTime(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<PlayerRotation>().airCheck = false;
        //     if (playerRigidbody.constraints != RigidbodyConstraints.None)
        //          playerRigidbody.constraints = RigidbodyConstraints.None;
    }

    IEnumerator StopAirInTime(float time)
    {
        yield return new WaitForSeconds(time);
        readyToGetIntoAir = true;
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

    IEnumerator AirTrickAnim()  //tmp
    {
        float jumpTime = 0.3f; // can change
        float rotationTime = 2.0f - jumpTime;
        float kickflipRotationAnimSpeed = 360f / rotationTime;

        //playerMovement.Jump(0.7f);
        yield return new WaitForSeconds(jumpTime);

        //playerRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        while (rotationTime > 0)
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * kickflipRotationAnimSpeed);
            rotationTime -= Time.deltaTime;
            yield return null;
        }
        //playerRigidbody.constraints = RigidbodyConstraints.None;
    }
}
