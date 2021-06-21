using UnityEngine;


public class NetEnt : MonoBehaviour
{
    private BaseNetworkEntity helper;
    internal uint Edict = 999999;
    [System.NonSerialized] public string entName = "";

    public const uint INVALID_EDICT = 999999999;

    /// <summary>
    /// Created by server
    /// </summary>
    public virtual void OnCreated()
    {
        helper = gameObject.AddComponent<BaseNetworkEntity>();
    }

    public uint GetEdict()
    {
        return Edict;
    }

    public void SetEdict(uint edict)
    {
        Edict = edict;
    }
}

