using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Tile
{
    [SerializeField] private List<Element> elements;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Element element in elements) 
        {
            element.Interact();
        }
    }
    private void CreateTower()
    { 
    
    }
    public void AddElement(Element element)
    { 
    
    }
    public void RemoveElement(Element element)
    {

    }
}
