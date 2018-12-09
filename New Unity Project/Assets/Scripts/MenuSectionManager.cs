using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class MenuSectionManager : MonoBehaviour {

    private GodManager godManager;

    private MenuSection section = null;

    private GameObject mainMenuCanvasGameObject;
    private Text SectionName;
    private GameObject mainMenuContent;

    void Awake () {
        godManager = GameObject.Find("GodManager").GetComponent<GodManager>();
        mainMenuCanvasGameObject = GameObject.Find("SectionMenuCanvas");

        SectionName = GameObject.Find("SectionName").GetComponent<Text>();
        mainMenuContent = GameObject.Find("SectionsMenuContent");
    }

    public void Activate(MenuSection section)
    {
        this.section = section;

        SectionName.text = section.SectionName;
        GenerateButtons();
        mainMenuCanvasGameObject.SetActive(true);
    }

    public void Deactivate()
    {
        SectionName.text = "";

        Button[] allChildren = mainMenuContent.GetComponentsInChildren<Button>(true);
        for (var index = 0; index < allChildren.Length; index++)
        {
            Destroy(allChildren[index].gameObject);
        }

        mainMenuCanvasGameObject.SetActive(false);
    }

    private void GenerateButtons()
    {
        RectTransform parentRect = mainMenuContent.GetComponent<RectTransform>();
        for (int i = 0; i < section.ChildSections.Count; i++)
        {
            MenuSection currentSection = section.ChildSections[i];

            var buttonInstance = Instantiate(Resources.Load("SectionButton") as GameObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            buttonInstance.transform.SetParent(parentRect, false);
            buttonInstance.GetComponentInChildren<Text>().text = currentSection.SectionName;

            int childIndex = i;

            buttonInstance.GetComponent<Button>().onClick.AddListener(() => ActivateNextSection(childIndex));
        }
    }

    private void ActivateNextSection(int index)
    {
        godManager.ActivateChildSection(index);

    }
}
