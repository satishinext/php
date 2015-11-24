using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingArticle : MonoBehaviour 
{
	#region [Public Variables]
	public Sprite[] loadingSprites;
	public float runAnimationSpeed;
	public bool once;
	#endregion
	#region [Private Variables]
	float AnimState;
	float spriteCounter;
	
	#endregion
	#region [Public Methods]
	#endregion
	#region [Unity Methods]
	void Start()
	{
		spriteCounter = 0;
		once = false;
	}
	void Update()
	{
		if(!once)
		{
			playAnims();
		}
		
		
	}
	#endregion
	#region [Private Methods]
	void  playAnims ( )
	{
		if (spriteCounter > loadingSprites.Length - 1)
		{
			spriteCounter = 0;
		}
		
		
		this.GetComponent<Image>().sprite = loadingSprites [Mathf.RoundToInt (spriteCounter)];
		spriteCounter += runAnimationSpeed *Time.deltaTime ;
		
		if (spriteCounter > loadingSprites.Length - 1)
		{
			
			spriteCounter = 0;
			this.GetComponent<Image>().sprite = loadingSprites [0];
			//	once = true;
			
		}
	}
	#endregion
}
