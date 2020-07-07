using HarmonyLib;
using System;
using UnityEngine;


public static class HarmonyPatches
{
    private static bool initialized = false;
    private static Harmony _harmony;


    public static void InitPatches()
    {
        if (initialized)
            return;

        _harmony = new Harmony("Jhrino.MFPClassic");
        _harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());


        initialized = true;
    }
}

