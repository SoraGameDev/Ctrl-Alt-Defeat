using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotCircle : MonoBehaviour {

    public GameObject spherePrefab;

    public int pointCount;

    public float radius;

    public List<GameObject> points = new List<GameObject>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        PlotCirclePoints();

    }

    void PlotCirclePoints()
    {

        DeletePoints();

        float angle = 0;
        Vector3 centre = transform.position;

        points = new List<GameObject>();

        /*We take the width of one sphere and multiply it by the incremental angle that we use to make a full circle. For example, if we want to create a circle with 4 points, the first angle/slice is 90 degrees OR 1.5708 radians (we use radians...
        ...because its easier to compute). 2 pi is 360 degrees. So 2 pi multiplied by the number of points gives us the angle between each point (just like dividing 360 by 4 gives 90 degrees).

        We multiply this resultant angle by the size of the sphere to get a radius that allows the cirumferance of the circle to grow by the width of one sphere each time.

        Example:
        sphere size = 6
        pointCount = 4

        radius = 6 units / (2 pi / 4 units)
        radius = 6 units / (1.57 radians)
        radius = 3.81971863421 units
         
         */
        radius = spherePrefab.transform.localScale.x / ((Mathf.PI * 2) / pointCount);

        for (int i = 0; i < pointCount; i++)
        {

            angle += ((Mathf.PI * 2) / pointCount);


            /* We're applying the values along the sine and cosine curves to the position of each sphere. Because sine and cosine are offset from each other (see sine and cosine graphs), the resulting curve is a circle
            */
            float x = Mathf.Sin(angle) * radius;
            float z = Mathf.Cos(angle) * radius;

            Vector3 position = centre + new Vector3(x, 0, z);

            GameObject point = GameObject.Instantiate(spherePrefab);
            point.transform.position = position;

            points.Add(point);

        }

    }

    void DeletePoints()
    {

        foreach(GameObject point in points)
        {
            Destroy(point);
        }

    }

}
