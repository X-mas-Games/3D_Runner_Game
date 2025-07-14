using UnityEngine;


public class MoveBarrier : MonoBehaviour
{
    public bool canMove = true;
    public float speedBarrier; // Speed movement along the Z axis

    void Update()
    {
        MovementBarrier();
    }

   public void MovementBarrier()
    {
        if (canMove)
        {
            transform.position += new Vector3(0, 0, -speedBarrier * Time.deltaTime);
        }
        
    }
}
