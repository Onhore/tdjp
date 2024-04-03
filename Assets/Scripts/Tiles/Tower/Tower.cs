using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
namespace BuildSystem
{
    public class Tower : Tile
    {
        [SerializeField] private List<Element> elements;
        [SerializeField] private Element Base;
        [SerializeField] private Element Top;
        
        [SerializeField] private int size = 5;
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            foreach (Element element in elements)
            {
                element.Interact();
                //element.transform.position = transform.position;
                
            }
        }
        public override void Build(Vector3 position)
        {
            if (Base == null)
            {
                Debug.LogError("Base is null.");
                return;
            }
            transform.position = position;
            Base = Instantiate(Base, transform);
            Base.transform.position = transform.position;
            elements.Add(Base);
        }
        public virtual void Build(Vector3 position, Base b)
        {
            transform.position = position;
            Base = Instantiate(b, transform);
            Base.transform.position = transform.position;
            //AddElement(b);
        }
        public void AddElement(Element prefab)
        {
            if (size <= elements.Count) return;
            Vector3 position = Top == null ? Vector3.zero : Top.transform.localPosition + Top.nextElementPivot;
            //Debug.Log("Добавить элемент " + prefab + " " + position);
            Element element = Instantiate(prefab, transform);
            element.transform.localPosition = position;
            //Element element = gObj.GetComponent<Element>();
            elements.Add(element);
            if (Base == null)
                Base = element;
            Top = element;
            health += element.Health;
        }
        /*public void RemoveElement(Element prefab)
        {
            elements.Remove(element);
            health -= element.Health;
        }*/
    }
}