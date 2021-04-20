using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator skateAnimator;
    Animator snailAnimator;

    void Start()
    {
        skateAnimator = transform.Find("Separator").GetComponent<Animator>();
        snailAnimator = transform.Find("Separator").Find("Root_ctrl").Find("SnaiByIItself").GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    /*---------------SKATEBOARD-ANIMATIONS---------------*/

    public void StartOllieSkateAnim()
    {
        skateAnimator.Play("A_Ollie");
    }

    public void StartWheelieSkateAnim()
    {
        skateAnimator.Play("A_WheelieBoard");
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
        skateAnimator.Play("A_VarialMcTwistBoard");
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
    }

    public void StartPopShoveitSnailAnim()
    {
        snailAnimator.Play("A_PopShoveIt_Snail");
    }
    public void StartHospitalFlipSnailAnim()
    {
        snailAnimator.Play("A_HospitalFlip_Snail");
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
    }
    public void StartVarialMcTwistSnailAnim()
    {
        snailAnimator.Play("A_VarialMcTwist_Snail");
    }
    public void StartAirKickflipSnailAnim()
    {
        snailAnimator.Play("A_Kickflip720_Snail");
    }

}
