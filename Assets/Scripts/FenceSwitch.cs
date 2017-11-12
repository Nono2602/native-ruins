﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceSwitch : Switch {

    public float smoothing = 1f;
    private Vector3 target;

    // Use this for initialization
    void Start () {
        target = transform.position - transform.up * 17;
    }

    // Used to launch a mechanism
    override public void Activate() {
        StartCoroutine(MyCoroutine(target));
    }

    IEnumerator MyCoroutine(Vector3 position)
    {
        while (Vector3.Distance(transform.position, position) > 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, position, smoothing * Time.deltaTime);

            yield return null;
        }
        yield return new WaitForSeconds(3f);
    }
}
