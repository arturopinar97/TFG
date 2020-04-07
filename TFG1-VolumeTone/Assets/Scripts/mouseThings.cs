using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class mouseThings : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject sph;
    public Rigidbody rb;
    private Vector3 mOffset;
   public Vector3[] positions = new Vector3[3];
    int cont = 0;
   public bool moved= false;


    private float mZCoord;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void OnMouseDown()

    {

        mZCoord = Camera.main.WorldToScreenPoint(

            gameObject.transform.position).z;



        // Store offset = gameobject world pos - mouse world pos

        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

    }



    private Vector3 GetMouseAsWorldPoint()

    {

        // Pixel coordinates of mouse (x,y)

        Vector3 mousePoint = Input.mousePosition;



        // z coordinate of game object on screen

        mousePoint.z = mZCoord;



        // Convert it to world points

        return Camera.main.ScreenToWorldPoint(mousePoint);

    }



    void OnMouseDrag()

    {
       
        

          transform.position = GetMouseAsWorldPoint() + mOffset;
        

    }

    void OnMouseUp()
    {
        
        if(cont < 3)
        {
            positions[cont] = transform.position;
            cont++;
            Debug.Log(transform.position);
        }
        
       // Debug.Log(transform.position);
       // Debug.Log("Drag ended!");
       // Debug.Log(cont);

    }

    void Update()
    {
        Debug.Log("Direction:" , (transform.forward).ToString());
    }
}
