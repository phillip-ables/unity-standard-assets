using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    Collider m_Collider;
    Vector3 m_Center;
    public Vector3[] dustParticles;
    public int startPlayers;
    public int m_stormSpeed;
    public int m_radianLength;



    void Start()
    {
        //Fetch the Collider from the GameObject
        m_Collider = GetComponent<Collider>();
        //Fetch the center of the Collider volume
        m_Center = m_Collider.bounds.center;
        //This gives me the center so everything else is garbage







        CreateDustStorm(startPlayers);


    }

    public Vector3[] UpdateDustBounds()
    {

        //CreateDustStorm();

        return dustParticles;
    }

    void CreateDustStorm(int startPlayers)
    {

    }

}
