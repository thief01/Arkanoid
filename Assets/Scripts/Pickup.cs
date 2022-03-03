using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private const float SPEED = -5;
    private enum PickupType
    {
        sizeUp,
        sizeDown,
        bullets,
        cloneBall,
        addBall
    }

    [SerializeField]
    private PickupType type;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, SPEED);
        Destroy(gameObject, 10);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (type)
        {
            case PickupType.sizeUp:
                collision.gameObject.GetComponent<PlatformController>().SizeUp();
                break;
            case PickupType.sizeDown:
                collision.gameObject.GetComponent<PlatformController>().SizeDown();
                break;
            case PickupType.bullets:
                collision.gameObject.GetComponent<WeaponController>().AddWeapon();
                break;
            case PickupType.cloneBall:
                GameState.instace.CloneBalls();
                break;
            case PickupType.addBall:
                collision.gameObject.GetComponent<PlatformController>().AddBall();
                break;
            default:
                break;
        }
        Destroy(gameObject);
    }
}
