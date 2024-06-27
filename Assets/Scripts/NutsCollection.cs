using System;
using System.Collections.Generic;
using UnityEngine;

public class NutsCollection : MonoBehaviour
{
    public List<Color> colors = new List<Color>();
    public Bolt bolt;
    public bool isNutOpened;

    [SerializeField]
    private List<Nut> nuts = new List<Nut>();

    // Start is called before the first frame update
    void Start()
    {
        AddNutsIntoStack();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void AddNutsIntoStack()
    {
        foreach (var nut in nuts)
        {
            bolt.nuts.Push(nut);
        }
    }

    public bool IsNutOpened()
    {
        return isNutOpened;
    }

    public void SetNutOpened(bool status)
    {
        isNutOpened = status;
    }
}
