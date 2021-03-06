﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonFireSwitch : Switch {

    override public IEnumerator PlayCutSceneStart() {
        ActivateSwitch();
        yield return new WaitForSeconds(3.6f);
        // Enable the save menu
        GameObject.Find("Affichages/Menus/Menu_sauvegarder").SetActive(!GameObject.Find("Affichages/Menus/Menu_sauvegarder").activeSelf);

        if (gameObject.GetComponent<SwitchObject>() != null) {
            // Activate all his children
            gameObject.GetComponent<SwitchObject>().ActivateChildren();
        }
    }

    override public IEnumerator PlayCutSceneEnd() {
        DiactivateSwitch();
        GameObject judy = GameObject.FindWithTag("Player");
        yield return new WaitForSeconds(4.4f);
        judy.GetComponent<MovementController>().setIsSaving(false);
        StopCutScene();

        if (gameObject.GetComponent<SwitchObject>() != null) {
            // Activate all his children
            gameObject.GetComponent<SwitchObject>().ActivateChildren();
        }
    }

    // The switch does what he's meant for here.
    override protected void ActivateSwitch() {
        GameObject judy = GameObject.FindWithTag("Player");
        judy.GetComponent<ActionsNew>().SitDown();
        // Remove control of judy
        judy.GetComponent<MovementController>().setIsSaving(true);
    }

    override protected void DiactivateSwitch() {
        GameObject judy = GameObject.FindWithTag("Player");
        judy.GetComponent<ActionsNew>().StandUp();
        // Disable the save menu
        GameObject.Find("Affichages/Menus/Menu_sauvegarder").SetActive(!GameObject.Find("Affichages/Menus/Menu_sauvegarder").activeSelf);
    }
}
