using UnityEngine;
using System;
using System.Collections.Generic;


//MPEntityFactory should be put at Assembly.
//Else registering and spawning templates will freeze the game
public static class MPEntityFactory
{
    internal static Dictionary<string, Type> registeredEnts = new Dictionary<string, Type>();
    internal static Dictionary<Type, GameObject> registeredEntTemplates = new Dictionary<Type, GameObject>();

    public static void RegisterEntity(Type type, string entName)
    {
        if (!type.IsSubclassOf(typeof(MonoBehaviour)))
            MFPEditorUtils.LogError(entName + ": can't register an entity which is not a monobehaviour " + type.ToString() + " " + type.BaseType.ToString());

        registeredEnts.Add(entName, type);
    }

    public static void RegisterEntityTemplate<T>(string entityName, GameObject instance) => RegisterEntityTemplate(typeof(T), entityName, instance);
    public static void RegisterEntityTemplate(Type type, string entityName, GameObject instance)
    {
        if (registeredEntTemplates.ContainsKey(GetEntityType(entityName)))
            return;

        //#if DEBUG
        if (!registeredEnts.ContainsKey(entityName))
            MFPEditorUtils.LogError(entityName + " cant register template for unregistered entity");
        else
        {
            GameObject template = GameObject.Instantiate(instance);

            MonoBehaviour obj = (MonoBehaviour)instance.GetComponent(type);

            registeredEntTemplates.Add(type, template);
            GameObject.DontDestroyOnLoad(template);
            template.SetActive(false);


            MFPEditorUtils.Log("Registered template for " + entityName);


        }
        //#endif
    }

    public static Type GetEntityType(string entityName)
    {
        return registeredEnts[entityName];
    }

    public static NetEnt GetEntityTemplate(string entityName, bool dontActivate = false)
    {
        Type entType = GetEntityType(entityName);
        GameObject obj = GameObject.Instantiate(registeredEntTemplates[entType]);
        NetEnt mono = (NetEnt)obj.GetComponent(entType);

        if (!dontActivate)
            mono.gameObject.SetActive(true);

        return mono;
    }


    public static T GetEntityTemplate<T>(string entityName, bool dontActivate = false)
    {
        return (T)(object)GetEntityTemplate(entityName, dontActivate);
    }

}

