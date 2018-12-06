using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class MainMenuManager : MonoBehaviour {

    private List<Section> sections = null;

    private SectionManager sectionManager;
    private GameObject mainMenuCanvasGameObject;
    private RectTransform mainMenuContent;

    private 

    void Awake () {
        sectionManager = GameObject.Find("SectionManager").GetComponent<SectionManager>();
        mainMenuCanvasGameObject = GameObject.Find("MainMenuCanvas")/*.GetComponent<Canvas>()*/;
        mainMenuContent = GameObject.Find("SectionsMenuContent").GetComponent<RectTransform>();
    }

    public void FillData(List<Section> sections)
    {
        this.sections = sections;
        GenerateButtons();
    }

    private void GenerateButtons()
    {
        for (int i = 0; i < sections.Count; i++)
        {
            Section currentSection = sections[i];

            var buttonInstance = Instantiate(Resources.Load("SectionButton") as GameObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            buttonInstance.transform.SetParent(mainMenuContent, false);
            buttonInstance.GetComponentInChildren<Text>().text = currentSection.SectionName;

            int temp = i;

            buttonInstance.GetComponent<Button>().onClick.AddListener(() => ActivateSection(temp));
        }
    }

    private void ActivateSection(int index)
    {
        sectionManager.FillData(sections[index]);
        mainMenuCanvasGameObject.SetActive(false);
    }
}
