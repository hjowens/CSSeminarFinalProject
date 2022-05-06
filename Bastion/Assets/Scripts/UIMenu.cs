using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenu : MonoBehaviour
{
    public bool isActive = true;
    public void toggleHidden()
    {
        isActive = !isActive;
        //List<Transform> Panels = new List<Transform>();
        foreach(Transform Panel in transform)
        {
            string Tag = Panel.gameObject.tag;
            if (Tag == "Panel")
            {
                //Panels.Add(Panel);
                Panel.gameObject.GetComponent<UITab>().changeButtons(Panel, isActive);
            }
        }
    }
}
