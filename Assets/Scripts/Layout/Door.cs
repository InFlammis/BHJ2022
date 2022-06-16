using System;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public bool IsOpen;

    public bool AlwaysOpen;

    public float CloseDelayTime = 1;

    private Animator Animator;
    private Collider2D OpenCollider;

    void Awake()
    {
        Animator = GetComponent<Animator>();
        OpenCollider = GetComponent<Collider2D>();
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

    void Update()
    {
       // Animator.SetBool("IsOpen", IsOpen);
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