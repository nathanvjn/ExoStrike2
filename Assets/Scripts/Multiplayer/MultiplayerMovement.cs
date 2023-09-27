using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MultiplayerMovement : NetworkBehaviour
{
    [Header("Player")]
    public Rigidbody r;

    [Header("Movement")]
    public float speed;
    private Vector3 movement;
    private Vector3 airMovement;

    public float forwardSpeed;
    public float sideSpeed;

    public float beginningSpeed;
    public float maxSpeed;
    private float normalDrag;
    public float dragWhenPlayerNotMoving;

    [Header("AirMovement")]
    public float airSpeed;

    [Header("Jumping")]
    public float gravity;
    public RaycastHit groundHit;
    public bool isGrounded;
    public float jumpSpeed;
    public float timeInAirGravity;
    private float normalGravity;

    public string spawnLocationsTag;

    private void Start()
    {
        beginningSpeed = speed;
        normalDrag = GetComponent<Rigidbody>().drag;
        normalGravity = gravity;
        Debug.Log($"Player {GetComponentInParent<NetworkIdentity>().netId} connected to the server!");
        if (this.isLocalPlayer)
        {
            GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");
            GameObject[] _spawnLocations = GameObject.FindGameObjectsWithTag(spawnLocationsTag);
            float _locationScore = -1;
            GameObject _choosenSpawnLocation = new GameObject();
            for (int i = 0; i < _spawnLocations.Length; i++)
            {
                float _score = 9999;
                if (GetComponentInParent<NetworkIdentity>().netId > 1)
                {
                    foreach (GameObject _player in _players)
                    {
                        if(Vector3.Distance(_player.transform.position, _spawnLocations[i].transform.position) < _score)
                        {
                            _score = Vector3.Distance(_player.transform.position, _spawnLocations[i].transform.position);
                        }
                    }
                }
                else
                {
                    _score = 9999;
                }

                if (_score > _locationScore)
                {
                    _locationScore = _score;
                    _choosenSpawnLocation = _spawnLocations[i];
                }
                else if (_score == _locationScore)
                {
                    if (Mathf.RoundToInt(Random.value) == 0)
                    {
                        _locationScore = _score;
                        _choosenSpawnLocation = _spawnLocations[i];
                    }
                }
            }
            transform.position = _choosenSpawnLocation.transform.position;
        }
    }

    void Update()
    {
        if (this.isLocalPlayer)
        {
            //movement
            if (isGrounded)
            {
                if (GetComponent<PlayerSliding>().isSliding == false)
                {
                    forwardSpeed = Input.GetAxis("Vertical");
                }

                else
                {
                    forwardSpeed = 1;
                }


                if (GetComponent<PlayerSliding>().isSliding == false)
                {
                    sideSpeed = Input.GetAxis("Horizontal");
                }
            }

            //player can only move when on ground
            if (isGrounded)
            {
                // Calculate the new velocity based on input
                movement = transform.forward * forwardSpeed * speed + transform.right * sideSpeed * speed;
            }

            //air movement speed
            if (isGrounded == false)
            {
                sideSpeed = Input.GetAxis("Horizontal");

                // Calculate the new velocity based on input
                airMovement = transform.right * sideSpeed * airSpeed;
                r.AddForce(airMovement);
            }

            // Set the Rigidbody's velocity directly
            r.velocity = new Vector3(movement.x, r.velocity.y, movement.z);

            //add drag when player not moving
            if (sideSpeed < 0.6f && forwardSpeed < 0.6f && isGrounded)
            {
                r.drag = dragWhenPlayerNotMoving;
            }

            else
            {
                r.drag = normalDrag;
            }
        }
    }

    private void FixedUpdate()
    {
        if (this.isLocalPlayer)
        {
            //limit speed
            if (r.velocity.magnitude > maxSpeed && GetComponent<PlayerSliding>().isSliding == false)
            {
                GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(r.velocity, maxSpeed);
            }

            //change gravity

            r.AddForce(-transform.up * gravity * Time.deltaTime);

            if (isGrounded == false)
            {
                gravity += timeInAirGravity;

                //als te lang in de lucht komt er meer gravity
                gravity = gravity * 1.05f;
            }

            else
            {
                gravity = normalGravity;
            }





            //jumping
            if (Input.GetButton("Jump") && isGrounded)
            {
                r.AddForce(transform.up * jumpSpeed * Time.deltaTime);
                print("jumping");
            }
        }
    }

    //isGrounded
    private void OnTriggerEnter(Collider other)
    {
        if (this.isLocalPlayer)
        {
            //player does not get forced into ground
            r.velocity = new Vector3(r.velocity.x, 0f, r.velocity.z);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        isGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }
}
