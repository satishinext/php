using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Module.Todo{
public class TodoAllListsModel : MonoBehaviour {

	public List<TodoListModel> lists = new List<TodoListModel>(){new TodoListModel(0,"Default"),new TodoListModel(1,"Shopping")};

	private List<int> listsId = new List<int>(){0, 1};

	public void CreateNewList(string listName){
		if (lists.Any (x => x.listName == listName))
			return;
		TodoListModel newList = new TodoListModel (GetListId(),listName);
		newList.SaveTodoList ();
		lists.Add (newList);
		listsId.Add (newList.listId);
		SaveAllListsIds ();
	}

	public void CreateNewList(TodoListModel newList){
		if (lists.Any (x => x.listName == newList.listName))
			return;
		newList.SaveTodoList ();
		lists.Add (newList);
		listsId.Add (newList.listId);
		SaveAllListsIds ();
	}

	public void SetCurrentList(string listName){
		PlayerPrefs.SetString ("CurrentList", listName);
	}

	public TodoListModel GetCurrentList(){
		string currentListName = "";
		TodoListModel currentList = null;
		if (PlayerPrefs.HasKey ("CurrentList"))
			currentListName = PlayerPrefs.GetString ("CurrentList");
		if(!string.IsNullOrEmpty(currentListName))
			currentList = lists.FirstOrDefault(x => x.listName == currentListName);
		if (currentList != null)
			return currentList;
		return lists [0];
	}

	public void EditCurrentList(TodoListModel list){
		list.SaveTodoList ();
	}

	public void SaveAllLists(){
		SaveAllListsIds ();
		foreach (TodoListModel list in lists)
			list.SaveTodoList ();
	}

	public void SaveAllListsIds(){
		string saveData = JsonFx.Json.JsonWriter.Serialize(listsId);
		Debug.Log (saveData);
		PlayerPrefs.SetString ("PlayersLists", saveData);
	
	}

	public void LoadAllLists(){
		Debug.Log ("LoadAllLists");
		listsId.Clear();
		if (PlayerPrefs.HasKey ("PlayersLists")) {
			Debug.Log(PlayerPrefs.GetString ("PlayersLists"));
			var ls = JsonFx.Json.JsonReader.Deserialize<int[]>(PlayerPrefs.GetString ("PlayersLists"));

			listsId.AddRange(ls);
		}
		if (listsId != null) {
			lists.Clear();
			foreach(int listId in listsId){
				TodoListModel list = TodoListModel.LoadTodoList(listId.ToString());
				if(list != null)
					lists.Add(list);
			}
		}
		Debug.Log (listsId.Count);
		if (listsId == null || listsId.Count == 0) {
			listsId = new List<int>(){0, 1};
			lists = new List<TodoListModel>(){new TodoListModel(0, "Default"),new TodoListModel(1, "Shopping")};
			SaveAllLists();
		}
		Debug.Log ("lists " + lists.Count);
	}

	public void DestroyList(TodoListModel list){
		listsId.Remove (list.listId);
		SaveAllListsIds ();
		lists.Remove (list);
		PlayerPrefs.DeleteKey (list.listId.ToString());
	}

	public int GetListId(){
		if (lists.Count == 0)
			return 0;
		int id = lists.Max(x => x.listId);
		return id + 1;
	}

}
}
