﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour {

    public GameObject Life;
    public GameObject Hunger;
    public Color BarColor;

    private int timeMaxFaim = 3600;
    private int currentTimeFaim = 0;
	private GameObject Judy;
	private Actions actions;
    private GameObject forms;

    // Use this for initialization
    void Start () {
		Judy = GameObject.FindWithTag ("Player");
        forms = GameObject.Find("Forms");
		actions = Judy.GetComponent ("Actions") as Actions;
        Life.transform.Find("Mask").Find("Sprite").GetComponent<Image>().color = Color.red;
	}

    // Update is called once per frame (60 frames)
    void FixedUpdate () {
        //*****************PERTE DE VIE******************//
        //Si Judy a sa barre de vie à 0 : Mort*
        //Life.GetComponent<Scrollbar>().size -= 0.005f;
        if (Life.GetComponent<Scrollbar>().size <= 0f)
        {
            actions.Death();
        }

        //Si Judy chute 
       if (Judy.GetComponent<Rigidbody>().velocity.y < 0 && Judy.GetComponent<Rigidbody>().velocity.magnitude > 100f)
        {
            Life.GetComponent<Scrollbar>().size -= 0.005f; 
        }

        //Si la barre de faim est vide
        if(Hunger.GetComponent<Scrollbar>().size == 0f)
        {
            if(currentTimeFaim == timeMaxFaim)
            {
                Life.GetComponent<Scrollbar>().size -= 0.1f; //valeur en pourcent (0.1f = 10%)
                currentTimeFaim = 0;
            }
            currentTimeFaim++;
        }

    }
    //*****************RECUPERATION DE VIE******************//
    void JudyEatsSomething(float lifeBack)
    {
        if (Hunger.GetComponent<Scrollbar>().size >= 1f)
        {
            Life.GetComponent<Scrollbar>().size += lifeBack;
        } else
        {
            Hunger.GetComponent<Scrollbar>().size += lifeBack;
        }
    }

    //*****************SE FAIT ATTAQUER******************//
    void JudyIsHurtByAnAnimal(float lifeLoosed)
    {
        //si forme puma, 50% de degats en plus
        if (forms.GetComponent<Forms>().currentForm == (int)Forms.forms.puma)
        {
            Life.GetComponent<Scrollbar>().size -= lifeLoosed + lifeLoosed *0.5f;
        }
        else if (forms.GetComponent<Forms>().currentForm == (int)Forms.forms.human)
        {
            Life.GetComponent<Scrollbar>().size -= lifeLoosed;
        } //si forme ours, 25% de degats en moins
        else if (forms.GetComponent<Forms>().currentForm == (int)Forms.forms.bear)
        {
            Life.GetComponent<Scrollbar>().size -= lifeLoosed - lifeLoosed*0.25f;
        }
    }
}
