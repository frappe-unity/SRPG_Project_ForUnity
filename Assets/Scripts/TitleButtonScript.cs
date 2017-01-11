using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleButtonScript : MonoBehaviour {
    
    public enum State
    {
        Start,
        Load,
        Settings
    }

    public State states;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject != EventSystem.current.currentSelectedGameObject)
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
	}

    public void OnClick()
    {
        switch (states)
        {
            case State.Start:
                SceneManager.LoadScene(1);
                break;
            case State.Load:
                break;
            case State.Settings:
                break;
        }
    }
}
