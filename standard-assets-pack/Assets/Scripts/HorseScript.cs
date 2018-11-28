using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseScript : MonoBehaviour {
    public Transform feet;
    public GameObject rabbit;
    public GameObject dusty;

    private bool isTwoLeg;
    private bool isFourLeg;
    private bool isBunnyLeg;
    private bool isRatLeg;

	// Use this for initialization
	void Start () {
        isTwoLeg = true;
	}
	
    // i dont think we need an update function
    private void getDustyLegs()
    {
        for (int i = 0; i < 4; i++)
        {
            Instantiate(dusty, feet.GetChild(i).transform.position, transform.rotation);
        }
    }

    public void ChangePosition()
    {
        // im gonna do a switch here
    }
}
