using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanAnimationController : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void PlayIdle()
    {
        animator.SetBool("isWalking", false);
    }
    public void PlayWalkAnimation(float speed)
    {
        animator.SetBool("isWalking", true);
        animator.SetFloat("Speed", speed);
    }
}
