using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Text references")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI startText;
    [Space]

    [Header("Script references")]
    [SerializeField] private LocalizationManager localizationManager;

    public void SetLanguageOfText()
    {
        titleText.text = localizationManager.GetMainMenuText().title;
        startText.text = localizationManager.GetMainMenuText().start;
    }
}
