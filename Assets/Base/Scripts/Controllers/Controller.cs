using UnityEngine;

namespace Base
{
	public abstract class Controller : MonoBehaviour
	{
		protected App.Main    appMain;
		protected Helper      h;
		protected Config      conf;
		protected DataStorage ds;
		
		protected virtual void BaseAwake()
		{
			appMain = App.Main.Instance;
			h       = Helper.Instance;
			conf    = Config.Instance;
			ds      = DataStorage.Instance;
		}
		
		protected virtual void BaseStart()
		{
			
		}
	}
}