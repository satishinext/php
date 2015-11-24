using UnityEngine;
using Base;

namespace Module.Article
{
	public class ArticleModulePannel : ModulePanelController 
	{
		JsonBase jsonBase;
		void Awake()
		{
			BaseAwake();
		}
		
		public void OnClickOnClose()
		{
			if(jsonBase.articlePannel.activeInHierarchy)
			{
				jsonBase.articlePannel.SetActive(false);
				jsonBase.tablePannel.SetActive(true);
				jsonBase.loadingPannel.SetActive(false);
			}
			else
			{
				Close();
			}
			
		}
		
		public override bool Open()
		{
			gameObject.SetActive(true);
			
			return true;
		}
		
		public override bool Close()
		{
			gameObject.SetActive(false);
			
			return true;
		}
		void Start()
		{
			jsonBase = JsonBase.instance;
		}
	}
}
