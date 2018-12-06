using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DishUIManager : MonoBehaviour {

    public string DishName;
    public string DishPrice;

    private GameObject canvas;
    private Text nameText;
    private Text priceText;
    	
	void Awake () {
        canvas = GameObject.Find("MenuItemCanvas");
        if (canvas != null)
        {
            CanvasScaler canvasScaler = canvas.GetComponent<CanvasScaler>();
            if (canvasScaler != null)
            {
                canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            }
        }

        nameText = GameObject.Find("DishNameText").GetComponent<Text>();
        priceText = GameObject.Find("PriceText").GetComponent<Text>();

        canvas.SetActive(false);
    }

    public void FillData(string name, float price)
    {
        DishName = name;
        DishPrice = String.Format("Price: {0} UAH", String.Format("{0:0.00}", price));

        nameText.text = DishName;
        priceText.text = DishPrice;

        canvas.SetActive(true);
    }
}
