using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

  public float radiusMultiplier = 1.0f;
  public float Radius {
    get {
      return transform.lossyScale.x;
    }
  }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localScale = radiusMultiplier * new Vector3(1.0f, 1.0f, 1.0f);
	}
}
