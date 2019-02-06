using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;

using System.Text.RegularExpressions;

public class PPM_Reader : MonoBehaviour {
	private Texture2D myPic;
	public int width;
	public int height;

	//public string[] entries;

	private string stringToEdit = "Enter Pic Name";
	public string path;

	//private Texture2D flipPic;


	void func1()
	{
		string allText = System.IO.File.ReadAllText ("C:\\Users\\william\\Desktop\\College\\Year_3\\Semester_2\\325_Graphics\\Program1\\"+path+".ppm");
		string replaceText = Regex.Replace(allText, "[^a-zA-Z0-9%._]", " ");
		string[] entries= replaceText.Split(new string[]{" "}, System.StringSplitOptions.RemoveEmptyEntries);

		width = int.Parse(entries[1]);
		height = int.Parse(entries[2]);

		myPic = new Texture2D((int)(width), (int)(height));

		int maxColor = int.Parse(entries[3]);
		int myx=0;
		int myy=height-1;

		for(int i= 4; i<entries.Length; i=i+3)
		{
			Color c = new Color (float.Parse(entries[i])/maxColor, float.Parse(entries[i+1])/maxColor, float.Parse(entries[i+2])/maxColor);
			myPic.SetPixel(myx, myy, c);
			myx++;
			if(myx==width)
			{
				myx=0;
				myy--;
			}
		}
		myPic.Apply ();
	}

	void Start()
	{
		//func1 ();
	}

	void OnGUI ()
	{
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), myPic);

		stringToEdit = GUI.TextField (new Rect (130, 10, 100, 30), stringToEdit, 25);
		if (GUI.Button (new Rect (240, 10, 100, 30), "Load")) {
			SubmitName ();
		}
		if (GUI.Button (new Rect (240, 50, 100, 30), "Save")) {
			savePic ();
		}
		//if (!btntexture) {
		//	Debug.LogError ("{;ease assign a texture on the inspector");
		//	return;
		//}
		//if(GUI.Button(new Rect(10,10,50,50), btntexture))
		//	Debug.Log("Clicked the button with an image");
		//if(GUI.Button(new Rect(10,70,50,30), "Click"))
		//	Debug.Log("Clicked the button with text");
		if (GUI.Button (new Rect (10, 10, 100, 30), "Negate Red")) {
			negate_red ();
		}
		if (GUI.Button (new Rect (10, 50, 100, 30), "Negate Green")) {
			negate_green ();
		}
		if (GUI.Button (new Rect (10, 90, 100, 30), "Negate Blue")) {
			negate_blue ();
		}
		if (GUI.Button (new Rect (10, 130, 100, 30), "Gray Scale")) {
			grey_scale ();
		}
		if (GUI.Button (new Rect (10, 170, 100, 30), "Flip Horizontal")) {
			flip_horizontal ();
		}
		if (GUI.Button (new Rect (10, 210, 100, 30), "Flatten Red")) {
			flatten_red ();
		}
		if (GUI.Button (new Rect (10, 250, 100, 30), "Flatten Green")) {
			flatten_green ();
		}
		if (GUI.Button (new Rect (10, 290, 100, 30), "Flatten Blue")) {
			flatten_blue ();
		}
		if (GUI.Button (new Rect (10, 330, 100, 30), "Reset Picture")) {
			reset_pic ();
		}
	}

	public void SubmitName(){
		Debug.Log ("submitted");
		path = stringToEdit;
		func1 ();
	}

	public void savePic(){
		Debug.Log ("saved");
		var fileName = stringToEdit;
		if (File.Exists ("C:\\Users\\william\\Desktop\\College\\Year_3\\Semester_2\\325_Graphics\\Program1\\" + fileName + ".ppm")) {
			Debug.Log (fileName + " already exists");
			return;
		}

		float[] fileText = new float[(height*width*3)];
		int i = 0;
		for (int x = 0; x < myPic.width; x++) {
			for (int y = 0; y < myPic.height; y++) {
				Color color1;
				color1 = myPic.GetPixel (x, y);
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
		//File.WriteAllText ("C:\\Users\\william\\Desktop\\College\\Year_3\\Semester_2\\325_Graphics\\Program1\\"+fileName+".ppm", "P3\t" + myPic.width+" " + myPic.height+ " " + "255 " + fileText);
		System.IO.File.WriteAllText("C:\\Users\\william\\Desktop\\College\\Year_3\\Semester_2\\325_Graphics\\Program1\\"+fileName+".ppm", "P3\t" + myPic.width+" " + myPic.height+ " " + "255 ");
		using (System.IO.StreamWriter file = new System.IO.StreamWriter ("C:\\Users\\william\\Desktop\\College\\Year_3\\Semester_2\\325_Graphics\\Program1\\" + fileName + ".ppm", true)) {
			for (int j = 0; j < fileText.Length; j++) {
				file.WriteLine (fileText [j]);
			}
		}
	}

	void MakeItRed()
	{
		for (int x = 0; x < myPic.width; x++) 
		{
			for (int y = 0; y < myPic.height; y++) 
			{
				Color color = Color.red;
				myPic.SetPixel (x, y, color);
			}
		}

		myPic.Apply ();
	}

	public void negate_red()
	{
		Debug.Log ("Negate red function");
		for (int x = 0; x < myPic.width; x++) 
		{
			for (int y = 0; y < myPic.height; y++) 
			{
				Color color1;
				color1 = myPic.GetPixel (x, y);
				float newColorVal;
				newColorVal = color1.r;
				newColorVal = 1-newColorVal;
				color1.r = newColorVal;
				myPic.SetPixel (x, y, color1);
			}
		}
		myPic.Apply ();
		Debug.Log ("Negate red done");
	}

	public void negate_green()
	{
		Debug.Log ("negate green function");
		for (int x = 0; x < myPic.width; x++) 
		{
			for (int y = 0; y < myPic.height; y++) 
			{
				Color color1;
				color1 = myPic.GetPixel (x, y);
				float newColorVal;
				newColorVal = color1.g;
				newColorVal = 1 - newColorVal;
				color1.g = newColorVal;
				myPic.SetPixel (x, y, color1);
			}
		}
		myPic.Apply ();
		Debug.Log ("Negate Green done");
	}

	public void negate_blue()
	{
		Debug.Log ("negate blue function");
		for (int x = 0; x < myPic.width; x++) 
		{
			for (int y = 0; y < myPic.height; y++) 
			{
				Color color1;
				color1 = myPic.GetPixel (x, y);
				float newColorVal;
				newColorVal = color1.b;
				newColorVal = 1 - newColorVal;
				color1.b = newColorVal;
				myPic.SetPixel (x, y, color1);
			}
		}
		myPic.Apply ();
		Debug.Log ("Negate blue done");
	}

	public void flip_horizontal()
	{
		Debug.Log ("flip function");
		string allText = System.IO.File.ReadAllText ("C:\\Users\\william\\Desktop\\College\\Year_3\\Semester_2\\325_Graphics\\Program1\\"+path+".ppm");
		string replaceText = Regex.Replace (allText, "[^a-zA-Z0-9%._]", " ");
		string[] entries = replaceText.Split (new string[]{ " " }, System.StringSplitOptions.RemoveEmptyEntries);

		width = int.Parse (entries [1]);
		height = int.Parse (entries [2]);
	
		myPic = new Texture2D ((int)(width), (int)(height));

		int maxColor = int.Parse (entries [3]);
		int myx = width;
		int myy = height - 1;

		for (int i = 4; i < entries.Length; i = i + 3) {
			Color c = new Color (float.Parse (entries [i]) / maxColor, float.Parse (entries [i + 1]) / maxColor, float.Parse (entries [i + 2]) / maxColor);
			myPic.SetPixel (myx, myy, c);
			myx--;
			if (myx == 0) {
				myx = width;
				myy--;
			}
		}
		myPic.Apply ();
		Debug.Log ("flipped");
	}

	public void grey_scale()
	{
		Debug.Log ("grey scale function");
		for (int x = 0; x < myPic.width; x++) 
		{
			for (int y = 0; y < myPic.height; y++) 
			{
				Color color1;
				color1 = myPic.GetPixel (x, y);
				float greyColor = 0;
				greyColor += color1.r;
				greyColor += color1.g;
				greyColor += color1.b;
				greyColor = greyColor / 3;
				color1.r = greyColor;
				color1.g = greyColor;
				color1.b = greyColor;
				myPic.SetPixel (x, y, color1);
			}
		}
		myPic.Apply ();
	}

	public void flatten_red()
	{
		Debug.Log ("flatten r");
		for (int x = 0; x < myPic.width; x++) 
		{
			for (int y = 0; y < myPic.height; y++) 
			{
				Color color1;
				color1 = myPic.GetPixel (x, y);
				color1.r = 0;
				myPic.SetPixel (x, y, color1);
			}
		}
		myPic.Apply ();
		Debug.Log ("flatten r done");
	}

	public void flatten_green()
	{
		Debug.Log ("flatten g");
		for (int x = 0; x < myPic.width; x++) 
		{
			for (int y = 0; y < myPic.height; y++) 
			{
				Color color1;
				color1 = myPic.GetPixel (x, y);
				color1.g = 0;
				myPic.SetPixel (x, y, color1);
			}
		}
		myPic.Apply ();
		Debug.Log ("flatten g done");
	}

	public void flatten_blue()
	{
		Debug.Log ("flatten b");
		for (int x = 0; x < myPic.width; x++) 
		{
			for (int y = 0; y < myPic.height; y++) 
			{
				Color color1;
				color1 = myPic.GetPixel (x, y);
				color1.b = 0;
				myPic.SetPixel (x, y, color1);
			}
		}
		myPic.Apply ();
		Debug.Log ("flatten b done");
	}

	public void reset_pic()
	{
		Debug.Log ("reset pic");
		func1 ();
	}
}