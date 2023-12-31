using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Menu objects")]
    [SerializeField] private GameObject mainMenuObject;
    [SerializeField] private GameObject dialogueMenuObject;
    [Space]

    [Header("Script reference")]
    [SerializeField] private DialogueManager dialogueManager;

    private void Awake()
    {
        Screen.SetResolution(2560, 1440, FullScreenMode.ExclusiveFullScreen);
        DisplayMainMenu();
    }

    public void DisplayMainMenu()
    {
        mainMenuObject.SetActive(true);
        dialogueMenuObject.SetActive(false);
    }
    public void DisplayDialogueMenu()
    {
        mainMenuObject.SetActive(false);
        dialogueManager.RestartConversation();
        dialogueMenuObject.SetActive(true);
    }

    // get functions
    public GameObject GetMainMenuObject()
    {
        return mainMenuObject;
    }
    public GameObject GetDialogueMenuObject()
    {
        return dialogueMenuObject;
    }
}
