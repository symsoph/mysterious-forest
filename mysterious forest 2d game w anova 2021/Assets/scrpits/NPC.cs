using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public string NPCText;
    private bool interactable = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Player is in talking range of this NPC
            interactable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactable = false;
            if (dialogueBox.activeInHierarchy == true)
            {
                dialogueBox.SetActive(false);
            }
            // Player is no longer in talking range of the NPC 
        }
    }

    private void Update()
    {
        if (interactable & Input.GetKeyDown(KeyCode.E))
        {
            if (dialogueBox.activeInHierarchy == false)
            {
                dialogueText.text = NPCText;
                dialogueBox.SetActive(true);
            }
        }
    }
}
