using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveOnPlay : MonoBehaviour
{
    [SerializeField] GameObject @object;
    // Start is called before the first frame update
    void Start()
    {
        this.@object.SetActive(true);
    }
}
