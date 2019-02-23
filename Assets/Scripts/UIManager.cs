using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;

public class UIManager : MonoBehaviour
{
    public GameObject ContainerNPC;
    public GameObject ContainerPlayer;
    public Text text_NPC;
    public Text[] text_Choices;



    // Start is called before the first frame update
    void Start()
    {
        ContainerNPC.SetActive(false);
        ContainerPlayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(!VD.isActive)
            {
                Begin();
            }
            else
            {
                VD.Next();
            }
        }
    }

    void Begin()
    {
        VD.OnNodeChange += UpdateUI;
        VD.OnEnd += End;
        VD.BeginDialogue(GetComponent<VIDE_Assign>());
    }

    void UpdateUI(VD.NodeData data)
    {
        ContainerNPC.SetActive(false);
        ContainerPlayer.SetActive(false);

        if(data.isPlayer)
        {
            ContainerPlayer.SetActive(true);

            for(int i = 0; i< text_Choices.Length; i++)
            {
                if(i < data.comments.Length)
                {
                    text_Choices[i].transform.parent.gameObject.SetActive(true);
                    text_Choices[i].text = data.comments[i];
                }
                else
                {
                    text_Choices[i].transform.parent.gameObject.SetActive(false);
                }
            }

        }
        else
        {
            ContainerNPC.SetActive(true);
            text_NPC.text = data.comments[data.commentIndex];
        }
    }

    void End(VD.NodeData data)
    {
        ContainerNPC.SetActive(false);
        ContainerPlayer.SetActive(false);

        VD.OnNodeChange -= UpdateUI;
        VD.OnEnd -= End;
        VD.EndDialogue();
    }

    void OnDisable()
    {
        if(ContainerNPC != null)
            End(null);
    }

    public void SetPlayerChoice(int choice)
    {
        VD.nodeData.commentIndex = choice;
        if(Input.GetMouseButtonUp(0))
            VD.Next();
    }
}
