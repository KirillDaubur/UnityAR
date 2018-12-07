using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;

public class GodManager : MonoBehaviour {

    public string jsonInput;

    private MenuSection currentSection;
    private MenuSectionManager menuSectionManager;
    private DishListSectionManager dishListSectionManager;

	void Awake () {
        menuSectionManager = GameObject.Find("MenuSectionManager").GetComponent<MenuSectionManager>();
        dishListSectionManager = GameObject.Find("DishListSectionManager").GetComponent<DishListSectionManager>();

        SetTempData();

        SetComponentsData();       
    }
		
	void Update ()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (currentSection.SectionName == "Menu")
            {
                ActivateParentSection(currentSection, currentSection, currentSection.SectionName);
            }
            else
            {
                Application.Quit();
            }
        }
    }

    public void ActivateChildSection(int index)
    {
        DeactivateCurrentSection();
        currentSection = currentSection.ChildSections[index];
        ActivateCurrentSection();
    }

    private void ActivateCurrentSection()
    {
        if (currentSection != null)
        {
            if (currentSection.IsSubmenu)
            {
                menuSectionManager.Activate(currentSection);
            }
            else
            {
                dishListSectionManager.Activate(currentSection);
            }
        }
    }
    private void DeactivateCurrentSection()
    {
        if (currentSection != null)
        {
            if (currentSection.IsSubmenu)
            {
                dishListSectionManager.Deactivate();
            }
            else
            {
                menuSectionManager.Deactivate();
            }
        }
    }

    private void SetTempData()
    {
        jsonInput = @"{
	""SectionName"": ""Menu"",
	""Dishes"": [],
	""ChildSections"": [{
		""SectionName"": ""Food"",
		""Dishes"": [],
		""ChildSections"": [
		{
			""SectionName"": ""First"",
			""Dishes"": [],
			""ChildSections"": [
			{
				""SectionName"": ""First Dishes"",
				""Dishes"": [
					{
						""DishName"": ""First 1"",
						""ModelPath"": ""soup"",
						""Price"": 1.1
					},
					{
						""DishName"": ""First 2"",
						""ModelPath"": ""soup"",
						""Price"": 1.2
					},
					{
						""DishName"": ""First 3"",
						""ModelPath"": ""soup"",
						""Price"": 1.3
					}
				],
				""ChildSections"": []
			}]
		},
		{
			""SectionName"": ""Second"",
			""Dishes"": [],
			""ChildSections"": [
				{
					""SectionName"": ""Second Dishes"",
					""Dishes"": [
						{
							""DishName"": ""Second 1"",
							""ModelPath"": ""cube"",
							""Price"": 2.1
						},
						{
							""DishName"": ""First 2"",
							""ModelPath"": ""cube"",
							""Price"": 2.2
						},
						{
							""DishName"": ""First 3"",
							""ModelPath"": ""cube"",
							""Price"": 2.3
						}
					],
					""ChildSections"": []
				}
			]
		},
		{
			""SectionName"": ""Desserts"",
			""Dishes"": [],
			""ChildSections"": [
				{
					""SectionName"": ""Dessert Dishes"",
					""Dishes"": [
						{
							""DishName"": ""Dessert 1"",
							""ModelPath"": ""chocolateCake"",
							""Price"": 3.1
						},
						{
							""DishName"": ""Dessert 2"",
							""ModelPath"": ""chocolateCake"",
							""Price"": 3.2
						},
						{
							""DishName"": ""Dessert 3"",
							""ModelPath"": ""chocolateCake"",
							""Price"": 3.3
						}
					],
					""ChildSections"": []
				}
			]
		}]
	},
	{
		""SectionName"": ""Drinks"",
		""Dishes"": [],
		""ChildSections"": [{
			""SectionName"": ""Alcohol"",
			""Dishes"": [],
			""ChildSections"": [
				{
					""SectionName"": ""Alcohol Drinks"",
					""Dishes"": [
						{
							""DishName"": ""Alcohol 1"",
							""ModelPath"": ""chocolateCake"",
							""Price"": 4.1
						},
						{
							""DishName"": ""Alcohol 2"",
							""ModelPath"": ""chocolateCake"",
							""Price"": 4.2
						},
						{
							""DishName"": ""Alcohol 3"",
							""ModelPath"": ""chocolateCake"",
							""Price"": 4.3
						}
					],
					""ChildSections"": []
				}
			]
		},
		{
			""SectionName"": ""NonAlcohol"",
			""Dishes"": [],
			""ChildSections"": [
				{
					""SectionName"": ""Non Alcohol Drinks"",
					""Dishes"": [
						{
							""DishName"": ""Non Alcohol 1"",
							""ModelPath"": ""cube"",
							""Price"": 5.1
						},
						{
							""DishName"": ""Non Alcohol 2"",
							""ModelPath"": ""cube"",
							""Price"": 5.2
						},
						{
							""DishName"": ""Non Alcohol 3"",
							""ModelPath"": ""cube"",
							""Price"": 5.3
						}
					],
					""ChildSections"": []
				}
			]
		}]
	}]
}";
    }

    private void SetComponentsData()
    {
        currentSection = JsonUtility.FromJson<MenuSection>(jsonInput);

        ActivateCurrentSection();
    }

    private void ActivateParentSection(MenuSection currentSec, MenuSection parentSec, string searchSectionName)
    {
        if ((currentSec.ChildSections == null || currentSec.ChildSections.Count == 0)
            && currentSec.SectionName != searchSectionName)
        {
            return;
        }

        if (currentSec.SectionName == searchSectionName)
        {
            DeactivateCurrentSection();
            currentSection = parentSec;
            ActivateCurrentSection();
        }

        for (int i = 0; i < currentSec.ChildSections.Count; i++)
        {
            ActivateParentSection(currentSec.ChildSections[i], currentSec, searchSectionName);
        }
    }
}

[Serializable]
public class MenuSection
{
    public string SectionName;
    [SerializeField]
    public List<Dish> Dishes;
    [SerializeField]
    public List<MenuSection> ChildSections;

    public bool IsSubmenu { get { return ChildSections != null && ChildSections.Count > 0; } }
}

[Serializable]
public class Dish
{
    public string DishName;
    public string ModelPath;
    public float Price;
}