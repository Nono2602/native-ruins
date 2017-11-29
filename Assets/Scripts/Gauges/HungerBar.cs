﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour {

    public GameObject Hunger;
    public Color BarColor;

    private int currentTimeFaim = 0;
    private int timeMaxFaim = 1500;
	private GameObject forms;

	// Use this for initialization
	void Start () {
        Hunger.transform.Find("Mask").Find("Sprite").GetComponent<Image>().color = Color.green;
    }


	
	// Update is called once per frame
	void Update () {
        GameObject playerRoot = GameObject.Find("Player");
        if (currentTimeFaim == timeMaxFaim)
        {
            if (playerRoot.GetComponent<FormsController>().getCurrentForm() == (int)Forms.id_puma)
            {
                Hunger.GetComponent<Scrollbar>().size -= 0.06f;
            }
            else
            {
                Hunger.GetComponent<Scrollbar>().size -= 0.02f;
            }
            currentTimeFaim = 0;
        }
        currentTimeFaim++;
    }

    public float getSizeHungerBar()
    {
        return Hunger.GetComponent<Scrollbar>().size;
    }

    public void setSizeHungerBar(float size)
    {
        Hunger.GetComponent<Scrollbar>().size = size;
    }
}
