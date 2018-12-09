using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class DishListSectionManager : MonoBehaviour {

    private MenuSection section = null;

    private DishSlider dishSlider;
    private DishUIManager dishUIManager;

    private int activeDishIndex = -1;

	void Awake () {
        dishSlider = GameObject.Find("DishSlider").GetComponent<DishSlider>();
        dishUIManager = GameObject.Find("DishUIManager").GetComponent<DishUIManager>();
    }

    public void Activate(MenuSection section)
    {
        this.section = section;

        activeDishIndex = 0;
        SetActiveElement();
    }

    private void SetActiveElement()
    {
        dishUIManager.Activate(section.Dishes[activeDishIndex].DishName, section.Dishes[activeDishIndex].Price);

        dishSlider.Activate(section.Dishes.Select(d => d.ModelPath).ToArray());
    }

    public void SetActiveDish(int dishIndex)
    {
        activeDishIndex = dishIndex;

        dishUIManager.Activate(section.Dishes[activeDishIndex].DishName, section.Dishes[activeDishIndex].Price);
    }

    public void Deactivate()
    {
        dishUIManager.Deactivate();
        dishSlider.Deactivate();
    }
}