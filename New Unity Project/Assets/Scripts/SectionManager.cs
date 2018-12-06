using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class SectionManager : MonoBehaviour {

    private Section section = null;

    private HorizontalSwipe swipeControl;
    private DishUIManager dishUIManager;

    private int activeDishIndex = -1;

	void Awake () {
        swipeControl = GameObject.Find("ItemSlider").GetComponent<HorizontalSwipe>();
        dishUIManager = GameObject.Find("DishUIManager").GetComponent<DishUIManager>();
    }

    public void FillData(Section section)
    {
        this.section = section;

        activeDishIndex = 0;
        SetActiveElement();
    }

    public void SetActiveElement()
    {
        dishUIManager.FillData(section.Dishes[activeDishIndex].DishName, section.Dishes[activeDishIndex].Price);

        swipeControl.FillData(section.Dishes.Select(d => d.ModelPath).ToArray());
    }

    public void SetActiveDish(int dishIndex)
    {
        activeDishIndex = dishIndex;

        dishUIManager.FillData(section.Dishes[activeDishIndex].DishName, section.Dishes[activeDishIndex].Price);
    }
}