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
            if (currentSection.SectionName != "Menu")
            {
                ActivateParentSection();
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
                menuSectionManager.Deactivate();
            }
            else
            {
                dishListSectionManager.Deactivate();
            }
        }
    }

    private void SetTempData()
    {
        jsonInput = @"{
""Sections"":[
	{
		""SectionName"": ""Menu"",
		""Dishes"": [],
		""ChildSectionIds"": [1,2]
	},
	{
		""SectionName"": ""Food"",
		""Dishes"": [],
		""ChildSectionIds"": [3,4,5]
	},
	{
		""SectionName"": ""Drinks"",
		""Dishes"": [],
		""ChildSectionIds"": [6,7]
	},
	{
		""SectionName"": ""First"",
		""Dishes"": [],
		""ChildSectionIds"": [8]
	},
	{
		""SectionName"": ""Second"",
		""Dishes"": [],
		""ChildSectionIds"": [9]
	},
	{
		""SectionName"": ""Desserts"",
		""Dishes"": [],
		""ChildSectionIds"": [10]
	},
	{
		""SectionName"": ""Alcohol"",
		""Dishes"": [],
		""ChildSectionIds"": [11]
	},
	{
		""SectionName"": ""NonAlcohol"",
		""Dishes"": [],
		""ChildSectionIds"": [12]
	},
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
		""ChildSectionIds"": []
	},
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
		""ChildSectionIds"": []
	},		
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
		""ChildSectionIds"": []
	},
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
		""ChildSectionIds"": []
	},
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
		""ChildSectionIds"": []
	}
]
}";
    }

    private void SetComponentsData()
    {
       MenuDTO menuDTO = JsonUtility.FromJson<MenuDTO>(jsonInput);

        currentSection = new MenuSection();
        MenuMapper.GetMenuFromDTO(menuDTO.Sections, 0, currentSection, null);

        ActivateCurrentSection();
    }

    private void ActivateParentSection()
    {
        DeactivateCurrentSection();
        currentSection = currentSection.ParentSection;
        ActivateCurrentSection();
    }
}

[Serializable]
public class MenuDTO
{
    public List<MenuSectionDTO> Sections; 
}

[Serializable]
public class MenuSectionDTO
{
    public string SectionName;
    public List<DishDTO> Dishes;
    public List<int> ChildSectionIds;
}

[Serializable]
public class DishDTO
{
    public string DishName;
    public string ModelPath;
    public float Price;
}

public class MenuSection
{
    public MenuSection ParentSection;
    public string SectionName;
    public List<Dish> Dishes;
    public List<MenuSection> ChildSections;

    public bool IsSubmenu { get { return ChildSections != null && ChildSections.Count > 0; } }
}

public class Dish
{
    public string DishName;
    public string ModelPath;
    public float Price;
}

public static class MenuMapper
{
    public static void GetMenuFromDTO(List<MenuSectionDTO> menuDTO, int currentIndex,  MenuSection current, MenuSection parent)
    {
        current.SectionName = menuDTO[currentIndex].SectionName;
        current.Dishes = menuDTO[currentIndex].Dishes.Select(d => new Dish() { DishName = d.DishName, ModelPath = d.ModelPath, Price = d.Price }).ToList();
        current.ParentSection = parent;
        current.ChildSections = new List<MenuSection>();

        for (int i = 0; i < menuDTO[currentIndex].ChildSectionIds.Count; i++)
        {
            current.ChildSections.Add(new MenuSection());
            GetMenuFromDTO(menuDTO, menuDTO[currentIndex].ChildSectionIds[i], current.ChildSections[i], current);
        }
    }    
}