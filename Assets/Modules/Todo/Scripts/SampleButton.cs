using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SampleButton : MonoBehaviour {

	public Button button;
	public Text buttonText;
	public void addlistner()
	{
		button.onClick.AddListener (() => TestListener(button));
	}
	public void TestListener(Button btn)
	{
		btn.interactable =false;
		addToServer ();
		//Debug.Log (buttonText.text);
	}
	public void addToServer()
	{
		////code to be written here for further purpose
	}
	public void setIntractable(int i)
	{
		if (i == 1) {
			button.interactable = true;
			button.image.color = Color.grey;
		} else {
			button.interactable = true;
			button.image.color = Color.yellow;
		}
	}
	public int getCheckValue()
	{
		int i = 0;
		if (button.interactable) {
			return i;
		}
		else{
			i=1;
			return i;
		}
			
	}

	public void intract()
	{
		button.interactable = false;
	}
}

