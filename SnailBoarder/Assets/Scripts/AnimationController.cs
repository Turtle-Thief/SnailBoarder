using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator skateAnimator;
    Animator snailAnimator;

    GameObject snailByItself;
    GameObject rootSkate;
    bool isNotRelativeRotation = false;

    IEnumerator savedCoroutine;

    Vector3 initialGravity;

    void Start()
    {
        skateAnimator = transform.Find("Separator").GetComponent<Animator>();
        snailAnimator = transform.Find("Separator").Find("Model").Find("Root_ctrl").Find("SnaiByIItself").GetComponent<Animator>();
        snailByItself = transform.Find("Separator").Find("Model").Find("Root_ctrl").Find("SnaiByIItself").gameObject;
        rootSkate = transform.Find("Separator").Find("Model").Find("Root_ctrl").gameObject;
        initialGravity = Physics.gravity;
    }

    void Update()
    {
        if(isNotRelativeRotation)
        {
            snailByItself.transform.parent = transform;
        }
        else
        {
            snailByItself.transform.parent = rootSkate.transform;
        }

        //snailAnimator.SetBool("IsDoingRailgrind", gameObject.GetComponent<TricksController>().doRailGrind);
        //skateAnimator.SetBool("IsDoingRailgrind", gameObject.GetComponent<TricksController>().doRailGrind);
    }

    /*---------------SKATEBOARD-ANIMATIONS---------------*/

    public void StartOllieSkateAnim()
    {
        skateAnimator.Play("A_Ollie");

        // Next two lines may be unnecessary after merge
        //Physics.gravity = new Vector3(0, -0.01f, 0);
        //StartCoroutine(ReturnGravity(1.4f));
    }

    public void StartWheelieSkateAnim()
    {
        skateAnimator.Play("A_WheelieBoard");

        // Next two lines may be unnecessary after merge
        //Physics.gravity = new Vector3(0, -0.01f, 0);
        //StartCoroutine(ReturnGravity(1.9f));
    }

    public void StartKickflipSkateAnim()
    {
        skateAnimator.Play("A_KickflipBoard");
    }

    public void StartPopShoveitSkateAnim()
    {
        skateAnimator.Play("A_PopShoveit");
    }
    public void StartHospitalFlipSkateAnim()
    {
        Physics.gravity = initialGravity;
        skateAnimator.Play("A_HospitalFlip");
    }

    public void StartRailGrindSkateAnim()
    {
        skateAnimator.CrossFadeInFixedTime("A_Railgrind", 0.4f);
        //skateAnimator.Play("A_Railgrind");
    }

    public void StartAerialSkateAnim()
    {
        skateAnimator.Play("A_Aerial180Board");
    }
    public void StartHeelflipSkateAnim()
    {
        GetComponent<PlayerRotation>().addTimeForSavedVelocity = 0.0f;
        skateAnimator.SetBool("ShouldGoToStandard", true);
        skateAnimator.Play("A_180HeelFlipBoard");
    }
    public void StartVarialMcTwistSkateAnim()
    {
        isNotRelativeRotation = true;
        //skateAnimator.Play("A_SingleFrameBoard");
        //skateAnimator.Play("AT_VarialMcTwistBoard");
        skateAnimator.CrossFadeInFixedTime("AT_VarialMcTwistBoard", 0.4f);

    }
    public void StartAirKickflipSkateAnim()
    {
        GetComponent<PlayerRotation>().addTimeForSavedVelocity = 1.0f;
        skateAnimator.SetBool("ShouldGoToStandard", false);
        //skateAnimator.Play("A_SingleFrameBoard");
        //skateAnimator.Play("A_Kickflip720Board");
        skateAnimator.CrossFadeInFixedTime("A_Kickflip720Board", 0.4f);
    }

    /*---------------SNAIL-ANIMATIONS---------------*/

    public void StartOllieSnailAnim()
    {
        snailAnimator.Play("A_Ollie_Snail");
    }

    public void StartWheelieSnailAnim()
    {
        snailAnimator.Play("A_WheelieSnail");
    }

    public void StartKickflipSnailAnim()
    {
        snailAnimator.Play("A_Kickflip_Snail");
        isNotRelativeRotation = true;
        StartCoroutine(ReturnRotaion(2.083f + 0.1f));
    }

    public void StartPopShoveitSnailAnim()
    {
        snailAnimator.Play("A_PopShoveIt_Snail");
        isNotRelativeRotation = true;
        StartCoroutine(ReturnRotaion(2.042f + 0.1f));
    }
    public void StartHospitalFlipSnailAnim()
    {
        StopAllCoroutines();
        snailAnimator.Play("A_HospitalFlip_Snail");
        isNotRelativeRotation = true;
        StartCoroutine(ReturnRotaion(2.083f + 0.1f));
    }

    public void StartRailGrindSnailAnim()
    {
        snailAnimator.CrossFadeInFixedTime("A_RailGrind_Snail", 0.4f);
        //snailAnimator.Play("A_RailGrind_Snail");
    }

    public void StartAerialSnailAnim()
    {
        snailAnimator.Play("A_Aerial180_Snail");
    }
    public void StartHeelflipSnailAnim()
    {
        snailAnimator.SetBool("ShouldGoToStandard", true);
        isNotRelativeRotation = true;
        snailAnimator.Play("A_180HeelFlip_Snail");
        savedCoroutine = ReturnRotaion(1.625f);
        StartCoroutine(savedCoroutine);
    }
    public void StartVarialMcTwistSnailAnim()
    {
        isNotRelativeRotation = true;
        //snailAnimator.Play("A_SingleFrameStill_Snail");
        snailAnimator.CrossFadeInFixedTime("AT_VarialMcTwist_Snail", 0.4f);
        //snailAnimator.Play("AT_VarialMcTwist_Snail");
    }
    public void StartAirKickflipSnailAnim()
    {
        if(savedCoroutine != null)
            StopCoroutine(savedCoroutine);
        snailAnimator.SetBool("ShouldGoToStandard", false);
        isNotRelativeRotation = true;
        //snailAnimator.Play("A_SingleFrameStill_Snail");
        snailAnimator.CrossFadeInFixedTime("A_Kickflip720_Snail", 0.4f);
        //snailAnimator.Play("A_Kickflip720_Snail");
        StartCoroutine(ReturnRotaion(2f));
    }

    IEnumerator ReturnRotaion(float pauseTime)
    {
        // Waits until trick is finished
        yield return new WaitForSeconds(pauseTime);
        isNotRelativeRotation = false;
    }

    IEnumerator ReturnGravity(float pauseTime)
    {
        // Waits until trick is finished
        yield return new WaitForSeconds(pauseTime);
        Physics.gravity = initialGravity;

    }
}
