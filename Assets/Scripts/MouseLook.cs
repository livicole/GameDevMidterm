﻿using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {

	public float rotateSpeed = 150f;
	float rotationY;
	public Transform body;
//	public Texture2D cursorCrosshair;
//	public CursorMode cursorMode = CursorMode.Auto;
//	public Vector2 hotSpot = new Vector2(-20f,-20f);
//
	void Start(){
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = true;
//		Cursor.SetCursor (cursorCrosshair, hotSpot, cursorMode);
	}

	// Update is called once per frame
	void Update () {
		
		//if we aren't using the mouse, these values are zero
		float mouseX = Input.GetAxis ("Mouse X"); //move left = -1
		float mouseY = Input.GetAxis ("Mouse Y"); //move down = -1

		//causes inverted mouseY, camera rolling, and no clamping of Y axis
		//transform.Rotate(mouseY * Time.deltaTime * rotateSpeed, mouseX * Time.deltaTime * rotateSpeed, 0f);

		rotationY += -mouseY * Time.deltaTime * rotateSpeed;
		rotationY = Mathf.Clamp (rotationY, -80f, 80f); //so we don't keep looking upside down, constrains movement

		body.Rotate (0f, mouseX * Time.deltaTime * rotateSpeed, 0f); //horizontal mouse move
		transform.localEulerAngles = new Vector3 (rotationY, transform.localEulerAngles.y, 0f);  //vertical mouse move, setting Z to 0 unrolls camera


		if (Input.GetKeyDown (KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
		}
	}
}
