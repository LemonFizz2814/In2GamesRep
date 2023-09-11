using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class LayoutManager : MonoBehaviour
{
    [SerializeField] private bool stopLayoutManager;
    [Space]
    [SerializeField] private List<Layout> mainMenuLayouts = new List<Layout>();
    [SerializeField] private List<Layout> dialogueLayouts = new List<Layout>();
    [Space]
    [SerializeField] private List<GameObject> portraitUILayouts = new List<GameObject>();
    [SerializeField] private List<GameObject> landscapeUILayouts = new List<GameObject>();

    // private variables
    private float previousAspectRatio;

    [System.Serializable]
    struct Layout
    {
        public RectTransform uiElement;
        public RectTransform portrait;
        public RectTransform landscape;
        public RectTransform menu;
    }

    private void LateUpdate()
    {
        if (stopLayoutManager)
            return;

        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        float aspectRatio = Mathf.Round(((float)screenWidth / (float)screenHeight) * 100) * 0.01f;

        // check if the game view resolution changed
        if (aspectRatio != previousAspectRatio)
        {
            bool isPortrait = aspectRatio <= 1;

            // move ui elements to their new position
            for (int i = mainMenuLayouts.Count - 1; i >= 0; i--)
            {
                StartCoroutine(RepositionUIElement(isPortrait, mainMenuLayouts[i]));
            }
            for (int i = dialogueLayouts.Count - 1; i >= 0; i--)
            {
                StartCoroutine(RepositionUIElement(isPortrait, dialogueLayouts[i]));
            }

            // enable and disable layouts that aren't being displayed
            foreach (GameObject layouts in portraitUILayouts)
            {
                layouts.SetActive(isPortrait);
            }
            foreach (GameObject layouts in landscapeUILayouts)
            {
                layouts.SetActive(!isPortrait);
            }
        }

        previousAspectRatio = aspectRatio;
    }

    private IEnumerator RepositionUIElement(bool _isPortrait, Layout _layout)
    {
        _layout.uiElement.SetParent((_isPortrait) ? _layout.portrait : _layout.landscape);

        _layout.uiElement.offsetMin = Vector2.zero;
        _layout.uiElement.offsetMax = Vector2.zero;

        // delay to give time for rectransform to update appropriately
        yield return new WaitForSeconds(0.1f);

        _layout.uiElement.SetParent(_layout.menu);
        _layout.uiElement.SetSiblingIndex(0);
    }
}
