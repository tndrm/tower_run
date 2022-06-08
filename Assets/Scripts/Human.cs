using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    [SerializeField] private Transform _fixationPoint;

    public Transform fixationPoint => _fixationPoint; //это внешне можно читать, но перезаписать нельзя.
                                                      //TODO почитаать про лямбла выражения в сишарпе (?) 

}
