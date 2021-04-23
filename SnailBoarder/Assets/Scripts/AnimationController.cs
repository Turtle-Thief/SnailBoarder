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

    Vector3 initialGravity;

    void Start()
    {
        skateAnimator = transform.Find("Separator").GetComponent<Animator>();
        snailAnimator = transform.Find("Separator").Find("Root_ctrl").Find("SnaiByIItself").GetComponent<Animator>();
        snailByItself = transform.Find("Separator").Find("Root_ctrl").Find("SnaiByIItself").gameObject;
        rootSkate = transform.Find("Separator").Find("Root_ctrl").gameObject;
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
        skateAnimator.Play("A_Railgrind");
    }

    public void StartAerialSkateAnim()
    {
        skateAnimator.Play("A_Aerial180Board");
    }
    public void StartHeelflipSkateAnim()
    {
        skateAnimator.Play("A_180HeelFlipBoard");
    }
    public void StartVarialMcTwistSkateAnim()
    {
        skateAnimator.Play("AT_VarialMcTwistBoard");
    }
    public void StartAirKickflipSkateAnim()
    {
        skateAnimator.Play("A_Kickflip720Board");
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
        snailAnimator.Play("A_RailGrind_Snail");
    }

    public void StartAerialSnailAnim()
    {
        snailAnimator.Play("A_Aerial180_Snail");
    }
    public void StartHeelflipSnailAnim()
    {
        snailAnimator.Play("A_180HeelFlip_Snail");
        isNotRelativeRotation = true;
        StartCoroutine(ReturnRotaion(1.625f + 0.1f));
    }
    public void StartVarialMcTwistSnailAnim()
    {
        snailAnimator.Play("AT_VarialMcTwist_Snail");
    }
    public void StartAirKickflipSnailAnim()
    {
        snailAnimator.Play("A_Kickflip720_Snail");
        isNotRelativeRotation = true;
        StartCoroutine(ReturnRotaion(2f + 0.1f));
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
