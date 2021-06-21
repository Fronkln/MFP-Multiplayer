using UnityEngine;


public static class BetaBoneToRetailBone
{
    public static void Fixup(SkinnedMeshRenderer betaRenderer, SkinnedMeshRenderer retailRenderer)
    {
        Transform[] replacementBones = new Transform[25];

        for (int i = 0; i < 23; i++)
            replacementBones[i] = retailRenderer.bones[i];

        replacementBones[23] = replacementBones[0];
        replacementBones[24] = replacementBones[0];

        betaRenderer.bones = replacementBones;
        betaRenderer.rootBone = retailRenderer.rootBone;
        betaRenderer.probeAnchor = retailRenderer.probeAnchor;

        betaRenderer.updateWhenOffscreen = true;
    }
}

