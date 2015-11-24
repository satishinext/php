using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Item{
	public string description;
	public bool check;
}


public class CreateScrollList : MonoBehaviour {
	public GameObject sampButton;
	public List<Item>itemList;
	public List<SampleButton>buttonList=new List<SampleButton>();
	public Transform contentPanel;
	PData[]dat;
	// Use this for initialization
	void Start () {
		//PopulateList ();
		Debug.Log("here");
		if (!File.Exists (Application.persistentDataPath + "/Playerinfo.dat")) {

			save ();
		}
		LoadList ();
	}
	void OnGUI()
	{
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
			"View & update your to do list on the go",};
		int[] chec = {
			0,1,1,0,0,1,1,0,1,1,1,0,0,1,0,1,0,0
		};
		PData[] PD = new PData[des.Length];
		for (int i = 0; i < des.Length; i++)
			PD[i] = new PData(des[i],chec[i]);
		//
		bf.Serialize (file,PD);
		file.Close();
	}
	public void LoadList()
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


					GameObject newButton=Instantiate(sampButton) as GameObject;
					SampleButton button= newButton.GetComponent<SampleButton>();
					//Debug.Log(dat[i].description);
					button.buttonText.text=dat[i].description;
					newButton.transform.SetParent(contentPanel);
					button.addlistner();
					button.setIntractable(1);
					buttonList.Add(button);






					//allOptions = new bool[dat.Length];
					
					
					
					//save the referense of the object to further access  the 
					//search optimal ways to do so
					
					
					
					
				/*	//allOptions[i] = GUI.Toggle(new Rect(100, 10 + ((spa + 1) * 100), 500, 80), false, ""+dat[i].description);
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
					
				*/	
					
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
	public void Update () {
		
		if (Application.platform == RuntimePlatform.Android) {
			
			if (Input.GetKeyUp(KeyCode.Escape)) {
				
				//quit application on return button
				foreach(var item in buttonList)
				{
					if(item.getCheckValue()==1)
					{
						for(int i=0;i<dat.Length;i++)
						{
							if(dat[i].description.Equals(item.buttonText.text))
							{
								dat[i].check=0;
								break;
							}
						}
					}

				}
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Create(Application.persistentDataPath + "/Playerinfo.dat");
				PData[] PD = new PData[dat.Length];
				for (int i = 0; i < dat.Length; i++)
					PD[i] = new PData(dat[i].description,dat[i].check);
				//
				bf.Serialize (file,PD);
				file.Close();
				Application.LoadLevel("main menu");
				
				return;
				
			}
			
		}
		
	}
	// Update is called once per frame
	void PopulateList () {
		foreach (var item in itemList) {
			GameObject newButton=Instantiate(sampButton) as GameObject;
			SampleButton button= newButton.GetComponent<SampleButton>();
			Debug.Log(item.description);
					button.buttonText.text=item.description;
			newButton.transform.SetParent(contentPanel);
		}
	}
}
