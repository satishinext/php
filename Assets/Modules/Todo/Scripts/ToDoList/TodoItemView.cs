using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace Module.Todo{
	public class TodoItemView : MonoBehaviour, IPointerClickHandler {
		
		public TodoItemModel item;
		public InputField input;
		public Toggle toggle;

		public Action<TodoItemView> onDestroy;
		public Action<TodoItemView> onUpdate;
		public GameObject buttonsPanel;
		public LayoutElement layout;

		public void InitItem(TodoItemModel item)
		{
			this.item = item;
			input.text = item.text;
			input.textComponent.text = item.text;
			toggle.isOn = item.completed;
			CompleteItem (item.completed);
		}

		public void CompleteItem(bool complete){
			complete = toggle.isOn;
			bool needToSave = false;
			if (item.completed != complete) {
				needToSave = true;
				item.SetCompleted (complete);
			}
			Color c = input.textComponent.color;
			if (complete) {
				input.textComponent.color = new Color (c.r, c.g, c.b, 0.2f);
				//input.enabled = false;
			} else {
				input.textComponent.color = new Color (c.r, c.g, c.b, 1f);
				//input.enabled = true;
			}
			if (needToSave)
				UpdateItem ();
		}

		public void SetText(string text){
			bool needToSave = false;
			if (item.text != input.text)
				needToSave = true;
			item.text = input.text;
			if (needToSave)
				UpdateItem ();
			input.enabled = false;
		}

		public void UpdateItem(){
			if (onUpdate != null)
				onUpdate (this);
		}

		public void DestroyItem(){
			if (onDestroy != null)
				onDestroy (this);
			onDestroy = null;
			onUpdate = null;
			Destroy (gameObject);
		}

		public void StartEdit()
		{
			Debug.Log ("StartEdit");
			input.enabled = true;
			input.ActivateInputField ();
		}

		#region IPointerClickHandler implementation
		public void OnPointerClick (PointerEventData eventData)
		{
			buttonsPanel.SetActive (!buttonsPanel.activeSelf);
			if (buttonsPanel.activeSelf)
				layout.preferredHeight = 160;
			else
				layout.preferredHeight = 80;
		}
		#endregion
	}
}
