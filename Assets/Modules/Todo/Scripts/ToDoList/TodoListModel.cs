using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JsonFx;

namespace Module.Todo{
public class TodoListModel {

	public int listId;
	public string listName;
	public bool shareWithAllFrineds;
	public string [] friendsIdToShareWith;
	public string ownerId;
	public List <TodoItemModel> items = new List<TodoItemModel>();

	//public string NewListName{ get { return newListName; } set{newListName = value;}}
	//[System.NonSerialized]
	//private string newListName;

	public TodoListModel(){
	
	}

	public TodoListModel(int listId, string listName){
		this.listName = listName;
		this.listId = listId;
	}

	public int GetLastId(){
		if (items.Count == 0)
			return 0;
		return items.Max (x => x.id) + 1;
	}

	public bool ContainsItem(string text, int id){
		return true;
	}

	public void AddItem(string text){
		AddItem(new TodoItemModel(GetLastId(), text, false));
	}

	public void AddItem(TodoItemModel item){
		Debug.Log (item.id);
		if (!items.Contains (item))
			items.Add (item);
		SaveItemTodoList (item);
	}

	public void RemoveItem(TodoItemModel item){
		items.Remove (item);
		SaveTodoList ();
	}

	public void SaveTodoList(){
		string saveData = JsonFx.Json.JsonWriter.Serialize(this);
		Debug.Log (saveData);
		PlayerPrefs.SetString (this.listId.ToString(), saveData);
	}

	public static TodoListModel LoadTodoList(string listId){
		TodoListModel list = null;
		if (PlayerPrefs.HasKey (listId)) {
			Debug.Log(PlayerPrefs.GetString(listId));
			list = JsonFx.Json.JsonReader.Deserialize<TodoListModel> (PlayerPrefs.GetString(listId));
			//Debug.Log(PlayerPrefs.GetString (this.listName));
			//var list = JsonFx.Json.JsonReader.Deserialize<TodoListModel> (PlayerPrefs.GetString (this.listName));
			//this.items.Clear();
			//this.items.AddRange(items);
			//this = list;
		}
		return list;
	}

	public void SaveItemTodoList(TodoItemModel item){
		//:TODO If save via server update only one item
		SaveTodoList ();
	}

}
}
