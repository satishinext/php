using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Base;

namespace App
{
	public sealed class Main
	{
		private static readonly Main instance = new Main();
		private SceneType currSceneType;
		private Dictionary<string, System.Object> currSceneData;
		
		static Main() 
		{
		}
		
		private Main() 
		{
			currSceneType = GetSceneTypeFromName(Application.loadedLevelName);
			currSceneData = null;
			
			SceneType firstScene = SceneType.main;
			
			if(currSceneType != firstScene)
			{
				loadScene(firstScene);
			}
		}
		
		public static Main Instance 
		{ 
			get { return instance; }
		}
		
		public void loadScene(SceneType sceneType, Dictionary<string, System.Object> sceneData = null)
		{
			string sceneName = GetSceneNameFromType(sceneType);
			
			if(sceneName.Length > 0)
			{
				currSceneType = sceneType;
				currSceneData = sceneData;
				
				Application.LoadLevel(sceneName);
			}
		}
		
		public void reloadScene()
		{
			loadScene(currSceneType, currSceneData);
		}
		
		private SceneType GetSceneTypeFromName(string sceneName)
		{
			SceneType sceneType;
			
			if     (sceneName == "AppPlay") sceneType = SceneType.play;
			else if(sceneName == "AppMain") sceneType = SceneType.main;
			else                         sceneType = SceneType.unknown;
			
			return sceneType;
		}
		
		private string GetSceneNameFromType(SceneType sceneType)
		{
			string sceneName;
			
			if     (sceneType == SceneType.play) sceneName = "AppPlay";
			else if(sceneType == SceneType.main) sceneName = "AppMain";
			else                                 sceneName = "";
			
			return sceneName;
		}
		
		public SceneType CurrSceneType
		{
			get {return currSceneType;}
		}
		
		public Dictionary<string, System.Object> CurrSceneData
		{
			get {return currSceneData;}
		}
	}
}
