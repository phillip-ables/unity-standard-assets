using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BuildingScript : MonoBehaviour {
    public GameObject cactusPrefab;
    public float distanceToBuild = 2.0f;

    private bool m_Build;
    private bool m_Building;
    private Transform playerTransform;
    private PlayerScript player;
    private Vector3 wallPos;


	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
        player = GetComponent<PlayerScript>();

        m_Building = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!m_Build)
        {
            m_Build = CrossPlatformInputManager.GetButtonDown("Build");
        }


        /*
        if (!m_Build)  // so this  should be because you cant double tap and build more or maybe you can idk
        {
            //so while building you cant rebuild
            m_Build = true;  // because its true
            //this will stay true until after the animation
            //i feel like this should be a 
            //coroutine, after
            
            //animation
            //coroutine then build
            BuildBuilding();

        }
        */
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


        //FINALLY
        m_Build = false;

    }

    public void PrintTest()
    {
        Debug.Log("This button works, just not that script!");
    }
}
