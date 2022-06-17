using System;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    private Animator Animator;
    void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player"){
            return;
        }

        Open();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player"){
            return;
        }

        Close();
    }

    public void Open()
    {
        Animator.SetBool("IsOpen", true);
    }

    public void Close()
    {
        Animator.SetBool("IsOpen", false);
    }
}