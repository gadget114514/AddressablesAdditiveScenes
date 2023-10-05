using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.Events;
public class AddressablesAdditiveSceneManager : MonoBehaviour
{
	
	public Dictionary<string, SceneInstance> LoadedScenes;
	static public AddressablesAdditiveSceneManager instance;
	// Start is called before the first frame update
	void Awake() { instance = this; 
		LoadedScenes = new Dictionary<string, SceneInstance>();
	}
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
