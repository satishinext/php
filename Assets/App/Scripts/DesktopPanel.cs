using UnityEngine;
using Base;

namespace App
{
	public sealed class DesktopPanel : ModulePanelController
	{
		void Awake()
		{
			BaseAwake();
		}
		
		public void OnClickOnShortcut(GameObject shortcut)
		{
			MkShortcutActive(shortcut.name);
		}
		
		public void MkShortcutActive(string shortcutName)
		{
			// do some stuff if required, like to highlight active shortcut
			
			appMainPanel.OnDesktopShortcutActive(shortcutName);
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
	}
}
