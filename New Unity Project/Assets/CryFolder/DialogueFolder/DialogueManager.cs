using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text DialogueText;
    public string fulltext;
    public string currenttext;

    [SerializeField] public Queue<string> sentences = new Queue<string>();
    public void DialogueStart(Dialogue dialogue)
    {
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        NextSentence();
    }

    public void NextSentence()
    {
        if(sentences.Count == 0)
        {
            Debug.Log("end dialogue");
        }
        fulltext = sentences.Dequeue();
        StartCoroutine(DisplayDialogue());
    }

    IEnumerator DisplayDialogue() {
        for (int i = 0; i < fulltext.Length; i ++)
        {
            currenttext = fulltext.Substring(0, i+1);
            DialogueText.text = currenttext;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
