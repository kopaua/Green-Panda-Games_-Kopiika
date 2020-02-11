using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{    
    private float bezierSpeedModifier = 7;
    private Transform[] points;   
    private bool isMoving;
  

    // Start is called before the first frame update
    void Start()
    {
        points = BezierRoute.Instance.GetPath(transform.position);      
        isMoving = true;     
    }

    // Update is called once per frame
    void Update()
    {      
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[0].position, Time.deltaTime * PlayerManager.Instance.SpeedTable);
            if (Vector3.Distance(transform.position, points[0].position) < 0.01f)
            {
                isMoving = false;
                StartCoroutine(GoByBezier());
            }
        }
    }

    private IEnumerator GoByBezier()
    {
        float tParam = 0;
        Vector3 startRotate = transform.eulerAngles;
        while (tParam < 1)
        {
            tParam += Time.deltaTime * (PlayerManager.Instance.SpeedTable / bezierSpeedModifier);
            tParam = Mathf.Clamp(tParam, 0, 1);
            transform.position = BezierUtility.GetBezierPos(points, tParam); 
            transform.eulerAngles = new Vector3(0, 0, startRotate.z + (180 * tParam));
            yield return new WaitForEndOfFrame();
        }
        points = BezierRoute.Instance.GetPath(transform.position);   
        isMoving = true;
    }  
}
