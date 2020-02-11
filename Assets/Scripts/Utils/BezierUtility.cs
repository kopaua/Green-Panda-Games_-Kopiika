using UnityEngine;

public static class BezierUtility
{
    /// <summary>
    /// Calculating path by Bezier, should include path with 4 points
    /// </summary>  
    public static Vector3 GetBezierPos(Transform[] positions, float pathPart)
    {
        return Mathf.Pow(1 - pathPart, 3) * positions[0].position +
                 3 * Mathf.Pow(1 - pathPart, 2) * pathPart * positions[1].position +
                 3 * (1 - pathPart) * Mathf.Pow(pathPart, 2) * positions[2].position +
                 Mathf.Pow(pathPart, 3) * positions[3].position;
    }
}
