using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PanelsController : MonoBehaviour
{
    public GameObject buttonPause;
    public GameObject buttonSettings;
    public GameObject buttonQuestion;
    public GameObject pauseBlurPanel;
    private Animator animator;

    public void ChoseAnimation(int value)
    {
        animator = GetComponent<Animator>();
        animator.SetInteger("switchAnim", value);
    }

    public void UnclickableButtons()
    {
        buttonPause.GetComponent<Button>().interactable = false;
        buttonSettings.GetComponent<Button>().interactable = false;
        buttonQuestion.GetComponent<Button>().interactable = false;
        pauseBlurPanel.SetActive(true);
    }

    public void ClickableButtons()
    {
        buttonPause.GetComponent<Button>().interactable = true;
        buttonSettings.GetComponent<Button>().interactable = true;
        buttonQuestion.GetComponent<Button>().interactable = true;
        pauseBlurPanel.SetActive(false);
    }

    public void ActiveFalseAndClickableButtons()
    {
        ClickableButtons();
        this.gameObject.SetActive(false);
    }

    public void ActiveFalse()
    {
        this.gameObject.SetActive(false);
    }
}
