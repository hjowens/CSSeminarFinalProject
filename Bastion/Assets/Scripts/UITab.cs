using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UITab : MonoBehaviour
{
    public bool isActive = true;

    
    public void toggleTab()
    {
        /*
        This function is a bit odd because I initially wanted to design it to be called by a button with a field
        specifying which tab to toggle passed into the function. But buttons in Unity don't take objects as fields, so
        it runs in the tab itself and changes all of the contained buttons instead of running in a tab manager and changing the tab.
        */
        // calls the changebuttons if it's active.
        if(isActive == true)
        {
            changeButtons(this.transform, false);
        }
        // calls the changebuttons of every other tab on the same heirarchy level if it's not active, passing in false and making itself true.
        else if(isActive == false)
        {
            Transform Parent = transform.parent;
            List<Transform> Panels = new List<Transform>();
            foreach (Transform Panel in Parent.transform)
            {
                string Tag = Panel.gameObject.tag;
                if (Tag == "Panel")
                {
                    Panels.Add(Panel);
                    //Debug.Log(Panel.name);
                }
            }
            foreach (Transform Panel in Panels)
            {
                if (Panel.gameObject.Equals(this.gameObject))
                {
                    changeButtons(Panel, true);
                    //Debug.Log("hit this panel");
                }
                else
                {
                    changeButtons(Panel, false);
                    //Debug.Log("hit another panel");
                }
            }
        }
    }
    public bool CheckMouseOverTab()
    {
        /*
        This function is supposed to return true or false based on whether the mouse is over a tab, but it's
        not finished yet. I'm probably gonna pass a vector into it and convert it to camera space coordinates
        so I can check if it's over the tab boundaries.
        */
        if (EventSystem.current.IsPointerOverGameObject())
        {
            //Debug.Log("false");
            return false;
        }
        else
        {
            //Debug.Log("true");
            return true;
        }
    }

    public void changeButtons(Transform Panel, bool target)
    {
        /*
        This function loops through all of the buttons contained in a tab and activates or deactivates them,
        turning on or off their ability to be clicked and changing them to opaque or invisible.
        */
        foreach (Transform child in Panel)
        {
            try
            {
                GameObject obj = child.gameObject;
                //Debug.Log(obj.name);
                toggleInteract(obj, target);
            }
            catch
            {
                continue;
            }
        }
        Panel.GetComponent<UITab>().isActive = target;
    }
    public void toggleInteract(GameObject obj, bool target)
    {
        /*
        Called in the changebuttons function, it turns a specific button on or off and opaque or invisible
        depending on the passed in bool.
        */
        //Debug.Log(obj.name);
        Button targetButton = obj.GetComponent<Button>();
        Text textRenderer = obj.GetComponentInChildren<Text>();

        targetButton.interactable = target;

        if (target == true)
        {
            textRenderer.color = new Color(0, 0, 0, 255);
        }
        else if (target == false)
        {
            textRenderer.color = new Color(0, 0, 0, 0);
        }
    }
}
