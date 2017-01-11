using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TitleScript : MonoBehaviour {

    public bool charaOn = false;
    public bool titleOn = false;
    public bool buttonOn = false;


    public float speed = 0.05f;
    public GameObject selectButton;
    public GameObject saveButton;
    public GameObject buttonObj;

    void Start()
    {
        buttonObj.SetActive(false);
        charaOn = false;
        titleOn = false;
        buttonOn = false;
    }
    
    void Update()
    {
        if(charaOn && titleOn)
        {
            buttonObj.SetActive(true);
            selectButton = EventSystem.current.currentSelectedGameObject;
            saveButton = selectButton;
            buttonOn = true;
        }
        if (buttonOn)
        {
            selectButton = EventSystem.current.currentSelectedGameObject;
            Debug.Log(selectButton);
            // Debug.Log(saveButton);
            if (selectButton != saveButton)
            {
                saveButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            float toColor = selectButton.gameObject.GetComponent<Image>().color.a;
            // Alphaが0 または 1になったら増減値を反転
            if (toColor < 0 || toColor > 1)
            {
                speed = speed * -1;
            }
            // Alpha値を増減させてセット
            saveButton = selectButton;
            Debug.Log(selectButton);
            selectButton.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, toColor + speed);
        }
    }
}
