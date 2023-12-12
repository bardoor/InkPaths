using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine.UI;
using UnityEngine;

public class OpenPausePanel : MonoBehaviour
{
    private Animator animator;
    public GameObject buttonPause;
    public GameObject buttonSettings;
    public GameObject buttonQuestion;
    public GameObject pauseBlurPanel;
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

    public void UnclickableButtons()
    {
        buttonPause.GetComponent <Button>().interactable = false;
        buttonSettings.GetComponent <Button>().interactable = false;
        buttonQuestion.GetComponent <Button>().interactable = false;
        pauseBlurPanel.SetActive(true);
    }

    public void ClickableButtons()
    {
        buttonPause.GetComponent <Button>().interactable = true;
        buttonSettings.GetComponent<Button>().interactable = true;
        buttonQuestion.GetComponent<Button>().interactable = true;
        pauseBlurPanel.SetActive(false);
    }

    public void ChoseAnimaion(int value)
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("switchAnim", value);
    }

    public void ActiveFalse()
    {
        ClickableButtons();
        this.gameObject.SetActive(false);
    }
}
