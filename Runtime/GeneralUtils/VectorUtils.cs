using UnityEngine;

public static class VectorUtils
{
    public static bool Approximately(Vector3 v, Vector3 other, float tolerance = 1e-4f)
    {
        return Vector3.SqrMagnitude(v - other) <= tolerance * tolerance;
    }

    public static bool Approximately(Vector2 v, Vector2 other, float tolerance = 1e-4f)
    {
        return Vector2.SqrMagnitude(v - other) <= tolerance * tolerance;
    }

    public static Vector3 ToFlatVector3(this Vector2 v)
    {
        return new Vector3(v.x, 0, v.y);
    }

    public static Vector3 Flatten(this Vector3 v)
    {
        return new Vector3(v.x, 0, v.z);
    }

    public static float FlatDistance(Vector3 origin, Vector3 destination)
    {
        return Vector3.Distance(origin.Flatten(), destination.Flatten());
    }

    public static float CrossProduct2D(Vector2 a, Vector2 b)
    {
        return a.x * b.y - b.x * a.y;
    }

    /// <summary>
    /// Determine whether 2 lines intersect, and give the intersection point if so.
    /// </summary>
    /// <param name="p1start">Start point of the first line</param>
    /// <param name="p1end">End point of the first line</param>
    /// <param name="p2start">Start point of the second line</param>
    /// <param name="p2end">End point of the second line</param>
    /// <param name="intersection">If there is an intersection, this will be populated with the point</param>
    /// <returns>True if the lines intersect, false otherwise.</returns>
    public static bool IntersectLineSegments2D(Vector2 p1start, Vector2 p1end, Vector2 p2start, Vector2 p2end, out Vector2 intersection)
    {
        // Consider:
        //   p1start = p
        //   p1end = p + r
        //   p2start = q
        //   p2end = q + s
        // We want to find the intersection point where :
        //  p + t*r == q + u*s
        // So we need to solve for t and u
        var p = p1start;
        var r = p1end - p1start;
        var q = p2start;
        var s = p2end - p2start;
        var qminusp = q - p;

        float cross_rs = CrossProduct2D(r, s);

        if (MathUtils.Approximately(cross_rs, 0f))
        {
            // Parallel lines
            if (MathUtils.Approximately(CrossProduct2D(qminusp, r), 0f))
            {
                // Co-linear lines, could overlap
                float rdotr = Vector2.Dot(r, r);
                float sdotr = Vector2.Dot(s, r);
                // this means lines are co-linear
                // they may or may not be overlapping
                float t0 = Vector2.Dot(qminusp, r / rdotr);
                float t1 = t0 + sdotr / rdotr;
                if (sdotr < 0)
                {
                    // lines were facing in different directions so t1 > t0, swap to simplify check
                    MathUtils.Swap(ref t0, ref t1);
                }

                if (t0 <= 1 && t1 >= 0)
                {
                    // Nice half-way point intersection
                    float t = Mathf.Lerp(Mathf.Max(0, t0), Mathf.Min(1, t1), 0.5f);
                    intersection = p + t * r;
                    return true;
                }
                else
                {
                    // Co-linear but disjoint
                    intersection = Vector2.zero;
                    return false;
                }
            }
            else
            {
                // Just parallel in different places, cannot intersect
                intersection = Vector2.zero;
                return false;
            }
        }
        else
        {
            // Not parallel, calculate t and u
            float t = CrossProduct2D(qminusp, s) / cross_rs;
            float u = CrossProduct2D(qminusp, r) / cross_rs;
            if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
            {
                intersection = p + t * r;
                return true;
            }
            else
            {
                // Lines only cross outside segment range
                intersection = Vector2.zero;
                return false;
            }
        }
    }
}