using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleButtonScript : MonoBehaviour {
    
    [SerializeField] private SoundController soundcontroller;

    public enum State
    {
        Start,
        Load,
        Settings
    }

    public State states;

	// Use this for initialization
	void Start () {
        soundcontroller = GameObject.Find("SoundManager").GetComponent<SoundController>();
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
        soundcontroller.SoundPlayer(1);
        Invoke("Act", 0.5F);
    }

    public void Act()
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
