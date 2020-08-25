using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T _instance;

	protected virtual void Awake()
	{
		if (_instance !=null && _instance != this)
		{
			Destroy(this);
		}
		else
		{
			DontDestroyOnLoad(this);
			_instance = (T)FindObjectOfType(typeof(T));
		}
	}
	
	public static T Instance
	{
		get
		{
			if(_instance==null)
			{
				_instance = (T)FindObjectOfType(typeof(T));
				if(_instance==null)
				{
					Debug.Log("Instance of a " + typeof(T) + " does not exist");
				}
			}
			return _instance;
		}
	}	
}

