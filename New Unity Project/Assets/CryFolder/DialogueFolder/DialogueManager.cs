using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text DialogueText;
    public string fulltext;
    public string currenttext;
    public Dialogue[] DialogueSet;
    public int DialogueSetOrder;
    public bool FirstDialogue;
    public bool displaying;
    public Dialogue currentDialogue;
    public GameObject optionpanel;
    public GameObject[] optionbuttons;
    public Text[] optiontext;

    [SerializeField] public Queue<string> sentences = new Queue<string>();
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
        NextSentence();
        DialogueSet = dialogue.gameObject.GetComponents<Dialogue>();
    }

    public void NextSentence()
    {
        //Clearing of the sentence;
        if (sentences.Count == 0)
        {
            if (currentDialogue.optionCheck == true)
            {
                for (int i = 0; i < currentDialogue.options.Length; i++)
                {
                    Debug.Log("option " + i + " is " + currentDialogue.options[i]);
                    optionbuttons[i] = optionpanel.transform.GetChild(i).gameObject;
                    optiontext[i] = optionbuttons[i].transform.GetChild(0).gameObject.GetComponent<Text>();
                    optiontext[i].text = currentDialogue.options[i];
                }
                optionpanel.SetActive(true);
            }
            else
            {
                //reset
                FirstDialogue = true;
                currenttext = "";
                DialogueText.text = currenttext;
            }
        }

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

    IEnumerator DisplayDialogue() {
        displaying = true;
        //keyboard typing display style
        for (int i = 0; i < fulltext.Length; i ++)
        {
            if (displaying == true)
            {
                currenttext = fulltext.Substring(0, i + 1);
                DialogueText.text = currenttext;
                yield return new WaitForSeconds(0.3f);
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
