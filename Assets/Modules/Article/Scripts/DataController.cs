using UnityEngine;
using System.Collections;
using Tacticsoft.Examples;
using UnityEngine.UI;

namespace Module.Article
{
	public class DataController : MonoBehaviour 
	{
		JsonBase jsonBase;
		public GameObject displayImage;
		public ButtonId buttonIndex;
		public void AssignData(int visibleCellIndex)
		{
			//	Debug.Log("!!!!!!!" + jsonBase.title.Count);
			this.GetComponent<Text>().text = jsonBase.title[visibleCellIndex];
			displayImage.GetComponent<Image>().sprite = jsonBase.images[visibleCellIndex];
			//buttonIndex.id = visibleCellIndex;
		}
		void Awake()
		{
			jsonBase = GameObject.Find("ControlManagerArticle").GetComponent<JsonBase>();
		}
		
		
	}
	
}
