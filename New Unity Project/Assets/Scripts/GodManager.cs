using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;

public class GodManager : MonoBehaviour {

    public string jsonInput;

    private SectionManager sectionManager;

	void Start () {
        sectionManager = GameObject.Find("SectionManager").GetComponent<SectionManager>();

        SetTempData();

        SetComponentsData();       
    }
		
	void Update () { }

    private void SetTempData()
    {
        jsonInput = @"{
	                    ""Sections"": [
		                    {
			                    ""SectionName"": ""section 1"",
			                    ""Dishes"": [
					                    {
						                    ""DishName"": ""Dish 1"",
						                    ""ModelPath"": ""cube"",
						                    ""Price"": 1.25
					                    },
					                    {
						                    ""DishName"": ""Dish 2"",
						                    ""ModelPath"": ""dessert"",
						                    ""Price"": 3.45
					                    },
					                    {
						                    ""DishName"": ""Dish 3"",
						                    ""ModelPath"": ""chocolateCake"",
						                    ""Price"": 4
					                    },
					                    {
						                    ""DishName"": ""Dish 4"",
						                    ""ModelPath"": ""soup"",
						                    ""Price"":11.9
					                    }
			                    ]
		                    }
	                    ]	
                    }";
    }

    private void SetComponentsData()
    {
        sectionManager.FillData(JsonUtility.FromJson<Menu>(jsonInput));
    }
}


[Serializable]
public class Dish
{
    public string DishName;
    public string ModelPath;
    public float Price;
}

[Serializable]
public class Section
{
    public string SectionName;
    public List<Dish> Dishes;
}

[Serializable]
public class Menu
{
    public List<Section> Sections;
}