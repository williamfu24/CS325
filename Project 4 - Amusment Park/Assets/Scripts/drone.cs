using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drone : MonoBehaviour {

	//Drone stuff
	private bool controlled=false; 
	private bool r_click=false;
	private Vector3 thisPos;
	public float speed = 25;
	public string myString;
	private Vector3 moveDirection = Vector3.zero;
	private string instructions = "WASD + Mouse for Movement. Escape to Leave. Q+E to Increment Speed";


	//main camera stuff
	private Vector2 currentRotation;
	public float sensitivity = 10f;
	public float maxYAngle = 80f;


	// Use this for initialization
	void Start () {

		thisPos=GameObject.Find("Drone").transform.position;
		//Debug.Log(thisPos.x);
		//Debug.Log(thisPos.y);
		//Debug.Log(thisPos.z);
		
	}
	
	// Update is called once per frame
	void Update () {
		//Get Right Click
		if(Input.GetMouseButtonDown(1)){
			r_click=!r_click;
			Debug.Log("right clicked");
		}
		if (controlled == true && r_click==false){
			//Sets Camera position to drone
			Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 1.8f ,transform.position.z);
			//Movement of drone
			CharacterController controller = GetComponent<CharacterController>();
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			controller.Move(moveDirection *Time.deltaTime);
			//Camera Rotations
			currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
			currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
			currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
			currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);
			//Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
			transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
			if(Input.GetKeyDown("escape")){
				controlled = false;
				print("Left drone view");
				Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y+5, Camera.main.transform.position.z);

			}
			if(Input.GetKeyDown(KeyCode.Q)){
				Debug.Log("Q was pressed");
				speed+=5;
			}
			if(Input.GetKeyDown(KeyCode.E)){
				speed-=5;
			}
		}
	}
	void OnMouseDown(){
		Debug.Log("i have been clicked");
		controlled = true;
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 1.8f ,transform.position.z);

		
	}
	void OnGUI(){
		if(controlled){
			GUI.TextField(new Rect(10, 10, 400, 30), instructions);
			
			myString = speed.ToString();
			GUI.TextField(new Rect(10, 50, 50, 30), myString);
		}
	}
}
