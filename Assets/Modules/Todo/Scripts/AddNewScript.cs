using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Runtime.Serialization;
public class AddNewScript : MonoBehaviour {
	private string textFieldString = "";
	List<PData> ItemList = new List<PData>();
	public static AddNewScript control;
	// Use this for initialization
	public void OnGUI()
	{
		loadAll ();
		/*
		GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
		myButtonStyle.fontSize = 32;
		GUIStyle textStyle = new GUIStyle (GUI.skin.textField);
		textStyle.fontSize = 32;
		textFieldString = GUI.TextField (new Rect (100, 110, 500, 80), textFieldString,textStyle);
		if (GUI.Button (new Rect (100, 220, 500, 80), "Add Task",myButtonStyle)) {

			ItemList.Add(new PData(textFieldString,1));
			Application.LoadLevel("main menu");
			// Adding new TODO task to be coded here//
		}
		*/
	}
	public void Update () {
		
		if (Application.platform == RuntimePlatform.Android) {
			
			if (Input.GetKeyUp(KeyCode.Escape)) {
				
				//quit application on return button
				
				Application.LoadLevel("main menu");
				
				return;
				
			}
			
		}
		
	}
	public void AddTask()
	{
		ItemList.Add(new PData(textFieldString,1));
		Application.LoadLevel("main menu");
		//GUI.Label(new Rect(10,10,500,30),"Added Task");
	}

	public void loadAll()
	{
		if (File.Exists (Application.persistentDataPath + "/Playerinfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/Playerinfo.dat", FileMode.Open);
			PData [] dat = (PData[])bf.Deserialize (file);
			file.Close ();


			for(int i=0;i<dat.Length;i++)
			{
				ItemList.Add(dat[i]);

			}
			//PData f=new PData(des,1);
			//ItemList.Add(f);
			//GUI.Label(new Rect(10,10,300,30),ItemList.Contains(f)+"");

			
		}
	}
}
