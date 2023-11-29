using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class OpenPausePanel : MonoBehaviour
{
    private Animator animator;
    public GameObject _gameObject;
    public void ChoseAnimaion(int value)
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("switchAnim", value);
    }

    public void ActiveObjectFalse()
    {
        _gameObject.SetActive(false);
    }

    public void ActiveObjectTrue()
    {
        _gameObject.SetActive(true);
    }

    public void ActiveFalse()
    {
        this.gameObject.SetActive(false);
    }
}
