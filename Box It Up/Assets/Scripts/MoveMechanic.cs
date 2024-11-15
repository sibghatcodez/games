using UnityEngine;

public class MoveMechanic : MonoBehaviour
{
    // Speed of the movement
    // public float speed = 0.5f;
    public float speed = 1f;
    public float yStartAxis;
    public float yAxisRange;
    int rand = 2;

    // Update is called once per frame
    void Start () {
        yStartAxis = transform.position.y;
        yAxisRange = yStartAxis+10;
        //Invoke("RandomParameter", 5f);s
    }
    void RandomParameter () {
        if(rand == 1) rand = 2;
        if(rand == 2) rand = 3;
        Invoke("RandomParameter", 5f);
    }
    void Update()
    {
        MoveHolder();
    }

    void MoveHolder()
    {
        float xA = 170;
        float xB = 150;
        float yA = yAxisRange;
        float yB = yStartAxis;

        // Calculate the time-based interpolation factor
        float xt = Mathf.PingPong(Time.time * speed, 1);
        float yt = Mathf.PingPong(Time.time * speed/2, 1);
        float lerpedyA = Mathf.Lerp(yA, yB, yt);
        float y = Mathf.Clamp(lerpedyA, yA,yB);
        transform.position = new Vector3(Mathf.Lerp(xA, xB, xt), lerpedyA, transform.position.z);
    }
}
