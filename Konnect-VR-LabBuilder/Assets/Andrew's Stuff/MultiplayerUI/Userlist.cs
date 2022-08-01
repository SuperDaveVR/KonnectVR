using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Singleton to keep track of user names to be used later
public class Userlist : MonoBehaviour
{
	public static Userlist Instance{ get; private set; }
	//private static int counter = 0;
	private static List<string> ListofNames = new List<string>();
	public InputField userName;
	
	// If there is an instance, and it's not me, delete myself.
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	// Traverse list of names and set the last added name to the
	// Instance name of the user
	public static void setUsername()
	{
		foreach(string name in ListofNames)
		{
			Instance.userName.text = name;
			//Debug.Log("Name set to: " + name);
		}
	}

	// Every time a user joins, add their name to the list
	// To keep track of duplicate names ~ Still needs some working for error catching
	public static void addToList(InputField name)
	{
		if(ListofNames.Contains(name.text))
		{
			Debug.Log("Name already exists!");
		}
		else
		{
			ListofNames.Add(name.text);
			Debug.Log(name.text + " has been added");
		}
	}
}
