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

    public void Start()
    {
        talkboard = gameObject.transform.GetChild(0).gameObject;
        dialoguemanager = GameObject.Find("MainCanvas").gameObject.GetComponent<DialogueManager>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            talkboard.SetActive(true);
            readytotalk = true;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (readytotalk == true)
        {
            if (Input.GetKeyDown(KeyCode.Z) && istalking == false)
            {
                istalking = true;
                Debug.Log("talk");
                dialoguemanager.DialogueStart(gameObject.GetComponent<Dialogue>());
                StartCoroutine(timer());
                dialoguemanager.NextSentence();
            }
            if (Input.GetKeyDown(KeyCode.Z) && istalking == true && dialoguemanager.FirstDialogue == false)
            {
                if (timerbool == true)
                {
                    StartCoroutine(timer());
                    Debug.Log("next");
                    dialoguemanager.NextSentence();
                }
            }
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
        }
    }
}
