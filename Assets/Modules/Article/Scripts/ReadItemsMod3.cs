using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using SimpleJSON;

namespace Module.Article
{
	public class ReadItemsMod3 : MonoBehaviour 
	{
		
		#region [Public Variables]
		public GameObject ArticleTemplate;
		public Texture imgDefault;
	
		#endregion
		#region [Private Variables]
		string url = "http://personalizedgift.uehelp.com/wp-json/posts?type=gift";
		WWW www;
		WWWForm form;
		string myText;
		int value;
		ControlManagerMod3 controlManagerMod3;
		#endregion
		#region [Private Method]
	
	/*	void DisplayArticles()
		{
			for(int i=1; i<2; i++)
			{
				GameObject gObj = Instantiate(ArticleTemplate,Vector3.zero,Quaternion.identity) as GameObject;
				gObj.transform.parent = this.transform;
				gObj.transform.localPosition = new Vector3(0,-500 *i, 0);
			}		
		}*/
		private string getDataValueForKey(Dictionary<string, object> dict, string key) {
			object objectForKey;
			if (dict.TryGetValue(key, out objectForKey)) {
				return (string)objectForKey;
			} else {
				return "";
			}
		}
		public void LoadImage () {
			// Call the actual loading method
			StartCoroutine(RealLoadImage(0));
		}
		private IEnumerator RealLoadImage (int dataValue) 
		{
			www = new WWW(url); 
			yield return www;
			string myText = www.text; 
			JSONNode jsonObj = JSON.Parse(myText);
		
			string imgurl = jsonObj[value]["featured_image"]["source"];
			
			// url is fetched right here. This url if I parsed directly then image is shown but not in this way as I have written code.
			
			Debug.Log(imgurl);
			
			//Call the WWW class constructor
			WWW imageURLWWW = new WWW(imgurl);
			
			//Wait for the download
			yield return imageURLWWW;
			
			//Simple check to see if there's in deed a texture available
			if(imageURLWWW.texture != null) {
				
				//Construct a new Sprite
				Sprite sprite = new Sprite();
				
				//CreateanewspriteusingtheTexture2Dfromtheurl.
				
				sprite = Sprite.Create(imageURLWWW.texture, new Rect(0,0,imageURLWWW.texture.width,imageURLWWW.texture.height), Vector2.zero);
				
				//AssignthespritetotheImageComponent
				GetComponent<UnityEngine.UI.Image>().sprite = sprite;
			}
			
			yield return null;
			//controlManagerMod3.totData is data I have counted
			if(value < controlManagerMod3.totData)
			{
				StartCoroutine(RealLoadImage(value++));
			}
			
		}
/*	This is the code for displaying textual data
	******************************
		IEnumerator Read()
		{
			www = new WWW(url); 
			yield return www;
			myText = www.text;
			JSONNode jsonObj = JSON.Parse(myText);
			controlManagerMod3.totData = jsonObj.Count;
			
			Debug.Log("totData" + controlManagerMod3.totData);
			
			for (int j=0; j<jsonObj.Count; j++) 
			{
				string data = jsonObj[j]["title"].ToString();
				controlManagerMod3.title.Add(data);
				
				//Debug.Log("@@@@@@@@" +data);
				if (jsonObj[j]["featured_image"].ToString ().Contains("source") )
				{
//				//	Texture imgData = jsonObj[j]["title"].ToString();
//					string imgUrl ="http://personalizedgift.uehelp.com//wp-content//uploads//2015//07//glass_dani.max_.png";
//			//		string imgUrl = jsonObj[j]["featured_image"]["source"].ToString();
//			//		Debug.Log( jsonObj[j]["featured_image"]["source"].ToString());
//					value = j;
//					StartCoroutine("DownloadIMG",imgUrl);		
					
				}
				else
				{
				//	controlManagerMod3.dataImage = imgDefault;
				}
			}
		
			
		}
	
		IEnumerator DownloadIMG(string urlImg)
		{
			WWW wwwImg = new WWW(urlImg);
			yield return wwwImg;	
//			controlManagerMod3.rowTexture[value] = wwwImg.texture;			
		}
		***********************************
		*/
		#endregion
		#region [Unity Method]
		void Start()
		{
			LoadImage();
		//	StartCoroutine("Read");
			controlManagerMod3 = ControlManagerMod3.instance;
		}
		#endregion
		#region [Public Method]
		#endregion
	}
}
