using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class FeedScript : MonoBehaviour {
    private bool m_Feed;
    private Animator horse;
    private PlayerScript playerScript;
    private int feedCost = 20;
    //decided to make it cost more and make it one shot,
    //so if successful then you got it, if not then you dont
    //private int lastPosLvl;
    //private int currentBag = 0;
    private int feedSpeedInt;

    // Use this for initialization
    void Start () {
        //Debug.Log("feed script present");
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        feedSpeedInt = 0;
        horse = GameObject.FindGameObjectWithTag("Horse").GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!m_Feed)
        {
            m_Feed = CrossPlatformInputManager.GetButtonDown("Feed");
            Debug.Log("Button is now ready");
        }
        else
        {
            if (playerScript.money > feedCost)
            {
                playerScript.money -= feedCost;
                horse.SetTrigger("FeedPressed");
                //playerScript.feed();
                if(Random.Range(0,2) >= 1)
                {
                    feedSpeedInt++;
                    //playerScript.feedSpeedInt++;
                }
                Debug.Log("level " + feedSpeedInt);
                horse.SetInteger("FeedSpeedInt", feedSpeedInt);

            }
            m_Feed = false;
        }
	}

    public void CatchAnimal()
    {
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
