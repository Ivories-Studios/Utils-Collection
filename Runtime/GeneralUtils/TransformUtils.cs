using UnityEngine;

public static class TransformUtils
{
    public static void DestroyChildren(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }
    }

    public static void LookAtY(this Transform transform, Vector3 point)
    {
        Vector3 lookPos = point - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
