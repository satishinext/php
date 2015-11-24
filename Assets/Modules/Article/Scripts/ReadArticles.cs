using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using SimpleJSON;

namespace Module.Article
{
	public class ReadArticles : MonoBehaviour 
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
				
				string dataText = jsonObj[j]["featured_image"]["content"].ToString();				
				controlManagerMod3.text.Add(dataText);
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
		
		#endregion
		#region [Unity Method]
		void OnEnable()
		{
			StartCoroutine("Read");
			controlManagerMod3 = ControlManagerMod3.instance;
		}
		#endregion
		#region [Public Method]
		#endregion
	}
}
