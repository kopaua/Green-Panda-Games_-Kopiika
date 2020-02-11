using System;
using System.Collections;
using UnityEngine;

public abstract class APlate : MonoBehaviour
{
    public Action OnFinish;

    [SerializeField]
    private Transform[] FoodPosition;
    private Food currentFood;
    private const float speedMoveFood = 3;   

    public void InitPlate(Food _food)
    {
        gameObject.AddComponent<Rotating>();
        currentFood = _food;
        for (int i = 0; i < currentFood.Parts; i++)
        {
            if (i >= FoodPosition.Length)
                break;

            GameObject cloneFood = new GameObject();
            SpriteRenderer spriteRenderer = cloneFood.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = Resources.Load<Sprite>("Food/" + currentFood.FoodName.ToString());
            spriteRenderer.sortingOrder = 8 -i;
            cloneFood.transform.SetParent(FoodPosition[i]);
            cloneFood.transform.localPosition = Vector3.zero;
            cloneFood.transform.localEulerAngles = Vector3.zero;
        }
    }   

    public void StartEating(Vector3 destinationPoint, Vector3 foodPoint)
    {       
        Destroy(GetComponent<Rotating>());
        StartCoroutine(GoDestination(destinationPoint, foodPoint));
    }

    private void DisableFoodPart(int index)
    {
        PlayerManager.Instance.AddGold(currentFood.Cost);
        FoodPosition[index].gameObject.SetActive(false);     
    }

    private IEnumerator GoDestination(Vector3 destinationPoint, Vector3 foodPoint)
    {      
        while (transform.position != destinationPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPoint, Time.deltaTime * speedMoveFood);
            yield return new WaitForEndOfFrame();
        }

        for (int i = 0; i < currentFood.Parts; i++)
        {
            while (FoodPosition[i].position != foodPoint)
            {
                FoodPosition[i].position = Vector3.MoveTowards(FoodPosition[i].position, foodPoint, Time.deltaTime * speedMoveFood);
                yield return new WaitForEndOfFrame();
            }
            DisableFoodPart(i);
            yield return new WaitForSeconds(0.25f);
        }
        yield return new WaitForSeconds(0.2f);
        OnFinish();
        Destroy(gameObject);
    }

}
