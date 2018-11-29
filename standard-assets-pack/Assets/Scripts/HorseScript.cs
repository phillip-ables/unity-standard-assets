using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseScript : MonoBehaviour
{
    public Transform feet;
    public GameObject rabbit;
    public GameObject dusty;
    public GameObject bunny;
    public int positionLevel;
    public float ratHeight = 0.4f;

    private bool isFourLeg;
    private int legs = 4;

    // Use this for initialization
    void Start()
    {
        positionLevel = 0;
        //Debug.Log("Yooo Horse is hurr!!");
        GetDustyLegs();
    }

    // i dont think we need an update function
    private void GetDustyLegs()
    {
        Vector3 dustyRiderPos = transform.position;
        dustyRiderPos.y += ratHeight;
        for (int i = 0; i < legs; i++)
        {
            Instantiate(dusty, feet.GetChild(i).transform.position, transform.rotation);
        }
        transform.position = dustyRiderPos;
        Debug.Log("Bunnies should be instantiated");
    }

    private void GetBunnyLegs()
    {
        /*
         * YOU GET THE IDEA
        for (int i = 0; i < legs+1; i++)
        {
            if (i % 2 == 0)
            {
                Instantiate(bunny, feet.GetChild(i - 1).transform.position - feet.GetChild(i), transform.rotation);
            }
        }
        */
    }

    public void ChangePosition()
    {
        positionLevel++;
        //dont forget that we have to pass our bool to the feed script to know which animation for which animal we are suppose to be catching
        switch (positionLevel)
        {
            case 3:
                GetDustyLegs();
                break;
            case 2:
                GetBunnyLegs();
                break;
            case 1:
                isFourLeg = true;
                break;
            default:
                isFourLeg = false;
                break;
        }
    }
}
