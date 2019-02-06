using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class routing : MonoBehaviour {
	public GameObject A;
	//public GameObject B;
	public float speedTime=50;
	public float dist=225;
	public string stringA;
	public string stringB;
	Vector3 pointA;
	Vector3 pointB;
	private bool riding=false;
	private bool fwd=true;
	private string instructions = "Escape to Leave. Q/E:change speed. Z/C:change length";

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		pointA=A.transform.position;
		pointB=A.transform.position;
		pointB.z=A.transform.position.z+dist;

		if(transform.position.z>pointB.z){
			fwd=false;
			new WaitForSecondsRealtime(3000f);
		}
		if(transform.position.z<pointA.z){
			fwd=true;
			new WaitForSecondsRealtime(3);
		}
		if(fwd==true){
			transform.Translate(Vector3.forward*Time.deltaTime*speedTime);
		}
		if(fwd==false){
			transform.Translate(-Vector3.forward*Time.deltaTime*speedTime);
		}
		if(riding==true){
			Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y+6,transform.position.z);
			if(Input.GetKeyDown("escape")){
			riding = false;
			print("Left kanoe view");
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y+5, Camera.main.transform.position.z);
			}
			if(Input.GetKeyDown(KeyCode.Q)){
				speedTime+=5;
			}
			if(Input.GetKeyDown(KeyCode.E)){
				speedTime-=5;
			}
			if(Input.GetKeyDown(KeyCode.Z)){
				if(dist<230){
					dist+=10;
				}
			}
			if(Input.GetKeyDown(KeyCode.C)){
				if(dist>10){
					dist-=10;
				}
			}
		}
	}
	void OnMouseDown(){
		Debug.Log("kanoe click");
		riding = true;
		Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y+6,transform.position.z);
	}
	void OnGUI(){
		if(riding){
			GUI.TextField(new Rect(10, 10, 300, 30), instructions);
			
			stringA = speedTime.ToString();
			GUI.TextField(new Rect(10, 50, 50, 30), stringA);

			stringB = dist.ToString();
			GUI.TextField(new Rect(10, 90, 50, 30), stringB);
		}
	}
}
