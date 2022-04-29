using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITab : MonoBehaviour
{
    private bool isActive = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void toggleTab()
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in transform)
        {
            try
            {
                GameObject obj = child.gameObject;
                //Debug.Log(obj.name);
                toggleInteract(obj, !isActive);
            }
            catch
            {
                continue;
            }
        }
        isActive = !isActive;
    }
    public void toggleInteract(GameObject obj, bool target)
    {
        Debug.Log(obj.name);
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
