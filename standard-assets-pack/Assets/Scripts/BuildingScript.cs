using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour {
    public GameObject cactusPrefab;
    public float distanceToBuild = 2.0f;

    private Transform playerTransform;
    private PlayerScript player;
    private Vector3 wallPos;


	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
        player = GetComponent<PlayerScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BuildBuilding()  // this will eventually take a player type
    {
        Debug.Log("This should be building something");
        player.money -= 20;
        int randomIndex = Random.Range(0, 3);
        wallPos = playerTransform.position;
        wallPos.z += distanceToBuild;
        //there needs to be a corutine that 
        //plays animation before it builds
        switch (randomIndex)
        {
            case 3:
                Instantiate(player.buildingPrefab, wallPos, playerTransform.rotation);  //rotation may be backwards
                break;
            case 2:
                Instantiate(player.fencePrefab, wallPos, playerTransform.rotation);  //rotation may be backwards
                break;
            default:
                Instantiate(cactusPrefab, wallPos, playerTransform.rotation);  //rotation may be backwards
                break;
        }

    }

    public void PrintTest()
    {
        Debug.Log("This button works, just not that script!");
    }
}
