using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool talked;

    private GameObject talkboard;
    public bool readytotalk;
    public bool istalking;
    public DialogueManager dialoguemanager;
    public bool timerbool;
    public GameObject test;
    public bool talkinput;
    public void Start()
    {
        talkboard = gameObject.transform.GetChild(0).gameObject;
        dialoguemanager = GameObject.Find("MainCanvas").gameObject.GetComponent<DialogueManager>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            talkinput = true;
        }
        else
        {
            talkinput = false;
        }

        // if collision trigger
        if (readytotalk == true)
        {
            if (talkinput == true && istalking == false)
            {
                if (talked == true)
                {
                    //if talked already
                    Debug.Log("talked already");
                    dialoguemanager.DialogueStart(gameObject.GetComponent<RemainedDialogue>());
                    istalking = true;
                    StartCoroutine(timer());
                }
                else
                {
                    istalking = true;
                    dialoguemanager.DialogueStart(gameObject.GetComponent<Dialogue>());
                    StartCoroutine(timer());
                    //dialoguemanager.NextSentence();
                }
            }

            if (talkinput == true && istalking == true && dialoguemanager.FirstDialogue == false && timerbool == true)
            {
                StartCoroutine(timer());
                dialoguemanager.NextSentence();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            talkboard.SetActive(true);
            readytotalk = true;
            dialoguemanager.currentNPC = this.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            talkboard.SetActive(false);
            readytotalk = false;
            istalking = false;
            dialoguemanager.DialogueReset();
            dialoguemanager.currentNPC = null;
        }
    }
    IEnumerator timer()
    {
        timerbool = false;
        yield return new WaitForSeconds(0.1f);
        timerbool = true;
    }
}
