using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]

public class DropItem
{
    public GameObject itemToDrop;
    public float dropWeight;
}

public class DropManager : MonoBehaviour
{ 
    public List<DropItem> dropTable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject DropItem()
    {
        // Create our CDF Array
        List<float> CDFArray = new List<float>();

        // For each element in our drop table, fill in the cumulative density in the CDF aray
        float runningTotal = 0;
        foreach (DropItem item in dropTable)
        {
            // Update the running total by adding the newest dropweight 
            runningTotal = runningTotal + item.dropWeight;
            // Add it to the CDF array
            CDFArray.Add(runningTotal);
        }

        //for (int index = 0; index < dropTable.Count; index++)
        //{
        //    if(index == 0) 
        //    {
        //        CDFArray.Add(dropTable[index].dropWeight); 
        //    }
        //    else 
        //    {
        //        CDFArray.Add(dropTable[index - 1].dropWeight + dropTable[index].dropWeight);
        //    }
        //}

        // Choose random number < our total density
        float randomNumber = UnityEngine.Random.Range(0, runningTotal);

        // Go through CDF array, one at a time
        for (int i = 0; i < CDFArray.Count; i++)
        {
            // If random number is < density at the point
            if (randomNumber < CDFArray[i])
            {
                // Return the Item at the same point
                return dropTable[i].itemToDrop;
            }

        }

        // If impossible return null
        Debug.LogError("ERROR: Random number exceeded CDFArray values");
        return null;
    }
}
