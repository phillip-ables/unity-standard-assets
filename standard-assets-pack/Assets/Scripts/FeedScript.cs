using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FeedScript : MonoBehaviour {
    public HorseScript horseScript;

    private bool m_Feed;
    private PlayerScript playerScript;
    private int feedCost = 20;
    private int lastPosLvl;
    private int currentBag = 0;

    // Use this for initialization
    void Start () {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!m_Feed)
        {
            m_Feed = CrossPlatformInputManager.GetButtonDown("Feed");
        }
        else
        {
            if(playerScript.money > feedCost)
            {
                GetAnimal();
            }
        }
	}

    public void GetAnimal()
    {
        playerScript.money -= feedCost;
        //call animation for furry animal to mistily appear timidly look then
        int randomInt = Random.Range(0, 2);
        Debug.Log(randomInt);
        if(randomInt > 0)
        {
            // if captured
            //throw in sack
            currentBag++;
            switch (currentBag)
            {
                case 7:
                case 3:
                case 1:
                    horseScript.ChangePosition();
                    break;
                default:
                    Debug.Log("You haven't caught enought!!");
                    break;
            }

            
        }
        else
        {
            //return camera
        }

        m_Feed = false;
    }
}
