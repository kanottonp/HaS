﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionCollider : MonoBehaviour {
    private PickableObj itemHold;
    private bool isHold; 
    private GameObject interactObj;
    private bool isAction;

	// Use this for initialization
	void Start () {
        isAction = false;
        interactObj = null;
        isHold = false;
        Physics.IgnoreLayerCollision(9, 10);
	}
	
	// Update is called once per frame
	void Update () {
		if(interactObj != null)
        {
            InteractabltObj obj = interactObj.GetComponent<InteractabltObj>();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PickableObj tmp = obj as PickableObj;
                if (tmp != null)
                {
                    itemHold = interactObj.GetComponent<PickableObj>();
                    itemHold.Pick();
                    isHold = true;
                }
                isAction = true;
                obj.actionDirection = this.transform.forward.normalized;
                obj.action();
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                isAction = false;
                obj.noAction();
            }
        }
        else
        {
            isAction = false;
        }

        if (isHold)
        {
            itemHold.Pick();
            itemHold.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, this.transform.eulerAngles.y, this.transform.eulerAngles.z);
            Vector3 direction = this.transform.forward.normalized * 0.5f;
            itemHold.transform.position = new Vector3(this.transform.position.x + direction.x, (this.transform.position.y - 0.1f + direction.y), this.transform.position.z + direction.z) + transform.right.normalized*0.3f;
        }
	}

    public bool IsAction()
    {
        return isAction;
    }

    public bool isItemHold()
    {
        return itemHold != null;
    }

    public void ThrowItemHold()
    {
        if(itemHold != null)
        {
            isHold = false;
            itemHold.ThrowObj();
            itemHold = null;
        }
    }

    public void ToggleHold()
    {
        isHold = !isHold;
        if (isHold)
        {
            itemHold.Pick();
            return;
        }
        itemHold.Keep();
    }

    public bool IsHold()
    {
        return isHold;
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other);
        if (other.gameObject.CompareTag("Interact Obj"))
        {
            interactObj = other.gameObject;
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interact Obj"))
        {
            InteractabltObj obj = interactObj.GetComponent<InteractabltObj>();
            obj.noAction();
            interactObj = null;
        }
    }
}