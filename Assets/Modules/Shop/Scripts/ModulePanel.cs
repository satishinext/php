using UnityEngine;
using Base;

namespace Modules.Shop
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
