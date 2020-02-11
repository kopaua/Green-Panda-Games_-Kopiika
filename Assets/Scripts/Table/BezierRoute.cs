using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierRoute : Singelton<BezierRoute>
{
    [SerializeField]
    private Transform[] pointsTop, pointsBottom;
    private Vector2 gizmosPos;

    private void OnDrawGizmos()
    {
        if (pointsTop.Length == 0 || pointsBottom.Length == 0)
            return;

        for (float t =0; t<=1;t+=0.1f)
        {
            Gizmos.DrawSphere(BezierUtility.GetBezierPos(pointsTop, t), 0.25f);
            Gizmos.DrawSphere(BezierUtility.GetBezierPos(pointsBottom, t), 0.25f);
        }       
    }
    protected override void InitManager()
    {
    }

    public Transform[] GetPath(Vector3 posObject)
    {
        if(posObject == pointsTop[pointsTop.Length -1].position)
        {
            return pointsBottom;
        }
        else if (posObject == pointsBottom[pointsBottom.Length -1].position)
        {
            return pointsTop;
        }
        else
        {
            if (posObject.x > 0)
                return pointsTop;
            else
                return pointsBottom;
        }
      
    }

    
}
