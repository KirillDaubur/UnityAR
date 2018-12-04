using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class SectionManager : MonoBehaviour {

    public Menu Menu = null;

    private HorizontalSwipe swipeControl;
    private DishUIManager dishUIManager;

    private int activeSectionIndex = -1;
    private int activeDishIndex = -1;

	void Awake () {
        swipeControl = GameObject.Find("ItemSlider").GetComponent<HorizontalSwipe>();
        dishUIManager = GameObject.Find("DishUIManager").GetComponent<DishUIManager>();
    }

    public void FillData(Menu menu)
    {
        Menu = menu;
        SetActiveElement(0, 0);
    }

    public void SetActiveElement(int sectionIndex, int dishIndex)
    {
       
        activeSectionIndex = sectionIndex;
        activeDishIndex = dishIndex;

        dishUIManager.FillData(Menu.Sections[activeSectionIndex].Dishes[activeDishIndex].DishName, Menu.Sections[activeSectionIndex].Dishes[activeDishIndex].Price);

        swipeControl.FillData(Menu.Sections[0].Dishes.Select(d => d.ModelPath).ToArray());
    }

    public void SetActiveDish(int dishIndex)
    {
        activeDishIndex = dishIndex;

        dishUIManager.FillData(Menu.Sections[activeSectionIndex].Dishes[activeDishIndex].DishName, Menu.Sections[activeSectionIndex].Dishes[activeDishIndex].Price);
    }
}