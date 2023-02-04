using UnityEngine;

public static class Utils
{
    public static Vector3 GetRandomPointInPlane(Renderer meshPlane)
    {
        Bounds groundLocalBounds = meshPlane.localBounds;
        Vector3 randomPoint = new Vector3(groundLocalBounds.center.x + Random.Range(-1f, 1f) * groundLocalBounds.extents.x,
                groundLocalBounds.center.y,
                groundLocalBounds.center.x + Random.Range(-1f, 1f) * groundLocalBounds.extents.z);

        return meshPlane.transform.TransformPoint(randomPoint);
    }
}
