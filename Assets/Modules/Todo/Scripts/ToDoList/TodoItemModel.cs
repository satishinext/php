using UnityEngine;
using System.Collections;

namespace Module.Todo{
public class TodoItemModel {

	public int id;
	public string text;
	public bool completed;

	public TodoItemModel(){
	}

	public TodoItemModel(int id, string text, bool completed){
		this.id = id;
		this.text = text;
		this.completed = completed;
	}

	public void SetCompleted(bool completed){
		this.completed = completed;
	}

	public override bool Equals (object obj)
	{
		TodoItemModel _tim = obj as TodoItemModel;
		return _tim.id == this.id && _tim.text == text;
	}
}
}
