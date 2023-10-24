using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    Vector2 direction = Vector2.zero;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public GameObject winTextObject;
    bool stopwatchActive = false;
    float currentTime;
    public TextMeshProUGUI currentTimeText;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        currentTime = 0;
        stopwatchActive = true;
        winTextObject.SetActive(false);
        SetCountText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(stopwatchActive == true)
        {
                currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        // transform.position += new Vector3(direction.x, 0, direction.y) * Time.deltaTime;
        rb.AddForce(movement * speed);
    }

    void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
        movementX = direction.x;
        movementY = direction.y;
    }

    void SetCountText()
    {
        countText.text = "Count = " + count.ToString();
        if(count >= 12)
        {
            winTextObject.SetActive(true);
            stopwatchActive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }
}
