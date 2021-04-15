using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = transform.Find("riggedsnailv_01").GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    /*---------------SKATEBOARD-ANIMATIONS---------------*/

    public void StartOllieSkateAnim()
    {
        animator.Play("A_OllieBoard");
    }

    public void StartKickflipSkateAnim()
    {
        animator.Play("A_Kickflip");
    }

    public void StartWheelieSkateAnim()
    {
        animator.Play("A_Wheelie");
    }

    public void StartPopShoveitSkateAnim()
    {
        animator.Play("A_PopShoveit");
    }
    public void StartHospitalFlipSkateAnim()
    {
        animator.Play("A_HospitalFlip");
    }

    public void StartRailGrindSkateAnim()
    {
        animator.Play("A_Railgrind");
    }

    /*---------------SNAIL-ANIMATIONS---------------*/

    public void StartOllieSnailAnim()
    {
        animator.Play("A_OllieBaked");
    }

    public void StartRailGrindSnailAnim()
    {
        animator.Play("A_RailGrindBaked");
    }

}
