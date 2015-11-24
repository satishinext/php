using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Runtime.Serialization;
public class showListControl : MonoBehaviour {
	
	public static showListControl control;
	public string description;
	public bool check;
	// Use this for initialization
	void Awake () {
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if(control != this){
			Destroy(gameObject);
		}
	}
	
	void OnGUI()
	{
		save ();
		loadAll();
		string [] des = {
			"Work Done",
			"Work Pending",
			"PC Required",
			"Witcher 3d",
			"Need For Speed",
			"Todo Ending",
			"todo 2 ",
			"todo 3 ",
		};
		//int[] chec = {
		//	0,1,1,0,0,1
		//};
		//GUI.Label (new Rect (10, 10, 900, 80), "descp :" + description);
		//GUI.Label (new Rect (10, 20, 100, 30), "check :" + check);
		for (int i=0; i<8; i++) {
			//	GUI.Label (new Rect (50, 10+i*30, 900, 90), "descp :" + des[i]);
			//	GUI.Label (new Rect (10, 20+i*10, 100, 30), "check :" + chec[i]);
		}
	}
	public void save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/Playerinfo.dat");
		//
		string [] des = {
			"Work Done",
			"Work Pending",
			"PC Required",
			"Witcher 3d",
			"Need For Speed",
			"Todo Ending",
			"todo 2 ",
			"todo s ",
		};
		int[] chec = {
			0,1,1,0,0,1,1,0
		};
		PData[] PD = new PData[8];
		for (int i = 0; i < 8; i++)
			PD[i] = new PData(des[i],chec[i]);
		//
		bf.Serialize (file,PD);
		file.Close();
	}
	public void Update () {
		
		if (Application.platform == RuntimePlatform.Android) {
			
			if (Input.GetKeyUp(KeyCode.Escape)) {
				
				//quit application on return button
				Application.Quit();

				//Application.LoadLevel("main menu");
				
				return;
				
			}
			
		}
		
	}
	public void saveItem(string d,int c)
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.OpenWrite(Application.persistentDataPath + "/Playerinfo.dat");
	
		PData PD = new PData(d,c);

		//
		bf.Serialize (file,PD);
		file.Close();
	}
	public void load()
	{
		if (File.Exists (Application.persistentDataPath + "/Playerinfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file= File.Open(Application.persistentDataPath + "/Playerinfo.dat",FileMode.Open);
			PData []dat=(PData[])bf.Deserialize(file);
			file.Close();
			int spa=0;
			for (int i=0; i<dat.Length; i++) {
				if(dat[i].check==1)
				{
					GUI.Button (new Rect (50, 10+((spa+1)*40), 500, 30), spa+". " + dat[i].description);

					spa++;
				}
			}
			
		}
		
	}
	public void loadAll()
	{
		if (File.Exists (Application.persistentDataPath + "/Playerinfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/Playerinfo.dat", FileMode.Open);
			PData [] dat = (PData[])bf.Deserialize (file);
			file.Close ();
			int spa = 0;
			for (int i=0; i<dat.Length; i++) {
				if(dat[i].check==0)
				{
					GUI.contentColor=Color.white;

				GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
				myButtonStyle.fontSize = 32;
				GUI.Button (new Rect (0, 10 + ((spa + 1) * 80), Screen.width, 80), "" + dat [i].description,myButtonStyle);
				}
				else if(dat[i].check==1)
				{
					GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
					myButtonStyle.fontSize = 32;
					GUI.contentColor=Color.red;
					GUI.Button (new Rect (0, 10 + ((spa + 1) * 80), Screen.width, 80), "" + dat [i].description,myButtonStyle);

				}

				spa++;

			}
			
		}
	}
	
}

