using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Runtime.Serialization;
using UnityEngine.UI;


public class SceneControl : MonoBehaviour {

	public static SceneControl control;
	public string description;
	public bool check;
	public bool []allOptions;
	PData []dat;
	//public static Quaternion identity;
	//public GameObject btnPrefab= Instantiate(Resources.Load("MyPrefab")) as GameObject;
	//public  RectTransform panel = canvas.GetComponentInChildren<RectTransform>(); 
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
		load ();
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
			"Store documents & links on each task row",
			
			"Set alerts for due dates & sheet changes",
			
			"Log task details, comments, & notes",
			
			"Enable Gantt view to see a timeline",
			
			"View & edit tasks in a calendar",
			
			"Organize tasks into collapsible sections",
			
			"Collaborate in real-time",
			
			"Request updates on task status",
			
			"Collect new rows & take action",
			
			"View & update your to do list on the go",

		};
		int[] chec = {
			0,1,1,0,0,1,1,0,1,1,1,0,0,1,0,1,0,0,
		};
		PData[] PD = new PData[8];
		for (int i = 0; i < 8; i++)
				PD[i] = new PData(des[i],chec[i]);
		//
		bf.Serialize (file,PD);
		file.Close();
	}
	public void  getName(string des)
	{
		for(int i=0;i<dat.Length;i++)
		{
			if(des.Equals(dat[i].description))
			{
				GUI.Toggle(new Rect(100, 10 + (1 * 100), 500, 80), false, ""+dat[i].description);

			}
		}
	}
	public void load()
	{
		if (File.Exists (Application.persistentDataPath + "/Playerinfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file= File.Open(Application.persistentDataPath + "/Playerinfo.dat",FileMode.Open);
			dat=(PData[])bf.Deserialize(file);
			file.Close();
			int spa=0;

			for (int i=0; i<dat.Length; i++) {
				if(dat[i].check==1)
				{
					allOptions = new bool[dat.Length];



					//save the referense of the object to further access  the 
					//search optimal ways to do so




					//allOptions[i] = GUI.Toggle(new Rect(100, 10 + ((spa + 1) * 100), 500, 80), false, ""+dat[i].description);
					GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
					myButtonStyle.fontSize = 32;
					allOptions[i]=true;
											//Debug.Log("fcdf"+spa);

					if(allOptions[i]&&GUI.Button (new Rect (0, 10 + ((spa + 1) * 80), Screen.width, 80), spa+". " + dat[i].description,myButtonStyle))
					{

						dat[i].check=0;
						allOptions[i]=false;
						//getName(dat[i].description);
						Debug.Log("nef-"+spa);
					}



						//btnPrefab = new GameObject();
					//btn.name = dat[i].description;
				/*	GameObject go = (GameObject) Instantiate(btnPrefab);
					go.transform.localScale = new Vector3(1, 1, 1);
					//go.name=dat[i].description;
					Button btn = go.GetComponent<Button>();
					btn.name = "testttt";//dat[i].description;
					btn.onClick.AddListener(() => TestListener(btn));

					GameObject newButton = (GameObject)Instantiate(btnPrefab, Vector3(1,1,1), new Quaternion(0,0,0,0));
					newButton.transform.SetParent(panel, false);
					newButton.transform.localScale = new Vector3(1, 1, 1);
					newButton.name = "myPlayerName";
				*/
					spa++;


					//	
					}

		}

	}

}

	public void TestListener(Button btn)
	{
		Debug.Log ("Clicked: " + btn.name);
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

	public void saveItem(string d,int c)
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/Playerinfo.dat");
		
		PData PD = new PData(d,c);
		
		//
		bf.Serialize (file,PD);
		file.Close();
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
				if (dat [i].check == 1) {
					GUI.Button (new Rect (50, 10 + ((spa + 1) * 40), 500, 30), "descp :" + dat [i].description);
					spa++;
				}
			}
			
		}
	}
		
	}
	[Serializable ()]
class PData: ISerializable{
	public string description;
	public int check;
	public  PData(string d,int c)
	{
		this.description = d;
		this.check = c;
	}
	public void GetObjectData (SerializationInfo info, StreamingContext ctxt)
	{
			
			info.AddValue("description", (description));
			info.AddValue("check", check);
	}
		public PData (SerializationInfo info, StreamingContext ctxt)
		{
			// Get the values from info and assign them to the appropriate properties. Make sure to cast each variable.
			// Do this for each var defined in the Values section above
			description = (string)info.GetValue("description", typeof(string));
			check = (int)info.GetValue("check", typeof(int));
		}
	}
	