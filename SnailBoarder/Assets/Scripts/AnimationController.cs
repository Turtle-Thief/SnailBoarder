using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator skateAnimator;
    Animator snailAnimator;

    void Start()
    {
        skateAnimator = transform.GetComponent<Animator>();
        skateAnimator = transform.Find("Root_ctrl").Find("SnaiByIItself").GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    /*---------------SKATEBOARD-ANIMATIONS---------------*/

    public void StartOllieSkateAnim()
    {
        skateAnimator.Play("A_Ollie");
    }

    public void StartKickflipSkateAnim()
    {
        skateAnimator.Play("A_Kickflip");
    }

    public void StartWheelieSkateAnim()
    {
        skateAnimator.Play("A_Wheelie");
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

    /*---------------SNAIL-ANIMATIONS---------------*/

    public void StartOllieSnailAnim()
    {
        snailAnimator.Play("A_OllieBaked");
    }

    public void StartRailGrindSnailAnim()
    {
        snailAnimator.Play("A_RailGrindBaked");
    }

}
