using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCharaMove : MonoBehaviour {

    public GameObject gm;
    float time = 0;
    public int moveTime = 3;
    public float speed = 0.008F;
    public float moveX = 0.1F;
    float alfa;
    float red, green, blue;

    public enum ImageMove
    {
        Chara = 0,
        TitleLogo = 1,
    }

    public ImageMove img;

	// Use this for initialization
	void Start () {
        switch (img)
        {
            case ImageMove.Chara:
                alfa = 0;
                red = GetComponent<Image>().color.r;
                green = GetComponent<Image>().color.g;
                blue = GetComponent<Image>().color.b;
                time = 0;
                StartCoroutine(second());
                break;
            case ImageMove.TitleLogo:
                alfa = 0;
                red = GetComponent<Image>().color.r;
                green = GetComponent<Image>().color.g;
                blue = GetComponent<Image>().color.b;
                time = 0;
                StartCoroutine(second());
                break;
        }
        
	}
	
	// Update is called once per frame
	void Update () {
        switch (img)
        {
            case ImageMove.Chara:
                GetComponent<Image>().color = new Color(red, green, blue, alfa);
                alfa += speed;
                if (time <= moveTime)
                {
                    moveX = moveX * 1.06F;
                    transform.position += new Vector3(-moveX, 0, 0);
                }
                if(time == moveTime + 1)
                {
                    if (!gm.GetComponent<TitleScript>().charaOn)
                    {
                        gm.GetComponent<TitleScript>().charaOn = true;
                    }
                }
                break;
            case ImageMove.TitleLogo:
                GetComponent<Image>().color = new Color(red, green, blue, alfa);
                alfa += speed * 2 / 3;
                if (time == moveTime + 1)
                {
                    if (!gm.GetComponent<TitleScript>().titleOn)
                    {
                        gm.GetComponent<TitleScript>().titleOn = true;
                    }
                }
                break;
        }
        
    }

    IEnumerator second()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5F);
            time++;
        }
    }
}
