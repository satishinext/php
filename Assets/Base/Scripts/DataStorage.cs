using UnityEngine;

namespace Base
{
	public sealed class DataStorage
	{
		private static readonly DataStorage instance = new DataStorage();
		private string passwordPrefix;
		private string valuePrefix;
		private StringCipher sc;
		
		static DataStorage() 
		{
		}
		
		private DataStorage() 
		{
			passwordPrefix = "kps";
			valuePrefix    = "muf";
			sc             = StringCipher.Instance;
		}
		
		public static DataStorage Instance 
		{ 
			get { return instance; }
		}
		
		/* 
		 * Removes all keys and values. Use with caution.
		 */
		public void DeleteAll()
		{
			PlayerPrefs.DeleteAll();
		}
		
		/* 
		 * Removes key and its corresponding value.
		 */
		public void DeleteKey(string key)
		{
			PlayerPrefs.DeleteKey(key);
		}
		
		/* 
		 * Returns the value corresponding to key if it exists.
		 */
		public float GetFloat(string key, float defaultValue = 0f)
		{
			float value;
			float result;
			
			if(float.TryParse(GetString(key, defaultValue.ToString()), out result))
			{
				value = result;
			}
			else
			{
				value = defaultValue;
			}
			
			return value;
		}
		
		/* 
		 * Returns the value corresponding to key if it exists.
		 */
		public int GetInt(string key, int defaultValue = 0)
		{
			int value;
			int result;
			
			if(int.TryParse(GetString(key, defaultValue.ToString()), out result))
			{
				value = result;
			}
			else
			{
				value = defaultValue;
			}
			
			return value;
		}
		
		public bool GetBool(string key, bool defaultValue = false)
		{
			bool value;
			bool result;
			
			if(bool.TryParse(GetString(key, defaultValue.ToString()), out result))
			{
				value = result;
			}
			else
			{
				value = defaultValue;
			}
			
			return value;
		}
		
		/* 
		 * Returns the value corresponding to key if it exists.
		 */
		public string GetString(string key, string defaultValue = "")
		{
			string value           = defaultValue;
			string encryptedstring = PlayerPrefs.GetString(key);
			
			if(encryptedstring.Length > 0)
			{
				try
				{
					string decryptedstring   = sc.Decrypt(encryptedstring, passwordPrefix + key);
					int    valuePrefixLength = valuePrefix.Length;
					
					if(decryptedstring.Length >= valuePrefixLength && decryptedstring.Substring(0, valuePrefixLength) == valuePrefix)
					{
						value = decryptedstring.Substring(valuePrefixLength);
					}
				}
				catch(System.Exception e)
				{
					if(e.Message.Length > 0){}
				}
			}
			
			return value;
		}
		
		/* 
		 * Returns true if key exists in the preferences.
		 */
		public bool HasKey(string key)
		{
			return PlayerPrefs.HasKey(key);
		}
		
		/* 
		 * Writes all modified to disk.
		 */
		public void Save()
		{
			PlayerPrefs.Save();
		}
		
		/* 
		 * Sets the value identified by key.
		 */
		public void SetFloat(string key, float value)
		{
			SetString(key, value.ToString());
		}
		
		/* 
		 * Sets the value identified by key.
		 */
		public void SetInt(string key, int value)
		{
			SetString(key, value.ToString());
		}
		
		public void SetBool(string key, bool value)
		{
			SetString(key, value.ToString());
		}
		
		/* 
		 * Sets the value identified by key.
		 */
		public void SetString(string key, string value)
		{
			string encryptedstring = sc.Encrypt(valuePrefix + value, passwordPrefix + key);
			
			PlayerPrefs.SetString(key, encryptedstring);
		}
		
		public string AttendgEvents
		{
			get {return GetString("k_1", "[]");}
			set {       SetString("k_1", value);}
		}
	}
}