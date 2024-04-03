using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BuildSystem;
using System;
using UnityEditor;
namespace UI
{
public class TowersSelecter : MonoBehaviour
{
    public void SelectElement(string str)
    {
        //enum.TryParse(str, out BuildSelect select);
        BuildSelect result;
        if (Enum.TryParse<BuildSelect>(str, true, out result))
        {
            if (BuildManager.Instance.Select == BuildSelect.Null)
                BuildManager.Instance.Select = result;
            else
                BuildManager.Instance.Select = BuildSelect.Null;
        }
        else
            Debug.LogError("Нет такого элемента");

    }
}
}