using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CollectableObject : MonoBehaviour
{
    public static int coins { get; private set; }

    void OnEnable()
    {
        ++coins;
    }

    void OnDisable()
    {
        --coins;
    }
}


