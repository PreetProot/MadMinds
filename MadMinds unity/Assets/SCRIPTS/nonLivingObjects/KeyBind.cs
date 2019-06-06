using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class KeyBind : MonoBehaviour
{
    public Dictionary<string, KeyCode> Keys = new Dictionary<string, KeyCode>();
    public Text up, left, down, right, shoot;
    public GameObject currentKey; //currently using key controls

    // Start is called before the first frame update
    void Start()
    {
        Keys.Add("UP", KeyCode.W);
        Keys.Add("LEFT", KeyCode.A);
        Keys.Add("DOWN", KeyCode.S);
        Keys.Add("RIGHT", KeyCode.D);
        Keys.Add("SHOOT", KeyCode.Space);

        up.text = Keys["UP"].ToString();
        left.text = Keys["LEFT"].ToString();
        down.text = Keys["DOWN"].ToString();
        right.text = Keys["RIGHT"].ToString();
        shoot.text = Keys["SHOOT"].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(Keys["UP"]))
        {
            //move action
            //Debug.Log("UP");
        }

        if (Input.GetKeyDown(Keys["LEFT"]))
        {
            //move action
            //Debug.Log("UP");
        }

        if (Input.GetKeyDown(Keys["DOWN"]))
        {
            //move action
            //Debug.Log("UP");
        }

        if (Input.GetKeyDown(Keys["RIGHT"]))
        {
            //move action
            //Debug.Log("UP");
        }

        if (Input.GetKeyDown(Keys["SHOOT"]))
        {
            //move action
            //Debug.Log("UP");
        }
    }

    private void OnGUI()
    {
        if(currentKey != null) //when current key is pressed it will change the controls
        {
            Event keyBind = Event.current;
            if(keyBind.isKey)
            {
                Keys[currentKey.name] = keyBind.keyCode; //will replace control with the current key pressed
                currentKey.transform.GetChild(0).GetComponent<Text>().text = keyBind.keyCode.ToString();
                currentKey = null;
            }

        }
    }

    public void ChangeKey(GameObject clicked)
    {
        currentKey = clicked;
    }
}
