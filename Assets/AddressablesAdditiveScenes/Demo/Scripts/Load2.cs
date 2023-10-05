using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load2 : MonoBehaviour
{
	
	public AddressablesAdditiveSceneLoader loader;
	bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    if (Input.GetKey(KeyCode.Alpha0) && flag == false) { flag = true; loader.DoTriggered();}
    }
}
