using UnityEngine;
using Base;

namespace Modules.Setting
{
	public sealed class ModulePanel : ModulePanelController
	{
		void Awake()
		{
			BaseAwake();
		}

		public void OnClickOnClose()
		{
			Close();
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
