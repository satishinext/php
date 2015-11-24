using UnityEngine;

namespace Base
{
	public abstract class ModulePanelController : PanelController
	{
		protected App.MainPanel appMainPanel;
		
		protected override void BaseAwake()
		{
			base.BaseAwake();
			
			appMainPanel = GameObject.Find("MainCanvas/MainPanel").GetComponent<App.MainPanel>();
		}
		
		abstract public bool Open();
		
		abstract public bool Close();
	}
}