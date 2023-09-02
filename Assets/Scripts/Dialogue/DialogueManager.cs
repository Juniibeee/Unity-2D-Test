using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : Singleton<DialogueManager>
{
    #region Public Variables 

    [SerializeField] public GameObject dialogueBox;
    public bool justStarted;

    #endregion

    #region Private Variables

    [SerializeField] private int currentLine;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private GameObject nameBox;
    private string[] dialogueLines;
    private const string startsWithSignifierString = "n-";

    #endregion

    #region Public Methods

    public void ContinueDialogue()
    {
        if (!justStarted)
        {
        currentLine++;
            if (currentLine >= dialogueLines.Length)
            {
                dialogueBox.SetActive(false);
                justStarted = true;
                PlayerController.Instance.canMove = true;
                PlayerController.Instance.canAttack = true;
            }
            else
            {
                CheckIfName();
                dialogueText.text = dialogueLines[currentLine];
            }
        }
        else
        {
            justStarted = false;
        }
    }
    
    // newLines is passed through from the DialogueActivator class that calls this function
    public void ShowDialogue(string[] newLines, bool isPerson) {
        justStarted = true;
        dialogueLines = newLines;
        currentLine = 0;
        CheckIfName();
        dialogueText.text = dialogueLines[currentLine];
        dialogueBox.SetActive(true);
        nameBox.SetActive(isPerson);
        PlayerController.Instance.canMove = false;
        ContinueDialogue();
    }

    // Can signify who's talking in the inspector
    public void CheckIfName() {
        if (dialogueLines[currentLine].StartsWith(startsWithSignifierString)) {
            nameText.text = dialogueLines[currentLine].Replace(startsWithSignifierString, "");
            currentLine++;
        }
    }

    #endregion
}