using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eCustomerState
{
    idle,
    eating
}

public abstract class ACustomer : MonoBehaviour
{
    [SerializeField]
    private Transform foodPoint;
    private eCustomerState currentState;
    private Animator myAnimator;
    private APlate currentPlate;
    private float speedEating =2;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        SwitchToState(eCustomerState.idle);        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (currentState == eCustomerState.idle)
        {
            currentPlate = col.GetComponent<APlate>();
            if (currentPlate != null)
            {
                SwitchToState(eCustomerState.eating);
                currentPlate.StartEating(foodPoint.position, transform.position);
                currentPlate.OnFinish += OnStopEat;
            }
        }
    }
    private void OnStopEat()
    {
        currentPlate.OnFinish -= OnStopEat;
        SwitchToState(eCustomerState.idle);
    }

    private void SwitchToState(eCustomerState _state)
    {
        currentState = _state;
        switch (currentState)
        {
            case eCustomerState.eating:
                myAnimator.Play("PickUpFood");

                break;
            case eCustomerState.idle:
                myAnimator.Play("Idle");
                break;
        }
    }
}
