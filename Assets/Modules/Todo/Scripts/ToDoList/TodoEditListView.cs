using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Module.Todo{
public class TodoEditListView : MonoBehaviour {

	public InputField inputName;
	public Toggle shareWithFriends;
	public Action onFinishEdit;
	public TodoListModel editList;

	public void ShowEditListView(TodoListModel listModel){
		editList = listModel;

		if (editList != null) {
			inputName.text = listModel.listName;
			shareWithFriends.isOn = listModel.shareWithAllFrineds;
		} else {
			inputName.text = "";
			shareWithFriends.isOn = false;
			editList = new TodoListModel();
		}
		gameObject.SetActive (true);
	}

	public void OnEditName(){
		editList.listName = inputName.text;
	}

	public void OnChangeShareWithFriends(){
		if(editList != null)
		editList.shareWithAllFrineds = shareWithFriends.isOn;
	}

	public void OFinish(){
		if (onFinishEdit != null)
			onFinishEdit ();
		onFinishEdit = null;
		gameObject.SetActive (false);
	}

	public void OnCancle(){
		gameObject.SetActive (false);
		editList = null;
	}
	
}
}
