using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teammate : MonoBehaviour
{
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

        if (readytotalk == true)
        {
            if (talkinput == true && istalking == false)
            {
                istalking = true;
                dialoguemanager.DialogueStart(gameObject.GetComponent<Dialogue>());
                StartCoroutine(timer());
                //dialoguemanager.NextSentence();
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
        }
    }

    IEnumerator timer()
    {
        timerbool = false;
        yield return new WaitForSeconds(0.1f);
        timerbool = true;
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            talkboard.SetActive(false);
            readytotalk = false;
            istalking = false;
            dialoguemanager.DialogueReset();
        }
    }
}
