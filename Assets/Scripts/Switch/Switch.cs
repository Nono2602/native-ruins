﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switch : MonoBehaviour {

    protected Camera playerCamera;
    protected bool isActived;

    public Camera cameraCutScene;

    void Awake() {
        // Get the camera of the player
        playerCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Use this for initialization
    void Start() {
        isActived = false;
        if (cameraCutScene != null )
            cameraCutScene.enabled = false;
    }

    public void Activate() {
        // If the switch got a camera, switch to activate it
        if (cameraCutScene != null && !isActived) {
            // Call a cut-scene to start the switch
            SetupCutSceneStart();
            StartCoroutine("PlayCutSceneStart");
        }
        else if (cameraCutScene != null && isActived) {
            // Call a cut-scene to end the switch
            StartCoroutine("PlayCutSceneEnd");
        } else {
            // Simply activate the desired switch
            ActivateSwitch();
        }
    }

    // Used to launch the real mechanism
    protected abstract void ActivateSwitch();

    // Used to cancel the mechanism
    protected virtual void DiactivateSwitch() { }

    // Used to setup a cutscene
    public virtual void SetupCutSceneStart() {
        isActived = true;
        //GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<MovementController>().enabled = false;
        ActionsNew actions = GameObject.FindWithTag("Player").GetComponent<ActionsNew>();
        if (actions != null) {
            actions.Stay(100f);
        }
        
        // Enable the right camera
        playerCamera.enabled = false;
        cameraCutScene.enabled = true;
    }

    public virtual IEnumerator PlayCutSceneStart() {
        yield return null;
    }

    public virtual IEnumerator PlayCutSceneEnd()
    {
        yield return null;
    }

    public virtual void StopCutScene() {
        cameraCutScene.enabled = false;
        playerCamera.enabled = true;
        GameObject.FindWithTag("Player").GetComponent<MovementController>().enabled = true;
    }

}
