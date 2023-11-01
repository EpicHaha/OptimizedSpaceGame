using Unity.Burst.CompilerServices;
using Unity.Entities;
using UnityEngine;

namespace Player
{
    public class PlayerMono : Damagable
    {
        public float Speed;
        public float RotationSpeed;
        public int Damage;

        private void Update()
        {
            if (Input.GetKey(KeyCode.D))
            {
                Rotate(-RotationSpeed);
            }
            else if (Input.GetKey(KeyCode.A))
            {

                Rotate(RotationSpeed);
            }

            if (Input.GetKey(KeyCode.W))
            {
                Move(Speed);
            }else if (Input.GetKey(KeyCode.S))
            {
                Move(-Speed);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }


        private void Rotate(float direction)
        {
            transform.Rotate(new Vector3(0,0,direction));
        }
        private void Move(float speed)
        {
            transform.position += transform.up * speed;
        }


        private void Shoot()
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity, 0))
            {
            }
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.up) *1000, Color.yellow);
        }


    }
}