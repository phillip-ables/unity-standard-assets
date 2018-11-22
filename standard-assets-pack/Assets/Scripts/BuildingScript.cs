using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour {
    private PlayerScript player;

	// Use this for initialization
	void Start () {
        //player = GameObject.FindGameObjectWithTag("Player"); 
        player.GetComponent<PlayerScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetBuilding() 
    {

    }

    public void BuildBuilding()  // this will eventually take a player type
    {
        
    }
}
