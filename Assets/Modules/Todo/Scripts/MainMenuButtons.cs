using UnityEngine;
using System.Collections;

public class MainMenuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void Update () {
		

		
	}
	public void OnGUI()
	{if (Application.platform == RuntimePlatform.Android) {
			
			if (Input.GetKeyUp(KeyCode.Escape)) {
				
				//quit application on return button
				
				Application.Quit();
				
				return;
				
			}
			
		}
	/*	
		GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
		myButtonStyle.fontSize = 32;
		if(GUI.Button(new Rect(100, 250, 500, 60),"Mark TODO",myButtonStyle))
			Application.LoadLevel("mark_todo");
		if(GUI.Button(new Rect(100,320, 500, 60),"Add New",myButtonStyle))
			Application.LoadLevel("add_new");
		if (GUI.Button (new Rect (100, 390, 500, 60), "Show All",myButtonStyle)) {
			Application.LoadLevel("fullToDoList");
		}
	*/

	}
}
