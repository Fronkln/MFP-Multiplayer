using System;
using UnityEngine;

public static class CustomizationAssets
{
    public static Material mfpClassicHead;
    public static Material mfpClassicTorso;
    public static Texture2D mfpClassicLegs;


    public static void Load()
    {
        mfpClassicHead = DiscordCT.multiplayerBundle.LoadAsset("character_head_01_TEST 1") as Material;
        mfpClassicLegs = DiscordCT.multiplayerBundle.LoadAsset("legs01_2") as Texture2D;
        mfpClassicTorso = DiscordCT.multiplayerBundle.LoadAsset("torsor_long_coat_and_hoodie_mfpclassic") as Material;

    }
}

