using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

namespace Module.Todo{
public class TodoListController : MonoBehaviour {
	
	public TodoAllListsModel allListsModel;
	public TodoView todoView;
	public TodoEditListView todoEditListView;
	private TodoListModel currentTodoListModel;
		
	public void Start(){
		allListsModel.LoadAllLists ();
		currentTodoListModel = allListsModel.GetCurrentList ();
		todoView.SetCurrentListTitle (currentTodoListModel.listName);
		FillItems ();
	}
	
	#region Items

	public void FillItems(){
		todoView.FillItems (currentTodoListModel).ForEach(x => {x.onUpdate = UpdateItem; x.onDestroy = RemoveItem;});
	}

	public void AddItem(string text){
		if (string.IsNullOrEmpty (todoView.newItemInput.text))
			return;
		currentTodoListModel.AddItem(todoView.newItemInput.text);
		CreateOrUpdateItemView (currentTodoListModel.items[currentTodoListModel.items.Count-1]);
		todoView.newItemInput.text = "";
	}

	void CreateOrUpdateItemView(TodoItemModel itemModel){
		var itemView = todoView.CreateOrUpdateItemView (itemModel);
		itemView.onDestroy = RemoveItem;
		itemView.onUpdate = UpdateItem;
	}

	public void RemoveItem(TodoItemView itemView){
		currentTodoListModel.RemoveItem (itemView.item);
		todoView.RemoveItem (itemView);
	}

	public void UpdateItem(TodoItemView itemView){
		currentTodoListModel.SaveTodoList ();
	}
	#endregion

	#region Lists
	public void ShowAllListsView(){
		if (allListsModel.lists.Count != todoView.listsView.Count) {
			foreach (var list in allListsModel.lists) {
				todoView.CreateListView (list).onSelect = SetCurrentList;
			}
		}
		todoView.ShowAllListView ();
	}

	public void HideAllListsView(){
		todoView.HideAllListView ();
	}

	public void SetCurrentList(TodoListView listView){
		//Debug.Log ();
		allListsModel.SetCurrentList (listView.listModel.listName);
		currentTodoListModel = allListsModel.GetCurrentList ();
		todoView.SetCurrentListTitle (listView.listModel.listName);
		todoView.ClearItems ();
		FillItems ();
		todoView.HideAllListView ();
	}

	public void CreateNewList(){
		todoEditListView.onFinishEdit = AddNewList;
		todoEditListView.ShowEditListView (null);
	}

	public void AddNewList(){
		todoEditListView.editList.listId = allListsModel.GetListId ();
		allListsModel.CreateNewList(todoEditListView.editList);
		SetCurrentList(todoView.CreateListView (todoEditListView.editList));

	}

	public void EditCurrentList(){
		todoEditListView.onFinishEdit = UpdateCurrentList;
		todoEditListView.ShowEditListView (currentTodoListModel);
	}

	public void UpdateCurrentList(){
		currentTodoListModel.SaveTodoList ();
		todoView.SetCurrentListTitle (currentTodoListModel.listName);
	}
	#endregion
}
}
