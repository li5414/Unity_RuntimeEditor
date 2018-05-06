using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {


    [SerializeField]
    private Mesh m_resourceMesh;
	// Use this for initialization
	void Start () {
     //   Mesh mesh = new Mesh();
     //   Resources.UnloadAsset(mesh);

        Resources.UnloadAsset(m_resourceMesh);

        //Resources.LoadAsync()
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
