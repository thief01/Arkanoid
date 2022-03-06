using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private const float SPEED = -2.5f;
    private readonly Powerup[] POWERUPS =
    {
        new PowerupSizeUp(),
        new PowerupSizeDown(),
        new PowerupAddWeapon(),
        new PowerupCloneBall(),
        new PowerupAddBall()
    };

    public enum PickupType
    {
        sizeUp,
        sizeDown,
        bullets,
        cloneBall,
        addBall
    }

    private int pickupId = 0;
    [SerializeField]
    private Sprite[] spritesByType;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rigidbody2D;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rigidbody2D.velocity = new Vector2(0, SPEED);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (pickupId < POWERUPS.Length)
            POWERUPS[pickupId].Execute(collision);

        PrefabCollector<Pickup>.Instance.Destroy(this);
    }

    public void SetPickupType(PickupType pickupType)
    {
        pickupId = (int)pickupType;
        OnTypeChanged();
    }

    private void OnTypeChanged()
    {
        if (pickupId < spritesByType.Length)
            spriteRenderer.sprite = spritesByType[pickupId];
    }
}
