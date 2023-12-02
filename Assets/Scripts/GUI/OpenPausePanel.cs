using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.UI;
using UnityEngine;

public class OpenPausePanel : MonoBehaviour
{
    private Animator animator;
    public GameObject _gameObject;
    public GameObject buttonPause;
    public Image star_1;
    public Image star_2;
    public Image star_3;
    public Slider timer;

    private void StarController()
    {
        if (timer.value / timer.maxValue < 0.25f)
        {
            star_3.gameObject.SetActive(false);
            star_2.gameObject.SetActive(false);
            star_1.gameObject.SetActive(false);
        }
        else if (timer.value / timer.maxValue < 0.5f)
        {
            star_3.gameObject.SetActive(false);
            star_2.gameObject.SetActive(false);
        }
        else if (timer.value / timer.maxValue < 0.75f)
        {
            star_3.gameObject.SetActive(false);
        }
    }

    public void buttonPauseActiveTrue()
    {
        buttonPause.SetActive(true);
    }

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
        buttonPauseActiveTrue();
        this.gameObject.SetActive(false);
    }
}
