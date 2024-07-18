using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> unitlist = new List<GameObject>();
    public List<GameObject> unitSelected = new List<GameObject>();
    public Material selectionMaterial;
    private Material originalMaterial;
    

    private static UnitSelections _instance;
    public static UnitSelections Instance { get { return _instance; } }

    private void Awake()
    {
        // if an instance of this already exists and it isn't this one
        if (_instance != null && _instance != this)
        {
            // destroy this instance
            Destroy(this.gameObject);
        }
        else
        {
            // make this the instance
            _instance = this;
        }
    }


    public void ClickSelect(GameObject unitToAdd)
    {
       // DeselectAll();
        if (!unitSelected.Contains(unitToAdd))
        {  
            unitSelected.Add(unitToAdd);
        }
        else
        {
            unitSelected.Remove(unitToAdd);
        }
    }

    /*
    public void ShiftClickSelect(GameObject untiToAdd)
    {
        if (!unitSelected.Contains(untiToAdd))
        {
            unitSelected.Add(untiToAdd);
        }
        else
        {
            unitSelected.Remove(untiToAdd);
        }
    }
    public void DragSelect(GameObject untiToAdd)
    {

    }
    */

    public void DeselectAll()
    {
        unitSelected.Clear();
    }

    public void Deselect (GameObject unitToSelect)
    {
        
    }
}
