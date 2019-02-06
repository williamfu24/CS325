using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

using System.Text.RegularExpressions;


public class raytracer3d : MonoBehaviour {
	public int width;
	public int height;
	public Color backgroundc;
	public string path;
	private string stringToEdit = "File Name";

	public Texture2D screen;

	private Ray ray;
	ArrayList sphereList = new ArrayList ();

	public class Sphere
	{
		public float radius;
		public Vector3 coor;
		public Color colorambient;
		public Color colorreflected;

		public bool findhit(Ray ray, double t0, double t1, ref Color color)
		{
			double A = Vector3.Dot (ray.direction, ray.direction);
			double B = 2 * (Vector3.Dot (ray.direction, (ray.origin - coor)));
			double C = Vector3.Dot ((ray.origin - coor), (ray.origin - coor)) - (radius * radius);
			double discrim = (B * B) - (4 * A * C);
			if (discrim > 0) {
				double sqrtdis = Math.Sqrt (discrim);
				double t = (-B - sqrtdis) / (2 * A);
				if (t < t0) {
					t = (-B + sqrtdis) / (2 * A);
				}
				if (t < t0 || t > t1) {
					return false;
				}
				color = colorambient;
				return true;
			} else {
				return false;
			}
		}
	}

	// Use this for initialization
	void Start () {
		//func1();
	}

	// Update is called once per frame
	void Update () {

	}

	void func1()
	{
		string allText = System.IO.File.ReadAllText ("C:\\Users\\william\\Desktop\\College\\Year_3\\Semester_2\\325_Graphics\\Program2\\"+path+".cli");
		string replaceText = Regex.Replace(allText, "[^a-zA-Z-0-9%._]", " ");
		string[] entries= replaceText.Split(new string[]{" "}, System.StringSplitOptions.RemoveEmptyEntries);

		width = int.Parse(entries[1]);
		height = int.Parse(entries[2]);
		float bgr = float.Parse(entries[4]);
		float bgg = float.Parse(entries[5]);
		float bgb = float.Parse(entries[6]);

		screen = new Texture2D(width, height); //screen size
		Color backgroundc = new Color(bgr,bgg,bgb);
		//Color c = new Color(bgr,bgg,bgb); //set screen color

		//	for (int i = 0; i < width; i++) {
		//	for (int j = 0; j < height; j++) {
		//		screen.SetPixel (i, j, c);
		//	}
		//}

		for (int i=7; i<entries.Length;i=i+11) //parse in a bunch of things
		{	
			Sphere x = new Sphere ();
			x.radius =float.Parse(entries[i+1]); //set radius
			x.coor.x=float.Parse(entries[i+2]);
			x.coor.y= float.Parse(entries[i+3]);
			x.coor.z = float.Parse(entries[i+4]); //Set coor x,y,z

			x.colorambient.r=float.Parse(entries[i+5]); //set colorR
			x.colorambient.g=float.Parse(entries[i+6]); //set colorG
			x.colorambient.b=float.Parse(entries[i+7]); //set colorB

			x.colorreflected.r= float.Parse(entries[i+8]); //set a(?)
			x.colorreflected.g= float.Parse(entries[i+9]); //set b(?)
			x.colorreflected.b=float.Parse(entries[i+10]); //set c(?)

			sphereList.Add (x);
			Debug.Log("add sphere done");
		}
		Debug.Log ("parseing done");
		raytraceImage ();
		//screen.Apply ();
		Debug.Log ("done");
	}

	void OnGUI ()
	{
		if (screen!=null)
			GUI.DrawTexture (new Rect (0, 0, width, height), screen);

		stringToEdit = GUI.TextField (new Rect (130, 10, 100, 30), stringToEdit, 25);
		if (GUI.Button (new Rect (240, 10, 100, 30), "Enter")) {
			SubmitName ();
		}
		if (GUI.Button (new Rect (240, 50, 100, 30), "Save")) {
			SaveImage ();
		}
	}

	public void SubmitName(){
		path = stringToEdit;
		func1 ();
	}

	public void SaveImage(){ //goes through all pixels and saves them in array to be then written to a ppm file
		Debug.Log ("saved");
		var fileName = stringToEdit;
		if (File.Exists ("C:\\Users\\william\\Desktop\\College\\Year_3\\Semester_2\\325_Graphics\\Program2\\" + fileName + ".ppm")) {
			Debug.Log (fileName + " already exists");
			return;
		}

		float[] fileText = new float[(height*width*3)];
		int i = 0;
		for (int x = 0; x < screen.width; x++) {
			for (int y = 0; y < screen.height; y++) {
				Color color1;
				color1 = screen.GetPixel (x, y);
				float colorVal;
				colorVal = color1.r;
				colorVal = colorVal * 255;
				fileText[i] = colorVal;
				i++;
				colorVal = color1.g;
				colorVal = colorVal * 255;
				fileText[i] = colorVal;
				i++;
				colorVal = color1.b;
				colorVal = colorVal * 255;
				fileText[i]= colorVal;
				i++;
			}
		}
		System.IO.File.WriteAllText("C:\\Users\\william\\Desktop\\College\\Year_3\\Semester_2\\325_Graphics\\Program1\\"+fileName+".ppm", "P3\t" + screen.width+" " + screen.height+ " " + "255 ");
		using (System.IO.StreamWriter file = new System.IO.StreamWriter ("C:\\Users\\william\\Desktop\\College\\Year_3\\Semester_2\\325_Graphics\\Program1\\" + fileName + ".ppm", true)) {
			for (int j = 0; j < fileText.Length; j++) {
				file.WriteLine (fileText [j]);
			}
		}
	}

	public void raytraceImage()
	{
		Debug.Log ("ray trace image");
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < height; j++) {
				Vector3 origin = new Vector3(i,j,0);
				ray.origin = origin;

				Vector3 direction = new Vector3 (0, 0, -1);
				ray.direction = direction;

				Color newColor = new Color();
				newColor = trace(ray);
				//Debug.Log ("trace ray done");
				screen.SetPixel (i, j, newColor);
				if (i == 150) {
					Debug.Log (j);
					Debug.Log (newColor.r);
				}
			}
		}
		Debug.Log ("done");
		screen.Apply ();
		GUI.DrawTexture (new Rect (0, 0, width, height), screen);
	}

	public Color trace(Ray ray)
	{
		double t0 = .0001;
		double t1 = 1000000;
		bool hit = false;
		Color hitcolor = new Color (0,0,0);
		for (int i = 0; i < sphereList.Count; i++) {
			Sphere temp = new Sphere ();
			temp = (Sphere)sphereList [i];
			if (temp.findhit (ray, t0, t1, ref hitcolor) == true) {
				hit = true;
			}
		}
		if (hit) {
			return hitcolor;
		}else {
			return backgroundc;
		}
	}
}
