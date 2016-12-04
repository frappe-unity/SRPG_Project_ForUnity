using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

    [SerializeField] private UnitController unit;
    [SerializeField] private Text tex;

    public enum TextCommand
    {
        STAY,
        TURN,
    }

    public TextCommand textCommand;
    public int number = 0;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        switch (textCommand)
        {
            case TextCommand.STAY:
                number = unit.stayCount;
                break;
            case TextCommand.TURN:
                break;
        }
        tex.text = number.ToString();
    }
}
