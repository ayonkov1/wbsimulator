/*
    The script is attached to a popup. It is used to trigger an animation of a popup appearing
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpSystem : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        SetInactive();
    }
    public void PopUp(int seconds)
    {
        SetActive();
        animator.SetTrigger("PopUpChangeState");
        StartCoroutine(LateCall(seconds));
    }


    //sets the trigger after some seconds so the popup disappears fro the screen
    IEnumerator LateCall(int seconds)
    {

        yield return new WaitForSeconds(seconds);
        animator.SetTrigger("PopUpChangeState");
        //sets inactive after some time to prevent animation interruption
        yield return new WaitForSeconds(1);
        SetInactive();

    }

    public void SetInactive()
    {
        gameObject.SetActive(false);
    }

    public void SetActive()
    {
        gameObject.SetActive(true);
    }
}
