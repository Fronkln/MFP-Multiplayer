using UnityEngine;
using System;



#if EDICT
[Serializable]
public class EdictEntityCreationInfo
{
    /// <summary>
    /// The edict num will never exist on all clients
    /// Completely handled by host.
    /// </summary>
    public uint edictNum;
    public string entityName;

    public Vector3 position;
    public Vector3 rotation;

    public string serializedData = "";

    //serialized info here later

    public void Create()
    {

        MFPEditorUtils.Log("Attempt create edict obj");
        MFPEditorUtils.Log(edictNum);
        MFPEditorUtils.Log(entityName);

        if (MultiplayerManagerTest.inst.edictEnts.ContainsKey(edictNum))
            return;


        JsonUtility.FromJsonOverwrite(serializedData, MPEntityFactory.GetEntityTemplate(entityName));
        MFPEditorUtils.Log("Created edict!");

      // MPEntityFactory.GetEntityTemplate<
    }
}
#endif

