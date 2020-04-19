using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text DialogueText;
    public Text SpeakerName;
    public string fulltext;
    public string currenttext;
    public Dialogue[] DialogueSet;
    public int DialogueSetOrder;
    public bool FirstDialogue;
    public bool displaying;
    public Dialogue currentDialogue;
    public GameObject dialoguepanel;
    public GameObject optionpanel;
    public GameObject[] optionbuttons;
    public Text[] optiontext;
    public Queue<string> sentences = new Queue<string>();

    //item system
    public GameObject currentNPC;
    public NPC currentNPCstat;
    public PlayerInventory playerinventory;

    public void DialogueStart(Dialogue dialogue)
    {
        if (FirstDialogue == true)
        {
            DialogueSetOrder = 0;
            FirstDialogue = false;
        }
        currentDialogue = dialogue;
        sentences.Clear();
        //putting sentence inside dialogue class into the queue
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        dialoguepanel.SetActive(true);
        currentNPCstat = currentNPC.GetComponent<NPC>();
        NextSentence();
        DialogueSet = dialogue.gameObject.GetComponents<Dialogue>();
        SpeakerName.text = dialogue.NPC_name;
    }

    public void NextSentence()
    {
        //Clearing of the sentence;
        if (sentences.Count == 0)
        {
            //item system (Getting item from NPC)
            if(currentDialogue.HandCarry.Count >= 1)
            {
                Debug.Log("hand carry");
                playerinventory  = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
                playerinventory.additem(currentDialogue.HandCarry[0].ID);
            }

            if (currentDialogue.optionCheck == true)
            {
                optionbuttons = new GameObject[currentDialogue.options.Length];
                optiontext = new Text[currentDialogue.options.Length];
                for (int i = 0; i < currentDialogue.options.Length; i++)
                {
                    Debug.Log("option " + i + " is " + currentDialogue.options[i]);
                    optionbuttons[i] = optionpanel.transform.GetChild(i).gameObject;
                    optionbuttons[i].SetActive(true);
                    optiontext[i] = optionbuttons[i].transform.GetChild(0).gameObject.GetComponent<Text>();
                    optiontext[i].text = currentDialogue.options[i];
                }
                optionpanel.SetActive(true);
            }
            else
            {
                currentNPCstat.talked = true;
                DialogueReset();
                ////reset
                //dialoguepanel.SetActive(false);
                //FirstDialogue = true;
                //currenttext = "";
                //DialogueText.text = currenttext;
            }
        }
        else
        {
            // Quickly show the whole sentence
            if (displaying == true)
            {
                displaying = false;
                currenttext = fulltext;
                DialogueText.text = currenttext;
            }
            //normal process, display next sentence
            else
            {
                fulltext = sentences.Dequeue();
                StartCoroutine(DisplayDialogue());
            }
        }
    }

    public void DialogueReset()
    {
        //reset
        dialoguepanel.SetActive(false);
        FirstDialogue = true;
        currenttext = "";
        DialogueText.text = currenttext;
        optionpanel.SetActive(false);
        currentNPC = null;
    }

    IEnumerator DisplayDialogue()
    {
        displaying = true;
        //keyboard typing display style
        for (int i = 0; i < fulltext.Length; i ++)
        {
            if (displaying == true)
            {
                currenttext = fulltext.Substring(0, i + 1);
                DialogueText.text = currenttext;
                yield return new WaitForSeconds(0.05f);

                if (currenttext.Length == fulltext.Length && currentDialogue.optionCheck == true && sentences.Count == 0)
                {
                    NextSentence();
                }
            }
        }
        displaying = false;
    }

    //called when option button is onclicked
    public void OptionDetermine(int chosennumber)
    {
        DialogueSetOrder += chosennumber;
        DialogueStart(DialogueSet[DialogueSetOrder]);
        optionpanel.SetActive(false);
    }
}
