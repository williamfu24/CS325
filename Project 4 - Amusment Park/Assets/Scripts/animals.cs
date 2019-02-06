using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animals : MonoBehaviour {

	public float height=14;
	private string stringB;
	public float heightChange=2f;
	private string stringA;
	Vector3 A = new Vector3(0,3,0);
	Vector3 B = new Vector3(0,14,0);
	private bool rise=true;
	private bool ride=false;
	private string instructions = "Z/C changes height. R/V changes speed";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y>B.y){
			rise=false;
		}
		if(transform.position.y<A.y){
			rise=true;
		}
		if(rise==true){
			transform.Translate(Vector3.up*Time.deltaTime*heightChange);
		}
		if(rise==false){
			transform.Translate(-Vector3.up*Time.deltaTime*heightChange);
		}
		if(ride==true){
			Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y+6,transform.position.z);
			if(Input.GetKeyDown("escape")){
			ride = false;
			print("Left animal view");
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y+5, Camera.main.transform.position.z);
			}
			if(Input.GetKeyDown(KeyCode.R)){
				heightChange+=.5f;
			}
			if(Input.GetKeyDown(KeyCode.V)){
				heightChange-=5.5f;
			}
			if(Input.GetKeyDown(KeyCode.Z)){
				if(height<13.5f){
					height+=.5f;
				}
			}
			if(Input.GetKeyDown(KeyCode.C)){
				if(height>3.5){
					height-=.5f;
				}
			}
		}
		
	}
	void OnMouseDown(){
		ride = true;
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y+6,transform.position.z);
	}
	void OnGUI(){
		if(ride){
			GUI.TextField(new Rect(10, 90, 300, 30), instructions);
			
			stringA = heightChange.ToString();
			GUI.TextField(new Rect(10, 130, 50, 30), stringA);
		}
			stringB = height.ToString();
			GUI.TextField(new Rect(10, 170, 50, 30), stringB);
	}
}
