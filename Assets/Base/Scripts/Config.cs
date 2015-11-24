using UnityEngine;

namespace Base
{
	public sealed class Config 
	{
		private static readonly Config instance = new Config();
		
		private string       gameTitle;
		private PlatformType currPlatform;
		private string       gameVersion;
		private string       serverUrl;
		
		static Config() 
		{
		}
		
		private Config() 
		{
			currPlatform  = PlatformType.iOS;
			gameVersion   = "1.0"; // also change in unity editor
			serverUrl     = "http://personalizedgift.uehelp.com/";
			gameTitle     = "Danjur";
		}
		
		public static Config Instance 
		{ 
			get {return instance;}
		}
		
		public string GameTitle 
		{ 
			get {return gameTitle;}
		}
		
		public PlatformType CurrPlatform
		{
			get {return currPlatform;}
		}
		
		public string GameVersion
		{
			get {return gameVersion;}
		}
		
		public string ServerUrl
		{
			get {return serverUrl;}
		}
	}
}