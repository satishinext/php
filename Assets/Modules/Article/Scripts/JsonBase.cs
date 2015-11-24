using UnityEngine;
using System.Collections;
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using SimpleJSON;

namespace Module.Article
{
	public class JsonBase : MonoBehaviour 
	{
		public static JsonBase instance;
		[HideInInspector]
		public WWW www;
		public string url = "http://personalizedgift.uehelp.com/wp-json/posts?type=gift";
		[HideInInspector]
		public string myText;
		[HideInInspector]
		public JSONNode jsonObj;
		[HideInInspector]
		public int totalData;
		[HideInInspector]
		public List<string> title = new List<string>();
		[HideInInspector]
		public List<string> imgUrl = new List<string>();
		[HideInInspector]
		public List<Sprite> images = new List<Sprite>();
		[HideInInspector]
		public int selectedIndex;
		public GameObject articlePannel;
		[HideInInspector]
		public	List<string> text = new List<string>();
		public GameObject tablePannel;
		public GameObject loadingPannel;
		
		void OnEnable()
		{
			instance = this;
			StartCoroutine(ReadData());
		}
		IEnumerator ReadData()
		{
			
			www = new WWW(url); 
			yield return www;
			Debug.Log("! text parsed ! ... starting image parsing");
			myText = www.text;
			jsonObj = JSON.Parse(myText);
			totalData = jsonObj.Count;	
			for(int i=0; i<totalData; i++)
			{
				string titleData = jsonObj[i]["title"];
				
				title.Add(titleData);
				string imgData = jsonObj[i]["featured_image"]["source"];
				imgUrl.Add(imgData);
				string dataText = jsonObj[i]["featured_image"]["content"].ToString();				
				text.Add(dataText);
			}
			
			GameObject.Find("ImageParser").GetComponent<ImageParse>().LoadImage();
			
		}
		
	}
	
}
