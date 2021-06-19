using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Steamworks;

/*
 * 
 * DEFINITION OF A BASE NETWORK ENTITY: A SINGLEPLAYER ENTITY WHICH ALREADY EXISTED IN THE MAP OR WILL SPAWN
 * CONVERTED TO ITS NETWORKED STATE FOR THE MOST PART, HANDLED IN CONVERTOBJECTSTONETWORK()
 * 
 * BASENETWORKENTITIES ARE RECCOMENDED TO BE INITIALIZED ON AWAKE()
 * 
 * 
 * ALSO, MULTIPLAYERMANAGERTEST CONVERTING SINGLEPLAYER OBJECTS TO NETWORK MANUALLY WILL BREAK THINGS
 * 
 * 
 * THERE IS NO SUCH THING AS BASENETWORKENTITIES NOT SYNCHRONIZING LITERALLY ALL THE TIME.
 * 
 * ALSO, MAYBE YOU COULD ADD A NEW THING CALLED FREEIDS (that are recycled from destroyed BaseNetworkEntity) AND BEFORE WE ASSIGN A NEW ID BASED ON DICTIONARY LENGTH THESE COULD BE REUSED
 */ 

public class BaseNetworkEntity : MonoBehaviour
{
    public int entityIdentifier = -1;
    public MultiplayerManagerTest mpManager;

    public object[] packageVars; //USED TO SHAVE OFF A EXPENSIVE PER-FRAME GETCOMPONENT IN RPCFUNCTIONS

    public bool onlyHostWillSync = false;

    public bool dontDoDebug = false;

    public bool registerSelfOnSpawn = false;

    public bool requireMpManager = false;

    public int maxAllowedPackets = 5;
    public int curPackets = 0;

    public bool playerIsInteracting = false;
    public CSteamID interactingPlayer;
    public bool doSync = false;

    public bool trackLastInteractionTime = false;
    public float lastTimeSincePlayerInteract = 0;
    public float maxLastInteractTime = 3;


    private float packetResetTime = 0.2f;
    private float packetTime = 0;

    public bool ignoreMaxPacketsDoOnce = true;

    public DebugBaseNetworkedEntity debugHelper;

    //Definition of a state: a method that doesnt take actions (for example, flipping tables and
    //platform disappearing could make great "states")

    public List<Action> entityStates = new List<Action>();


 /*   public IEnumerator ResetPackets()
    {
        yield return new WaitForSeconds(0.2f);
        if (!MultiplayerManagerTest.connected)
        {
            StartCoroutine(ResetPackets());
            yield return null;
        }
        curPackets = 0;
        StartCoroutine(ResetPackets());

        yield return null;
    }
    */

    public BaseNetworkEntity()
    {
        if (GetComponent<SaveStateControllerScript>())
            DestroyImmediate(GetComponent<SaveStateControllerScript>());

        if (GetComponent<SaveStateSimpleControllerScript>())
            DestroyImmediate(GetComponent<SaveStateSimpleControllerScript>());
    }

    public void ExecuteState(byte state)
    {

        if (state < entityStates.Count - 1)
        {
            MFPEditorUtils.Log("State " + state + " does not exist on " + transform.name);
            return;
        }

        entityStates[state].Invoke();

        MFPEditorUtils.Log("Trying to run " + state.ToString());
    }

    public virtual void Awake()
    {
        if(MultiplayerManagerTest.inst != null)
        mpManager = MultiplayerManagerTest.inst;
        /*
        if (!mpManager.networkedBaseEntities.Contains(this))
        {
            mpManager.networkedBaseEntities.Add(this);
            entityIdentifier = Convert.ToInt16(mpManager.networkedBaseEntities.Count - 1);
        }
        */

        // StartCoroutine(ResetPackets());

        // if(!registerSelfOnSpawn)
        if (!MultiplayerManagerTest.inst.initComplete)
            MultiplayerManagerTest.entitiesToRegister.Add(this);
        else
        {
            MultiplayerManagerTest.entitiesToRegisterRUNTIME.Add(this);
            MFPEditorUtils.Log("Runtime Entity " + transform.name);
        }
    }

    [Obsolete]
    public void SelfRegister()
    {
        return;
        if (!mpManager.networkedBaseEntities.ContainsValue(this))
        {
          /*  entityIdentifier = mpManager.networkedBaseEntities.Count - 1;
            mpManager.networkedBaseEntities.Add(entityIdentifier, this);

            MFPEditorUtils.Log("Registered runtime BaseNetworkEntity: " + transform.name + " " + entityIdentifier.ToString());
            */
        }
    }

    public virtual void Start()
    {
#if DEBUG
        if (MultiplayerManagerTest.extraDebug)
            if(dontDoDebug)
            debugHelper = gameObject.AddComponent<DebugBaseNetworkedEntity>();
#endif
    }

    public virtual void Update()
    {

        if (requireMpManager)
        {
            if (mpManager == null)
                if (MultiplayerManagerTest.inst != null)
                    mpManager = MultiplayerManagerTest.inst;
        }

        if (!interactingPlayer.isNull())
            if (MultiplayerManagerTest.inst.playerObjects[interactingPlayer] == null)
                PacketSender.BaseNetworkedEntityRPC("OnPlayerStopInteract", entityIdentifier);

        if (curPackets != 0)
        {
            if (packetTime < packetResetTime)
                packetTime += Time.unscaledDeltaTime;
            else
            {
                packetTime = 0;
                curPackets = 0;
            }
        }
    }



    public virtual void OnDestroy()
    {
        if (mpManager != null && mpManager.networkedBaseEntities != null && mpManager.networkedBaseEntities.ContainsValue(this))
            mpManager.networkedBaseEntities.Remove(entityIdentifier);
    }

    public virtual void OnPlayerStartInteract(ulong activator)
    {
        playerIsInteracting = true;
        lastTimeSincePlayerInteract = 0;

        interactingPlayer = (CSteamID)activator;
    }


    public virtual void TestRPC(ulong activator)
    {
        CSteamID activatorID = (CSteamID)activator;

        if (activatorID.isLocalUser())
            MFPEditorUtils.Log("Sent and recieved TestRPC");
        else
            MFPEditorUtils.Log("Recieved TestRPC packet from " + SteamFriends.GetFriendPersonaName(activatorID));
    }

    /// <summary>
    /// Executes for all clients when host handles packet event 252
    /// </summary>
    public virtual void OnHostActivatedEntity() 
    {
        MFPEditorUtils.Log("Entity got activated: " + transform.name); 
    }

    public virtual void OnPlayerStopInteract()
    {
        playerIsInteracting = false;
        interactingPlayer = (CSteamID)0;
    }

    /// <summary>
    /// Capping the amount of packets sent so we don't send way too much data because of high FPS
    /// </summary>
    public bool PacketsExceeded()
    {
        if (MultiplayerManagerTest.singleplayerMode)
            return false;

        if(ignoreMaxPacketsDoOnce)
        {
            ignoreMaxPacketsDoOnce = false;
            return false;
        }

        else return curPackets > maxAllowedPackets || maxAllowedPackets <= 0;
    }

    public void HandleCollisionEnter(Collider collision)
    {
        if (collision.transform.root == PlayerScript.PlayerInstance.transform)
            MFPEditorUtils.Log("Handled collision event by singleplayer player");
    }
}

