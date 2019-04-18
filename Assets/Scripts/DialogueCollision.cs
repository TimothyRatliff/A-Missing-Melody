using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCollision : MonoBehaviour
{

    public Dialogue dialogue;
    private bool onePass = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !onePass)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            
            onePass = true;
            Pause();


        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)&& FindObjectOfType<DialogueManager>().animator.GetBool("isOpen").Equals(true))
        {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
        }
        if (FindObjectOfType<DialogueManager>().animator.GetBool("isOpen").Equals(false)&&onePass)
        {
            Resume();
        }
    }


    void Pause()
    {
        Time.timeScale = 0f;
    }

    void Resume()
    {
        Time.timeScale = 1f;
    }
}
