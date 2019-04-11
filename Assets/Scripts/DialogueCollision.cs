using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCollision : MonoBehaviour
{

    public Dialogue dialogue;
    private bool onePass;
    private bool gameIsPaused = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !onePass)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            onePass = true;
            if (gameIsPaused)
            {
               // Resume();
            }
            else {
                //Pause();
            }
        }
    }

   /* void Pause() {
        Time.timeScale = 0f;
    }

    void Resume() {
        Time.timeScale = 1f;
    }*/
}
