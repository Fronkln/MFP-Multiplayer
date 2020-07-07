using UnityEngine;

public class SpawnDoorScriptNetworkController : BaseNetworkEntity
{
    private SpawnDoorScript spawnDoor;
    private float sendPacketDelay = 0;

    public override void Awake()
    {
        base.Awake();
        spawnDoor = GetComponent<SpawnDoorScript>();
    }

    public override void Update()
    {
        base.Update();

        if (sendPacketDelay > 0)
            sendPacketDelay -= Time.deltaTime;
        else
            sendPacketDelay = 0;
    }

    public override void OnHostActivatedEntity() // triggered when the game wants to start spawning spawndoorscript enemies
    {
        base.OnHostActivatedEntity();
        spawnDoor.isTriggered = true;
    }

    public void RequestSpawnDoor()
    {
        if (sendPacketDelay > 0 || spawnDoor.isTriggered)
            return;

        MFPEditorUtils.Log("requesting to activate door");

        PacketSender.RequestBaseNetworkedEntityActivation(entityIdentifier);
        sendPacketDelay = 0.2f;
         
    }
}

