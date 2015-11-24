using UnityEngine;

namespace Base
{
	public abstract class SceneController : Controller
	{
		protected override void BaseAwake()
		{
			base.BaseAwake();
			
			h.InitSettings();
			
			//Screen.sleepTimeout = SleepTimeout.NeverSleep;
		}
	}
}