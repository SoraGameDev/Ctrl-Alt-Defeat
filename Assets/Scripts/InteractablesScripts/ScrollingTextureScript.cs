using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingTextureScript : MonoBehaviour {
    
    MeshRenderer meshRend;

    //   SCROLL SPEED   //
    public float scrollSpeedX;
    public float scrollSpeedY;

    //    COLOUR MANIPULATION  //
    Color targetColour;
    public bool isRed;
    Color green;
    public Color red;


    // Use this for initialization
    void Start () {

        meshRend = GetComponent<MeshRenderer>();
        green = meshRend.material.color;
        targetColour = green;
    }

    // Update is called once per frame
    void Update () {
       
	}

    private void FixedUpdate()
    {
        meshRend.material.mainTextureOffset += new Vector2(scrollSpeedX * Time.deltaTime, scrollSpeedY * Time.deltaTime);

        if (isRed)
        {
            targetColour = red;
        }
        else
        {
            targetColour = green;
        }

        // Lerp to the desired colour. Will lerp the the same colour if the targetColour value is unchanged
        meshRend.material.color = Color.Lerp(meshRend.material.color, targetColour, 0.8f * Time.deltaTime);
    }

}
