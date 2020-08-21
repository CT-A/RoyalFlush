using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 movement;

    private Rigidbody2D rb;

    public bool isMelee;

    public Weapon weapon = null;

    public Vector2 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = 5f;
        if (weapon != null)
        {
            weapon.InstantiateWeapon(this);
            weapon = GameObject.FindWithTag("Weapon").GetComponent<Weapon>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if (Input.GetMouseButton(0) && (weapon != null))
        {
            weapon.Attack(mousePos);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveSpeed * movement * Time.fixedDeltaTime);
    }

    public void LoadFromSave(PlayerData data)
    {
        gameObject.transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
    }
}
