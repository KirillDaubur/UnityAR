using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DishSlider : MonoBehaviour
{
    private string[] menuItemNames = null /*= new string[] { "Cube", "chocolateCake", "dessert", "soup" }*/;

    private DishListSectionManager dishListSectionManager;

    private SwipeBehavior swipeControls;
    private MenuSectionManager sectionManager;

    private GameObject currentItem;
    private GameObject previousItem = null;
    private GameObject nextItem = null;
    private List<GameObject> menuItems;
    private int currentIndex = 0;
    private Vector3 desiredPosition;

    private bool IsTransforminging = false;
    private bool isActive { get {
            return menuItemNames != null && menuItems != null;
        }
    }

    void Start()
    {
        dishListSectionManager = GameObject.Find("DishListSectionManager").GetComponent<DishListSectionManager>();
        sectionManager = GameObject.Find("MenuSectionManager").GetComponent<MenuSectionManager>();
        swipeControls = GameObject.Find("SwipeBehavior").GetComponent<SwipeBehavior>();
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }

        if (swipeControls.SwipeLeft && nextItem != null)
        {
            desiredPosition += new Vector3(-5, 0, 0);
            IsTransforminging = true;
        }
        if (swipeControls.SwipeRight && previousItem != null)
        {
            desiredPosition += new Vector3(5, 0, 0);
            IsTransforminging = true;
        }


        if (IsTransforminging)
        {
            TransformItems();

            if (currentItem.transform.localScale.x < 0 || currentItem.transform.localScale.x > 1)
            {
                IsTransforminging = false;
                if (swipeControls.Direction == SwipeDirection.Left)
                {
                    currentIndex++;
                    dishListSectionManager.SetActiveDish(currentIndex);
                }
                else if (swipeControls.Direction == SwipeDirection.Right)
                {
                    currentIndex--;
                    dishListSectionManager.SetActiveDish(currentIndex);
                }

                previousItem = SetItem(ItemPosition.Previous);
                currentItem = SetItem(ItemPosition.Current);
                nextItem = SetItem(ItemPosition.Next);
            }
        }
        else
        {
            RotateCurrentItem();
        }      
    }

    #region PublicMethods

    public void Activate(string[] dishPathes)
    {
        menuItemNames = dishPathes;
        currentIndex = 0;

        InstantiateAllItems();
        currentItem = SetItem(ItemPosition.Current);
        nextItem = SetItem(ItemPosition.Next);
    }

    public void Deactivate()
    {
        menuItemNames = null;

        for (int i = 0; i < menuItems.Count; i++)
        {
            Destroy(menuItems[i].gameObject);
        }

        menuItems = null;
    }

    #endregion

    #region PrivateMethods

    private void TransformItems()
    {
        if(swipeControls.Direction == SwipeDirection.Left)
        {
            if (currentItem != null)
            {
                currentItem.transform.position -= new Vector3(15f * Time.deltaTime, 0, 0);
                currentItem.transform.localScale -= new Vector3(2f * Time.deltaTime, 2f * Time.deltaTime, 2f * Time.deltaTime);
            }

            if (nextItem != null)
            {
                nextItem.transform.position -= new Vector3(15f * Time.deltaTime, 0, 0);
                nextItem.transform.localScale += new Vector3(2f * Time.deltaTime, 2f * Time.deltaTime, 2f * Time.deltaTime);
            }
        }
        else if (swipeControls.Direction == SwipeDirection.Right)
        {
            if (currentItem != null)
            {
                currentItem.transform.position += new Vector3(15f * Time.deltaTime, 0, 0);
                currentItem.transform.localScale -= new Vector3(2f * Time.deltaTime, 2f * Time.deltaTime, 2f * Time.deltaTime);
            }

            if (previousItem != null)
            {
                previousItem.transform.position += new Vector3(15f * Time.deltaTime, 0, 0);
                previousItem.transform.localScale += new Vector3(2f * Time.deltaTime, 2f * Time.deltaTime, 2f * Time.deltaTime);
            }
        }
    }

    private void RotateCurrentItem()
    {
        menuItems[currentIndex].transform.Rotate(Vector3.up,10f * Time.deltaTime);
    }

    private GameObject SetItem(ItemPosition position)
    {
        switch (position){
            case ItemPosition.Previous:
                if(currentIndex > 0)
                {
                    menuItems[currentIndex - 1].transform.localPosition = new Vector3(-7.77002f, 0, 0);
                    menuItems[currentIndex - 1].transform.localScale = new Vector3(0, 0, 0);
                    return menuItems[currentIndex - 1];
                }
                return null;
            case ItemPosition.Current:
                menuItems[currentIndex].transform.localPosition = new Vector3(0, 0, 0);
                menuItems[currentIndex].transform.localScale = new Vector3(1, 1, 1);
                return menuItems[currentIndex];
            case ItemPosition.Next:
                if (currentIndex < menuItemNames.Length - 1)
                {
                    menuItems[currentIndex + 1].transform.localPosition = new Vector3(7.77002f, 0, 0);
                    menuItems[currentIndex + 1].transform.localScale = new Vector3(0, 0, 0);
                    return menuItems[currentIndex + 1];
                }
                return null;
            default:
                return null;
        }
    }

    private void InstantiateAllItems()
    {
        menuItems = new List<GameObject>();

        GameObject gameItem;
        for (int i = 0; i < menuItemNames.Length; i++)
        {
            gameItem = Instantiate(Resources.Load(menuItemNames[i]) as GameObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            gameItem.transform.localScale = new Vector3(0, 0, 0);
            menuItems.Add(gameItem);
        }
    }

    #endregion
}

public enum ItemPosition
{
    Previous, Current, Next
}
