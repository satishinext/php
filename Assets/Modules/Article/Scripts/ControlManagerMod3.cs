using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text; 

namespace Module.Article
{
	public class ControlManagerMod3 : MonoBehaviour 
	{
		#region [Public Variables]
		public GameObject articlePannel;
		[HideInInspector]
		public int totData, dataSelected;
		[HideInInspector]
		public	List<string> title = new List<string>();
		[HideInInspector]
		public	List<string> text = new List<string>();
		[HideInInspector]
		public	List<string> rowValue = new List<string>();
		public static ControlManagerMod3 instance;
		[HideInInspector]
		public 	List<Texture> rowTexture = new List<Texture>();
		#endregion
		#region [Private Variables]
		#endregion
		#region [Private Method]
		public string RemoveUnwantedFromString(string str)
		{
			str.Replace("\"", string.Empty);
			return str;

		}
		#endregion
		#region [Unity Method]
		void Awake()
		{
			instance = this;
		
		}
		#endregion
		#region [Public Method]
		#endregion
	}
}

