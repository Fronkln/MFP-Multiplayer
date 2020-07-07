using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{

    public static DiscordController inst;
    public Discord.Discord discord;


    void Awake()
    {
        if (inst != null)
            Destroy(gameObject);
        else
            inst = this;
    }
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        discord = new Discord.Discord(705783892551925780, (System.UInt64)Discord.CreateFlags.Default);
        var activityManager = discord.GetActivityManager();
        activityManager.RegisterSteam(557340);

        var activity = new Discord.Activity
        {
            State = "Still Testing",
            Details = "Bigger Test"
        };
        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.LogError("Everything is fine!");
            }
        });

    }

    void OnApplicationQuit()
    {
        discord.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        discord.RunCallbacks();

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Z))
            GameObject.FindObjectOfType<OptionsMenuScript>().buildDebugMenu();
    }
}
