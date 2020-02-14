using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Cultist
{
    public int id;
    public int building;
    public readonly string name;
    public int age;
    public float faith;

    public Cultist(int index,int assignedBuilding,string cultistName,int cultistAge,float cultistFaith)
    {
        id = index;
        building = assignedBuilding;
        name = cultistName;
        age = cultistAge;
        faith = cultistFaith;
    }

    public void RandomCultist()
    {
        /*MAKE RANDOM SHIT*/
    }
}
