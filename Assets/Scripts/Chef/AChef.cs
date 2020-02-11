using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChefState
{
    Lock,
    Unlock
}

public abstract class AChef : MonoBehaviour, IClickable
{
    public int LevelToUnlock;
    [SerializeField]
    private FoodInfo foodInfo;
    [SerializeField]
    private GameObject PlatePrefab;
    [SerializeField]
    private GameObject tableObj, characterObj, plateStartPos;
    private float timerPlate;
    private const float clickPlate = 1, autoPlate = 10;
    private ChefState currentState;
    private BoxCollider2D myClickCollider;
    private Food currentFood;

    // Start is called before the first frame update
    void Start()
    {
        myClickCollider = GetComponent<BoxCollider2D>();
        if (PlayerManager.Instance.Level >= LevelToUnlock)
        {
            UnlockChef();
        }
        else
        {          
            characterObj.SetActive(false);
            currentState = ChefState.Lock;
            myClickCollider.size = new Vector2(4.8f,2.5f);
            myClickCollider.offset = new Vector2(0, 0.85f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == ChefState.Unlock)
        {
            timerPlate += Time.deltaTime;
            if (timerPlate >= autoPlate)
            {
                CratePlate();
            }
        }
    }

    public void OnClickEvent()
    {
        if (currentState == ChefState.Unlock)
        {
            if (timerPlate >= clickPlate)
            {
                CratePlate();
            }
        }
        else
        {
            Debug.Log("Lock need level:" + LevelToUnlock);
        }
    }

    private void UnlockChef()
    {
        myClickCollider.size = new Vector2(1.6f,1.5f);
        myClickCollider.offset = new Vector2(0, 0.18f);
        currentState = ChefState.Unlock;
        characterObj.SetActive(true);
    }

    private void CratePlate()
    {
        CreateRandomFood(ref currentFood);
        timerPlate = 0;
        GameObject _clonePlate = Instantiate(PlatePrefab);
        _clonePlate.GetComponent<APlate>().InitPlate(currentFood);
        _clonePlate.transform.position = plateStartPos.transform.position;
    }

    /// <summary>
    /// Just for EXAMPLE !!!
    /// </summary>
    private void CreateRandomFood(ref Food _currentFood)
    {
        int random = Random.Range(0, foodInfo.Food.Length);
        _currentFood = foodInfo.Food[random];
    }
}
