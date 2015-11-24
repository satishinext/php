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
	public class ImageParse : MonoBehaviour 
	{
		JsonBase jsonBase;
		public void LoadImage () 
		{
			// Call the actual loading method
			StartCoroutine(RealLoadImage());
		}
		private IEnumerator RealLoadImage () 
		{
			// url is fetched right here. This url if I parsed directly then image is shown but not in this way as I have written code.
			
			for(int i=0; i<jsonBase.totalData; i++)
			{
				string urlImage = jsonBase.imgUrl[i];
				//Call the WWW class constructor
				WWW imageURLWWW = new WWW(urlImage);
				
				//Wait for the download
				yield return imageURLWWW;
				//Simple check to see if there's in deed a texture available
				if(imageURLWWW.texture != null) 
				{
					Debug.Log("!!!!!!!!!!!!!" + urlImage);
					//Construct a new Sprite
					Sprite sprite = new Sprite();
					
					//CreateanewspriteusingtheTexture2Dfromtheurl.
					
					sprite = Sprite.Create(imageURLWWW.texture, new Rect(0,0,imageURLWWW.texture.width,imageURLWWW.texture.height), Vector2.zero);
					
					//AssignthespritetotheImageComponent
					jsonBase.images.Add(sprite);
					
					//	GetComponent<UnityEngine.UI.Image>().sprite = sprite;
				}
			}
			jsonBase.tablePannel.SetActive(true);
			jsonBase.loadingPannel.SetActive(false);
			yield return null;
		}
		
		void Start()
		{
			jsonBase = JsonBase.instance;
			
		}
		
	}
	
}
