using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public static class RaycastHit2Extension
{
    /// <summary>
    /// This function return Center of ColliderBox2D
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    public static Vector2 GetCenterHitPoint(this RaycastHit2D hit)
    {
        return hit.collider.bounds.center;
    }
    public static void DrawDebugCircleAtCenterPoint(
        this RaycastHit2D hit,
        float radius,
        Color color,
        int segments = 32)
    {
        if (hit.collider == null) return;

        Vector2 center = hit.collider.bounds.center;

        float angleStep = 360f / segments;
        Vector3 prevPoint = center + new Vector2(Mathf.Cos(0), Mathf.Sin(0)) * radius;

        for (int i = 1; i <= segments; i++)
        {
            float rad = (angleStep * i) * Mathf.Deg2Rad;
            Vector3 newPoint = center + new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;

            Debug.DrawLine(prevPoint, newPoint, color, 0f); // 0f = chỉ hiện trong 1 frame
            prevPoint = newPoint;
        }
    }

    public static InteractRegion GetInteractRegion(this RaycastHit2D hit)
    {
        var interacRegions = hit.collider.GetComponentsInParent<InteractRegion>();
        if (interacRegions != null)
        {
            foreach (var cmp in interacRegions)
            {
                if (cmp.Colider == hit.collider)
                {
                   return cmp;
                }
            }

        }
        return null;
    }

}

