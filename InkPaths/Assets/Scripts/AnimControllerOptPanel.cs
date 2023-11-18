using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AnimControllerOptPanel : MonoBehaviour
{
    private Animator animator;
    public void ChoseAnimation(int i)
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("switchAnim", i);
    }

    public void InactivePanel()
    {
        gameObject.SetActive(false);
    }
}
