using UnityEngine;
using Base;

namespace App
{
	public sealed class MainPanel : PanelController
	{
		private DesktopPanel                    desktopPanel;
		private Modules.Setting.ModulePanel     settingModulePanel;
		private Modules.User.ModulePanel        userModulePanel;
		private Modules.Friend.ModulePanel      friendModulePanel;
		private Modules.Timeline.ModulePanel    timelineModulePanel;
		private Modules.Shop.ModulePanel        shopModulePanel;
		private Modules.Chat.ModulePanel        chatModulePanel;
		private Modules.PremiumCall.ModulePanel premCallModulePanel;
		private Modules.Games.ModulePanel       gamesModulePanel;
		private Modules.Todo.ModulePanel        todoModulePanel;
		private Module.Article.ArticleModulePannel articleModulePannel;
		
		void Awake()
		{
			BaseAwake();
			
			desktopPanel        = transform.Find("Desktop").GetComponent<DesktopPanel>();
			settingModulePanel  = transform.Find("Modules/Setting").GetComponent<Modules.Setting.ModulePanel>();
			userModulePanel     = transform.Find("Modules/User").GetComponent<Modules.User.ModulePanel>();
			friendModulePanel   = transform.Find("Modules/Friend").GetComponent<Modules.Friend.ModulePanel>();
			timelineModulePanel = transform.Find("Modules/Timeline").GetComponent<Modules.Timeline.ModulePanel>();
			shopModulePanel     = transform.Find("Modules/Shop").GetComponent<Modules.Shop.ModulePanel>();
			chatModulePanel     = transform.Find("Modules/Chat").GetComponent<Modules.Chat.ModulePanel>();
			premCallModulePanel = transform.Find("Modules/PremiumCall").GetComponent<Modules.PremiumCall.ModulePanel>();
			gamesModulePanel    = transform.Find("Modules/Games").GetComponent<Modules.Games.ModulePanel>();
			todoModulePanel     = transform.Find("Modules/Todo").GetComponent<Modules.Todo.ModulePanel>();
			articleModulePannel = transform.Find("Modules/Article").GetComponent<Module.Article.ArticleModulePannel>();
			
		}
		
		void Start()
		{
			CloseAllModules();
			desktopPanel.Open();
		}
		
		private void CloseAllModules()
		{
			settingModulePanel.Close();
			userModulePanel.Close();
			friendModulePanel.Close();
			timelineModulePanel.Close();
			shopModulePanel.Close();
			chatModulePanel.Close();
			premCallModulePanel.Close();
			gamesModulePanel.Close();
			todoModulePanel.Close();
			articleModulePannel.Close();
		}
		
		public void OnDesktopShortcutActive(string shortcutName)
		{
			CloseAllModules();
			
			if     (shortcutName == "Settings")    settingModulePanel.Open();
			else if(shortcutName == "Profile")     userModulePanel.Open();
			else if(shortcutName == "AddFriends")  friendModulePanel.Open();
			else if(shortcutName == "Friends")     friendModulePanel.Open();
			else if(shortcutName == "Timeline")    timelineModulePanel.Open();
			else if(shortcutName == "Stickers")    shopModulePanel.Open();
			else if(shortcutName == "ThemeShop")   shopModulePanel.Open();
			else if(shortcutName == "Chats")       chatModulePanel.Open();
			else if(shortcutName == "PremiumCall") premCallModulePanel.Open();
			else if(shortcutName == "Games")       gamesModulePanel.Open();
			else if(shortcutName == "MyLife")      todoModulePanel.Open();
			else if(shortcutName == "Articles")    articleModulePannel.Open();
			
		}
	}
}
