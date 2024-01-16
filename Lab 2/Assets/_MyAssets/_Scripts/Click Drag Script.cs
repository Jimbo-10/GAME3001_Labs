using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDragScript : MonoBehaviour
{
    private bool isDragging = false;
    private Rigidbody2D currentDraggedObject;
    private Vector2 offset;

    // Update is called once per frame
    void Update()
    {
        // Frist we check if player clicked on screen
        if (Input.GetMouseButtonDown(0))
        {
            //Raycast to check if the the mouse is over a collider
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null) //  we click to a object with collider
            {
                // check if the clicked Gameobject has a rigidbody2D
                Rigidbody2D rb2d = hit.collider.GetComponent<Rigidbody2D>(); // try to get rigidbody2d from the gameobject from its collider

                if (rb2d != null)
                {
                    // Start dragging only if no object being dragged
                    isDragging = true;
                    currentDraggedObject = rb2d;

                    offset = rb2d.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                   
                }
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            //Stop dragging
            isDragging = false;
            currentDraggedObject = null;
        }

        if (isDragging && currentDraggedObject != null)
        {
            //Move the dragged Gameobject based on the mouse position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentDraggedObject.transform.position = mousePosition;
            currentDraggedObject.MovePosition(mousePosition + offset);
        }
    }
}
