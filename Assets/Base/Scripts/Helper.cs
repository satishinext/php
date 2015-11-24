using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Base
{
	public sealed class Helper
	{
		private static readonly Helper instance = new Helper();
		private Vector2 gridSize;
		private float   pixelToGridRatio;
		
		static Helper() 
		{
		}
		
		private Helper() 
		{
			
		}
		
		public void InitSettings()
		{
			float gridSizeX  = 320f;
			pixelToGridRatio = Screen.width / gridSizeX;
			gridSize         = new Vector2(gridSizeX, pixelToGrid(Screen.height));
		}
		
		public static Helper Instance 
		{ 
			get { return instance; }
		}
		
		public Vector2 GridSize
		{
			get { return gridSize; }
		}
		
		public int gridToPixel(float grid)
		{
			return (int) Mathf.Round(grid * pixelToGridRatio);
		}
		
		public float pixelToGrid(float pixel)
		{
			return pixel / pixelToGridRatio;
		}
		
		public Vector3 RotateVect(Vector3 vector, float angle, Vector3 aroundVect) // angle in degree
		{
			return Quaternion.AngleAxis(angle, aroundVect) * vector;
		}
		
		public float DegreeToRad(float degree)
		{
			return degree * Mathf.PI / 180f;
		}
		
		public float SignedAngleBetween(Vector3 a, Vector3 b, Vector3 n)
		{
			// angle in [0,180]
			float angle = Vector3.Angle(a,b);
			float sign = Mathf.Sign(Vector3.Dot(n,Vector3.Cross(a,b)));
			
			// angle in [-179,180]
			float signed_angle = angle * sign;
			
			return signed_angle;
		}
		
		public float SignedAngleBetween(Vector2 from, Vector2 to)
		{
			float ang = Vector2.Angle(from, to);
			Vector3 cross = Vector3.Cross(from, to);
			
			if (cross.z > 0) ang = 360 - ang;
			
			return ang;
		}
		
		public float saturateValue(float val, float min, float max)
		{
			if     (val > max) val = max;
			else if(val < min) val = min;
			
			return val;
		}
		
		public int saturateValueInt(int val, int min, int max)
		{
			if     (val > max) val = max;
			else if(val < min) val = min;
			
			return val;
		}
		
		public void ShuffleList<T>(List<T> items)
		{  
			T item;
			int n = items.Count;  
			
			while (n > 1)
			{  
				n--;  
				int k = Random.Range(0, n);
				
				item = items[k];  
				items[k] = items[n];  
				items[n] = item;  
			}  
		}
		
		public Material GetMaterialByNameFromMaterials(string name, Material[] materials)
		{
			Material material = null;
			
			foreach(Material mat in materials)
			{
				if(mat.name == name)
				{
					material = mat;
					break;
				}
			}
			
			return material;
		}
		
		public string ReverseString(string s)
		{
			char[] charArray = s.ToCharArray();
			System.Array.Reverse( charArray );
			return new string(charArray);
		}
		
		public void MuteAllSound(bool mute = true)
		{
			AudioListener.volume = mute ? 0 : 1;
		}
		
		public int CalcSignature(string str)
		{
			int signr = 0;
			
			for(int i = 0; i < str.Length; i++)
			{
				signr += (i + 4) * ((int)str[i]);
			}
			
			return signr;
		}
		
		/*
		 * get dui with signature
		 */
		public string CalcDeviceId()
		{
			string dui    = SystemInfo.deviceUniqueIdentifier;
			int duiSignr  = CalcSignature(dui);
			string platfm = (Config.Instance.CurrPlatform == PlatformType.android) ? "a" : "i";
			
			return dui+"-"+duiSignr+"p"+platfm;
		}
		
		/*
		 * utcDateTimeStr like 2015-02-19 00:00:00
		 */
		public string UtcToLocDateTime(string utcDateTimeStr)
		{
			System.DateTime utcDateTime = System.DateTime.SpecifyKind(System.DateTime.Parse(utcDateTimeStr), System.DateTimeKind.Utc);
			System.DateTime locDateTime = utcDateTime.ToLocalTime();
			
			return locDateTime.ToString();
		}
		
		/*
		 * utcDateTimeStr like 2015-02-19 00:00:00
		 */
		public bool IsUtcDateTimeBeforeNow(string utcDateTimeStr)
		{
			return IsUtcDateTimeBeforeNow(MkUtcDateTime(utcDateTimeStr));
		}
		
		public bool IsUtcDateTimeBeforeNow(System.DateTime utcDateTime)
		{
			System.DateTime now = System.DateTime.UtcNow;
			
			return (System.DateTime.Compare(utcDateTime, now) < 0);
		}
		
		/*
		 * dateTimeStr like 2015-02-19 00:00:00
		 */
		public bool IsDateTimeBeforeRef(string dateTimeStr, string refDateTimeStr)
		{
			System.DateTime    dateTime = System.DateTime.Parse(dateTimeStr);
			System.DateTime refDateTime = System.DateTime.Parse(refDateTimeStr);
			
			return (System.DateTime.Compare(dateTime, refDateTime) < 0);
		}
		
		/*
		 * utcDateTimeStr like 2015-02-19 00:00:00
		 */
		public System.DateTime MkUtcDateTime(string utcDateTimeStr)
		{
			return System.DateTime.SpecifyKind(System.DateTime.Parse(utcDateTimeStr), System.DateTimeKind.Utc);
		}
		
	}
}
