using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace Module.Todo{
public class TodoView : MonoBehaviour {

	public Text currentListTitle;
	public GameObject allListView;
	public Transform allListsContentHolder;
	public GameObject oneListViewPrefab;

	public GameObject itemPrefab;
	public Transform contentItemsHolder;
	public InputField newItemInput;

	public List<TodoListView> listsView = new List<TodoListView>();

	List <TodoItemView> itemsView = new List<TodoItemView>();

	public void ShowAllListView(){
		allListView.SetActive (true);
	}

	public void HideAllListView(){
		allListView.SetActive (false);
	}

	public TodoListView CreateListView(TodoListModel todoListModel){
		TodoListView listView = Instantiate (oneListViewPrefab).GetComponent<TodoListView> ();
		listView.InitListView (todoListModel);
		listView.transform.SetParent (allListsContentHolder);
		listView.transform.localScale = Vector3.one;
		listsView.Add (listView);
		return listView;
	}

	public void SetCurrentListTitle(string listName){
		currentListTitle.text = listName;
	}

	public List<TodoItemView> FillItems(TodoListModel currentTodoListModel){
		foreach (var item in currentTodoListModel.items) {
			CreateOrUpdateItemView(item);
		}
		return itemsView;
	}

	public TodoItemView CreateOrUpdateItemView(TodoItemModel itemModel){
		TodoItemView itemView = itemsView.FirstOrDefault(x => x.item.Equals(itemModel));
		if (itemView == null) {
			itemView = Instantiate (itemPrefab).GetComponent<TodoItemView> ();
			itemView.InitItem (itemModel);
			itemView.transform.SetParent (contentItemsHolder);
			itemView.transform.localScale = Vector3.one;
			itemsView.Add(itemView);
		} else {
			itemView.InitItem(itemModel);
		}
		return itemView;
	}
	
	public void ClearItems(){
		foreach (var item in itemsView)
			Destroy (item.gameObject);
		itemsView.Clear ();
	}

	public void RemoveItem(TodoItemView itemView){
		itemsView.Remove (itemView);
	}
}
}
