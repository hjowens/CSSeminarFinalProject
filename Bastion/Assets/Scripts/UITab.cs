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
        if(isActive == true)
        {
            changeButtons(this.transform, false);
        }
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

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void changeButtons(Transform Panel, bool target)
    {
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
