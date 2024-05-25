using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static List<Transform> GetChildren(this Transform someTransform)
    {
        var children = new List<Transform>();
        for (int i = 0; i < someTransform.childCount; i++)
        {
            children.Add(someTransform.GetChild(i));
        }
        return children;
    }

    public static void DestroyChildren(this Transform someTransform)
    {
        var children = GetChildren(someTransform);
        foreach (Transform child in children)
        {
            Object.DestroyImmediate(child.gameObject);
        }
    }

    public static void ReparentChildrenTo(this Transform oldTransform, Transform newTransform)
    {
        var children = GetChildren(oldTransform);
        foreach (Transform child in children)
        {
            child.parent = newTransform;
        }
    }
}