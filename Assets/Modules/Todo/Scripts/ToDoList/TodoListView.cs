using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Module.Todo{
public class TodoListView : MonoBehaviour, IPointerClickHandler {

	public Text listName;
	public Text itemsAmount;

	public TodoListModel listModel;
	public Action <TodoListView> onSelect;

	public void InitListView(TodoListModel listModel){
		this.listModel = listModel;

		listName.text = listModel.listName;
		itemsAmount.text = "(" + listModel.items.Count + ")";

	}

	public void UpdateListItemsAmount(){
		itemsAmount.text = listModel.items.Count.ToString();
	}
	
	#region IPointerClickHandler implementation
	public void OnPointerClick (PointerEventData eventData)
	{Debug.Log ("OnPointerClick");
		if (onSelect != null)
			onSelect (this);
	}
	#endregion


}
}
