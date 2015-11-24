using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Module.Article
{
	public class ArticleSelected : MonoBehaviour 
	{
		public Text articleHeading, articleText;		
		int dataSelected;
		JsonBase jsonBase;
	//	ControlManagerMod3 controlManagerMod3;
		void Awake()
		{
			jsonBase = JsonBase.instance;
		//	controlManagerMod3 = ControlManagerMod3.instance;
		}
		// Use this for initialization
		void OnEnable() 
		{
			ArticleTitle();	
			TextArticle();
		}
		void ArticleTitle()
		{			
			
			string str =  jsonBase.title[jsonBase.selectedIndex].ToString();
			str.Replace("\"", string.Empty);
			articleHeading.text = str.ToString();
		}
		void TextArticle()
		{
			string str =  jsonBase.text[jsonBase.selectedIndex].ToString();
			str.Replace("\"", string.Empty);
			articleText.text = str.ToString();
		}
		// Update is called once per frame
	
	}
}