using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour {
    
    public float speed = 0.05f;


    void Update()
    {
        float toColor = this.gameObject.GetComponent<Image>().color.a;
        // Alphaが0 または 1になったら増減値を反転
        if (toColor < 0 || toColor > 1)
        {
            speed = speed * -1;
        }
        // Alpha値を増減させてセット
        this.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, toColor + speed);
    }
}
