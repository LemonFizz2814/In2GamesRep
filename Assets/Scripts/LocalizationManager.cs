using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

using UnityEngine.UI;
using UnityEngine;

using Newtonsoft.Json;
using TMPro;

public class LocalizationManager : MonoBehaviour
{
    [Header("Object reference")]
    [SerializeField] private TMP_Dropdown languageDropdown;
    [Space]

    [Header("Script reference")]
    [SerializeField] private MainMenuManager mainMenuManager;

    public enum Language
    {
        English,
        Maori,
        Klingon,
        // add more languages as needed
    }

    [System.Serializable]
    public class MainMenuText
    {
        public string title;
        public string start;
    }

    [System.Serializable]
    public class Conversation1
    {
        public string name;
        public string dialogue;
    }

    [System.Serializable]
    public class LocalizedTextList
    {
        public MainMenuText[] mainMenuText;
        public Conversation1[] conversation1;
    }

    LocalizedTextList localizedTextList = new LocalizedTextList();

    // private variables
    private Language currentLanguage;

    private void Awake()
    {
        LanguageSelected(Language.English);
        UpdateLanguageDropDownList();
    }

    // update list if new languages have been added to the enum
    void UpdateLanguageDropDownList()
    {
        languageDropdown.ClearOptions();

        int amountOfLanguages = Enum.GetValues(typeof(Language)).Length;
        List<string> dropDownOptions = new List<string>();

        for (int i = 0; i < amountOfLanguages; i++)
        {
            // convert enum to string and add to list
            string languageString = ((Language)i).ToString();
            dropDownOptions.Add(languageString);
        }

        // add strings to dropdown
        languageDropdown.AddOptions(dropDownOptions);
    }

    public void LanguageSelected(int _language)
    {
        currentLanguage = (Language)_language;
        LoadLocalizedText();
    }
    public void LanguageSelected(Language _language)
    {
        currentLanguage = _language;
        LoadLocalizedText();
    }

    // get text data from JSON file for language
    private void LoadLocalizedText()
    {
        // get string data for the language type
        string jsonFilePath = Path.Combine(Application.dataPath, "Dialogue Data", currentLanguage.ToString() + ".json");

        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);

            localizedTextList = JsonUtility.FromJson<LocalizedTextList>(jsonData);
        }
        else // language JSON file doesn't exist
        {
            Debug.LogWarning($"Localization JSON file not found for {jsonFilePath}");
        }

        SetLanguageOfText();
    }

    void SetLanguageOfText()
    {
        mainMenuManager.SetLanguageOfText();
    }

    // GET functions
    public MainMenuText GetMainMenuText()
    {
        return localizedTextList.mainMenuText[0];
    }
    public Conversation1[] GetConversation1()
    {
        return localizedTextList.conversation1;
    }
}
