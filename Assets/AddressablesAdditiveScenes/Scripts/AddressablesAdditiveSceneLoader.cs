using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.Events;

public class AddressablesAdditiveSceneLoader : MonoBehaviour
{
	public enum Mode { None, Start };
	public Mode mode;
	public string[] LoadSceneAddressables;
	public string[] UnloadScenes;
	AsyncOperationHandle<SceneInstance> [] handles;

	public bool OnActivateFlag;
	public enum LoadStatus { None, Loading, Activating };
	public LoadStatus status;
	public UnityEvent OnLoadScenesCompleted;
	public UnityEvent OnActivateScenesCompleted;
	
	int n_loadcompleted;
	int n_activatecompleted;
	int n_activate;
	
    // Start is called before the first frame update
    void Start()
	{
		//
	    if (mode == Mode.Start) Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	public bool Load()
	{
		if (status != LoadStatus.None) {
			
			Debug.Log("busy");
			return false;
		}
		StartCoroutine("LoadScene2Coroutine");
		return true;
	}
	public void DoTriggered() { if (Load()) Debug.Log("success"); else Debug.Log("fail"); 
		Unload(); }
	public bool Unload() {
		if (UnloadScenes == null) return true;
		for (int i = 0; i < UnloadScenes.Length; i++) {
			SceneInstance si = AddressablesAdditiveSceneManager.instance.LoadedScenes[UnloadScenes[i]];
			Debug.Log("Unload " + UnloadScenes[i]);
			Addressables.UnloadSceneAsync(si);
			AddressablesAdditiveSceneManager.instance.LoadedScenes.Remove(UnloadScenes[i]);
		}	
		return true;
	}
	IEnumerator LoadScene2Coroutine()
	{
		n_loadcompleted = 0;
		n_activatecompleted = 0;
		status = LoadStatus.Loading;
		handles = new AsyncOperationHandle<SceneInstance> [LoadSceneAddressables.Length];
		//	yield return SceneManager.LoadSceneAsync("Scene2", LoadSceneMode.Additive);
		//  Debug.Log(GameObject.Find("TestGameObject"));
		for (int i = 0; i < LoadSceneAddressables.Length; i++) {
			AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(LoadSceneAddressables[i], LoadSceneMode.Additive, OnActivateFlag);
			handles[i] = handle;

			handle.Completed += SceneLoadComplete;
			yield return handle;
		}
		while (n_loadcompleted < LoadSceneAddressables.Length) {
			yield return null;
		}
		
	    LoadScene2Completed();
	}
	


	void SceneLoadComplete(AsyncOperationHandle<SceneInstance> obj)
	{
		if (obj.Status == AsyncOperationStatus.Succeeded)
		{
			Debug.Log("Load SceneInstance name=" + obj.Result.Scene.name + " Success");
			AddressablesAdditiveSceneManager.instance.LoadedScenes.Add(obj.Result.Scene.name, obj.Result);
			//	handle = obj;
		} else {
			Debug.Log("Load SceneInstance name=" + obj.Result.Scene.name + " Fail");
		}
		n_loadcompleted++;
	}
	void LoadScene2Completed() {
		OnLoadScenesCompleted?.Invoke();
		
		if (OnActivateFlag == true) {
			
			OnActivateScenesCompleted?.Invoke();
			status = LoadStatus.None; 
		} else
			status = LoadStatus.Activating;
		//		handles = null;
	}
	
	public bool Activate() {
		if (status != LoadStatus.Activating) {
			
			Debug.Log("not activatig");
			return false;
		}
		StartCoroutine("ActivateScene2Coroutine");
		return true;
	}
	IEnumerator ActivateScene2Coroutine()
	{
		status = LoadStatus.Activating;
		
		//	yield return SceneManager.LoadSceneAsync("Scene2", LoadSceneMode.Additive);
		//  Debug.Log(GameObject.Find("TestGameObject"));
		n_activate = 0;
		for (int i = 0; i < handles.Length; i++) {
			if (handles[i].Status == AsyncOperationStatus.Succeeded) {
				n_activate++;	
			}
		}
		Debug.Log("n_activate="+ n_activate);
		for (int i = 0; i < handles.Length; i++) {
			if (handles[i].Status == AsyncOperationStatus.Succeeded) {
				AsyncOperation h = handles[i].Result.ActivateAsync();
				h.completed += SceneActivateCompleted;
				yield return h;
			}
		}
		while (n_activatecompleted < n_activate) {
			yield return null;
		}
		ActivateScene2Completed();
	}
	void SceneActivateCompleted(AsyncOperation obj)
	{
		if (obj.isDone)
		{
			Debug.Log("Activate "  + obj + " Success");
			//	handle = obj;
		} else {
			Debug.Log("Activate " + obj + " Fail");
		}
		n_activatecompleted++;
	}
	
	void ActivateScene2Completed() {
		OnActivateScenesCompleted?.Invoke();
		status = LoadStatus.None;
		//		handles = null;
		
	}
}
