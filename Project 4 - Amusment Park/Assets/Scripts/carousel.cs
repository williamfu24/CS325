using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carousel : MonoBehaviour {

	public float pace = 10f;
	private string stringA;
	private bool ride=false;
	private string instructions = "Q/E sets speed. (-) results in reverse";
	//public Vector3 pos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(0,Time.deltaTime*pace,0));
		if(ride==true){
			Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y+6,transform.position.z);
			if(Input.GetKeyDown("escape")){
			ride = false;
			print("Left carousel");
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y+5, Camera.main.transform.position.z);
			}
			if(Input.GetKeyDown(KeyCode.Q)){
				pace+=3;
			}
			if(Input.GetKeyDown(KeyCode.E)){
				pace-=3;
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
			
			stringA = pace.ToString();
			GUI.TextField(new Rect(10, 50, 50, 30), stringA);
		}
	}
}
