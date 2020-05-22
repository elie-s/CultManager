using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GatherChildren
{
    public static List<GameObject> GatherAllChildrenInList(this GameObject _parent)
    {
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < _parent.transform.childCount; i++)
        {
            GameObject current = (_parent.transform.GetChild(i).gameObject);
            result.Add(current);

        }
        return result;
    }

    public static List<GameObject> GatherActiveChildrenInList(this GameObject _parent)
    {
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < _parent.transform.childCount; i++)
        {
            GameObject current = (_parent.transform.GetChild(i).gameObject);
            if (current.activeSelf)
            {
                result.Add(current);
            }

        }
        return result;
    }

    public static List<T> GatherBehaviorInList<T>(this GameObject _parent)
    {
        List<T> result = new List<T>();
        for (int i = 0; i < _parent.transform.childCount; i++)
        {
            GameObject current = (_parent.transform.GetChild(i).gameObject);
            if (current.GetComponent<T>()!=null)
            {
                result.Add(current.GetComponent<T>());
            }

        }
        return result;
    }

    public static GameObject[] GatherAllChildrenInArray(this GameObject _parent)
    {
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < _parent.transform.childCount; i++)
        {
            GameObject current = (_parent.transform.GetChild(i).gameObject);
            result.Add(current);
        }
        return result.ToArray();
    }

    public static GameObject[] GatherActiveChildrenInArray(this GameObject _parent)
    {
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < _parent.transform.childCount; i++)
        {
            GameObject current = (_parent.transform.GetChild(i).gameObject);
            if (current.activeSelf)
            {
                result.Add(current);
            }

        }
        return result.ToArray();
    }

    public static T[] GatherBehaviorInArray<T>(this GameObject _parent)
    {
        List<T> result = new List<T>();
        for (int i = 0; i < _parent.transform.childCount; i++)
        {
            GameObject current = (_parent.transform.GetChild(i).gameObject);
            if (current.GetComponent<T>() != null)
            {
                result.Add(current.GetComponent<T>());
            }

        }
        return result.ToArray();
    }


}
