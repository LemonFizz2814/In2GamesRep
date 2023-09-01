using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

using static LocalizationManager;

public class DialogueManager : MonoBehaviour
{
    [Header("Text references")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [Space]

    [Header("Object references")]
    [SerializeField] private CharacterData[] characterSprites;
    [Space]

    [Header("Sprite colours")]
    [SerializeField] private Color32 characterHightlightColour;
    [SerializeField] private Color32 characterDullColour;
    [Space]

    [Header("Script Reference")]
    [SerializeField] private LocalizationManager localizationManager;

    // private variables
    private int currentSentence = 0;
    private int maxSentences;

    [System.Serializable]
    struct CharacterData
    {
        public CharacterNames name;
        public Image uiImage;
        public Sprite sprite;
    }

    enum CharacterNames
    {
        Character1,
        Character2,
    }

    private void Start()
    {
        maxSentences = localizationManager.GetConversation1().Length;
        SetCharacterImages();
    }

    public void RestartConversation()
    {
        UpdateConversationText();
    }

    void UpdateConversationText()
    {
        Conversation1 conversation1 = localizationManager.GetConversation1()[currentSentence];

        // set text
        nameText.text = conversation1.name;
        dialogueText.text = conversation1.dialogue;

        // get CharacterData index by using json name string to get enum CharacterNames index
        int characterIndex = (int)System.Enum.Parse(typeof(CharacterNames), conversation1.name);

        HighlightCharacterTalking(characterIndex);
    }

    public void NextDialogue()
    {
        currentSentence++;

        if (currentSentence < maxSentences)
        {
            UpdateConversationText();
        }
    }

    // set the ui image for characters to active character sprites
    void SetCharacterImages()
    {
        foreach(CharacterData characterSprite in characterSprites)
        {
            characterSprite.uiImage.sprite = characterSprite.sprite;
        }
    }

    // visually highlight character whos dialogue is displayed
    public void HighlightCharacterTalking(int _charactersIndex)
    {
        // set all character images to unhighlighted
        for (int i = 0; i < characterSprites.Length; i++)
        {
            characterSprites[i].uiImage.color = characterDullColour;
        }

        // highlight selected character
        characterSprites[_charactersIndex].uiImage.color = characterHightlightColour;
    }
}