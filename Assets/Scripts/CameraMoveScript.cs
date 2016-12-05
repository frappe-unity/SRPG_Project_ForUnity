using UnityEngine;
using System.Collections;

public class CameraMoveScript : MonoBehaviour {
    
    [SerializeField] private GameObject cirsor;
    public float z = -60F;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(cirsor.transform.position.x, transform.position.y, cirsor.transform.position.z + z);
	}
}
