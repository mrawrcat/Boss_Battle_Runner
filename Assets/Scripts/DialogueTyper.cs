using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTyper : MonoBehaviour
{
    public string dialogue;
    public Text dialoguetxt;
    public bool isComplete;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type_Sentence(dialogue));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Type_Sentence(string sentence)
    {
        dialoguetxt.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialoguetxt.text += letter;
            yield return null;
            //print("done");
            //isComplete = true;
        }
        yield return StartCoroutine(Make_Complete());
    }

    IEnumerator Make_Complete()
    {
        isComplete = true;
        print("done");
        yield return null;
    }
}
