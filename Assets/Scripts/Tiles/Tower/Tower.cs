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
        
        [SerializeField] private int maxSize = 5;

        public int Size => elements.Count;

        private BoxCollider _collider;

        void Awake()
        {
            _collider = GetComponent<BoxCollider>();
        }
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
            //if (Base == null)
            //{
            //    Debug.LogError("Base is null.");
            //    return;
            //}
            //transform.position = position;
            //Base = Instantiate(Base, transform);
            //Base.transform.position = transform.position;
            //elements.Add(Base);
        }
        public void AddElement(Element prefab)
        {
            if (maxSize <= elements.Count) return;
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
            // Increase collider
            _collider.size = new Vector3(_collider.size.x, _collider.size.y + element.nextElementPivot.y, _collider.size.z);
            _collider.center = new Vector3(_collider.center.x, _collider.size.y / 2, _collider.center.z);

            element.Build();
        }
        /*public void RemoveElement(Element prefab)
        {
            elements.Remove(element);
            health -= element.Health;
        }*/
    }
}