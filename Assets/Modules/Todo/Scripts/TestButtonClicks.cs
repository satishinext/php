using UnityEngine;
using System.Collections;
using System;

public class TestButtonClicks : MonoBehaviour {

	// Use this for initialization
	public void OnClickMarkTODO()
	{
		Application.LoadLevel("MdlsTodoMrkLst");
	}
	public void OnClickAddNewTask()
	{
		Application.LoadLevel("MdlsTodoAddTsk");
	}
	public void OnClickShowAll()
	{
		Application.LoadLevel("MdlsTodoFulLst");
	}
}
