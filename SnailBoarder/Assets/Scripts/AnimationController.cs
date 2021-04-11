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

    public void StartOllieAnim()
    {
        animator.Play("A_Ollie");
    }

    public void StartKickflipAnim()
    {
        animator.Play("A_Kickflip");
    }

    public void StartWheelieAnim()
    {
        animator.Play("A_Wheelie");
    }

    public void StartPopShoveitAnim()
    {
        animator.Play("A_PopShoveit");
    }
    public void StartHospitalFlipAnim()
    {
        animator.Play("A_HospitalFlip");
    }

    public void StartRailgringAnim()
    {
        animator.Play("A_Railgrind");
    }
}
