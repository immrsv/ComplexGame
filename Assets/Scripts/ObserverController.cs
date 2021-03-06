﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverController : MonoBehaviour {

    public float cameraSpeed;

    private Rigidbody rb;

    private GameObject Selected;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {

    }

    // Update is called once per frame
    void Update() {
        Vector3 move = new Vector3();
        move += Input.GetAxis("Horizontal") * transform.right;
        move += Input.GetAxis("Vertical") * transform.forward;
        move += Input.GetAxis("Jump") * transform.up;
        move *= cameraSpeed;

        rb.velocity = move;

        Vector3 rotate = new Vector3();
        rotate.z = -Input.GetAxis("Roll");

        if (Input.GetMouseButtonDown(2)) { // Middle Mouse toggles mouse capture
            if (Cursor.lockState != CursorLockMode.Locked) {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            } else {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        if (Cursor.lockState == CursorLockMode.Locked) {
            rotate.x = -Input.GetAxisRaw("Mouse Y");
            rotate.y = Input.GetAxisRaw("Mouse X");
        } else {
            HandleInteraction();
        }

        rb.angularVelocity = transform.TransformDirection(rotate);


    }

    private void HandleInteraction() {
        // Reference: http://answers.unity3d.com/comments/366286/view.html
        if (Input.GetMouseButtonDown(0)) { // if left button pressed...
                                           // create a ray passing through the mouse pointer:

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity)) { // if something hit...
                                                                         // if you must do something with the previously
                                                                         // selected item, do it here,
                                                                         // then select the new one:


                Selected = hit.transform.gameObject;
                Debug.Log("Mouse Select: " + Selected.name);

                // do whatever you want with the newly selected
                // object
                UiManager.Instance.SelectedObject = Selected;
                UiManager.Instance.IsUpdateRequired = true;
            } else {
                UiManager.Instance.SelectedObject = null;
                UiManager.Instance.IsUpdateRequired = true;
            }
        }
    }
}
