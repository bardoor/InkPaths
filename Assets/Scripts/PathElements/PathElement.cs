using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathElement : MonoBehaviour
{
    protected StateMachine _stateMachine = new StateMachine();
    protected Color _inkColor { get; set; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public virtual void UpdateElementsStatesAround()
    {

    }
}
