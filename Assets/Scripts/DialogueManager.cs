using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using static LocalizationManager;

public class DialogueManager : MonoBehaviour
{
    [Header("Text references")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [Space]

    [Header("Script Reference")]
    [SerializeField] private LocalizationManager localizationManager;

    // private variables
    private int currentSentence = 0;
    private int maxSentences;

    private void Start()
    {
        maxSentences = localizationManager.GetConversation1().Length;
    }

    public void RestartConversation()
    {
        UpdateConversationText();
    }

    void UpdateConversationText()
    {
        Conversation1 conversation1 = localizationManager.GetConversation1()[currentSentence];

        nameText.text = conversation1.name;
        dialogueText.text = conversation1.dialogue;
    }

    public void NextDialogue()
    {
        currentSentence++;

        if (currentSentence < maxSentences)
        {
            UpdateConversationText();
        }
    }
}