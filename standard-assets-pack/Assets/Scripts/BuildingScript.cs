using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;

public class BuildingScript : MonoBehaviour {
    public GameObject cactusPrefab;
    public float distanceToBuild = 2.0f;

    private Transform playerTransform;
    private PlayerScript player;
    private Vector3 wallPos;

    //okay so these are an exact replica of all jump vars
    [SerializeField] private float m_BuildSpeed;
    [SerializeField] private LerpControlledBob m_BuildBob = new LerpControlledBob();  // Don't think this is an official unity type
    [SerializeField] private AudioClip m_BuildSound;  // need this
    //that will be a quick hammer along with a saw mill sound over the back of it

    private bool m_Build;

    //this one isnt building but its used in the jumping unity code soooo
    private Vector3 m_BuildDir = Vector3.zero;
    private CharacterController m_CharacterController;  // this will probably  be left up to the fps controller
    private bool m_PreviouslyBuilding;  // not sure if i need this or not
    private bool m_Building;
    private AudioSource m_AudioSource;

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
            Debug.Log("m_Build is now true, you ready to build i think, or are you building?");
        }

        // NOT JUMPING
        if(!m_PreviouslyBuilding && m_CharacterController.isGrounded)
        {
            StartCoroutine(m_BuildBob.DoBobCycle());
            PlayBuildingSound();
            m_BuildDir.y = 0f;
            m_Building = false;
        }
        // i think these two go in the main script and call all the inners of the second 

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

    private void PlayBuildingSound()  // and see the audio source is on the main player so.... prob in fps script
    {
        m_AudioSource.clip = m_BuildSound;
        m_AudioSource.Play();

        /*
         * TEMPERARLY REMOVE THIS
         * 
        //i chose to keep this as a step because building is going to mess up your next step, there will be offset
        m_NextStep = m_StepCycle + .5f;  // this may go in the fp controller when the function is called or i may just access the script from in here. 
        //def if its already accessed then imma use it there too

        */
    }


    /*
     *  THIS WHOLE THING CAN GO TOO
     *  it mostly handles the charachter position and finding out if there is a collision with your sphere
    private void FixedUpdate()
    {
        float speed;
        GetInput(out speed); // you have to access the first person controller
        Vector3 desiredMove = transform.forward * m_moveInput.y + transform.right * m_moveInput.x;
        //im pretty sure most of this stuff will go in the fp controller script

        RaycastHit hitInfo;
        Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
            m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);

        desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

        m_MoveDir.x = desiredMove.x * speed;
        m_MoveDir.y = desiredMove.z * speed;

        if (m_CharacterController.isBuilding)
        {
            m_MoveDir.y = -m_StickToBuildingForce;

            if (m_Build)
            {
                m_MpveDir.y = m_BuildSpeed;
                PlayBuildSound();
                m_Build = false;
                m_Building = false;
            }
        }
        else
        {
            m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
        }
        m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

        ProgressStepCycle(speed);
        UpdateCameraPosition(speed);
        
    }
    */

   
    /*
     * ALREADY HAVE A playbuildingsound
    private void PlayBuildSound()
    {
        m_AudioSource.clip = m_BuildSound;
        m_AudioSource.Play();
    }
    */



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
