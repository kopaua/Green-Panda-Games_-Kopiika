using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TopUI : MonoBehaviour
{

    [SerializeField]
    private Text goldTx;
    private Coroutine counter;

    private void Start()
    {
        PlayerManager.Instance.OnGold += SetGold;
    }

    private void OnDisable()
    {
        PlayerManager.Instance.OnGold -= SetGold;
    }  

    private void SetGold(int currentGold, int DestinationGold)
    {
        if (counter != null)
            StopCoroutine(counter);
        counter = StartCoroutine(GoDestination(currentGold, DestinationGold));       
    }

    private IEnumerator GoDestination(int currentGold, int DestinationGold)
    {
        while (currentGold < DestinationGold)
        {
            currentGold++;
            goldTx.text = currentGold.ToString();
            yield return new WaitForSeconds(0.02f);
        }       
    }
}
