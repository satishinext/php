using UnityEngine;
using System.Collections;

namespace Module.Article
{
	public class ButtonId : MonoBehaviour 
	{
		[HideInInspector]
		int id;
		
		JsonBase jsonBase;
		public void GetIndex(int indexSelected)
		{
			id = indexSelected;
		}
		public void OnButtonClick()
		{
			jsonBase.selectedIndex = id;
			jsonBase.articlePannel.SetActive(true);
			jsonBase.tablePannel.SetActive(false);
	
		}
		// Use this for initialization
		void Start () 
		{
			jsonBase = JsonBase.instance;
		}
		
		// Update is called once per frame
		void Update () 
		{
			
		}
	}
}
