using UnityEngine;

public class TruckController : MonoBehaviour
{

    private LayerMask _fruitLayerMask => LayerMask.NameToLayer("Fruit");

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!collision.gameObject.layer.Equals(_fruitLayerMask))
        {
            return;
        }

        var rigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();

        rigidbody2D.velocity = Vector2.Reflect(collision.relativeVelocity, collision.contacts[0].normal);

    }

}
