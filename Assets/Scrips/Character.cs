using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] float speedMovement;
    [SerializeField] Joystick Joystick;


    void Start()
    {

    }

    void Update()
    {
        float horizontal = Joystick.Horizontal; /*Input.GetAxis("Horizontal");*/
        float vertical = Joystick.Vertical; /*Input.GetAxis("Vertical");*/

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        transform.Translate(movement * speedMovement * Time.deltaTime);
    }
}
