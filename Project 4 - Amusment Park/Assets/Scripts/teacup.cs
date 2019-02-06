using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teacup : MonoBehaviour {

	private string instructions = "Z/C changes height. (-) reverses direction";
	private bool ride = false;
	public float turnPace=4;
	private string stringA;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(0,-Time.deltaTime*turnPace,0));
		if(ride==true){
			Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y+6,transform.position.z);
			if(Input.GetKeyDown("escape")){
			ride = false;
			print("Left teacup");
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y+5, Camera.main.transform.position.z);
			}
			if(Input.GetKeyDown(KeyCode.Z)){
				turnPace+=3;
			}
			if(Input.GetKeyDown(KeyCode.C)){
				turnPace-=3;
			}
		}
	}

	void OnMouseDown(){
		ride = true;
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y+6,transform.position.z);
	}

	void OnGUI(){
		if(ride){
			GUI.TextField(new Rect(10, 10, 300, 30), instructions);
			
			stringA = turnPace.ToString();
			GUI.TextField(new Rect(10, 50, 50, 30), stringA);
		}
	}
}
