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


            RaycastHit2D hit = Physics2D.Raycast(transform.position + transform.up, transform.TransformDirection(Vector3.up) * 1000);

            Debug.DrawLine(transform.position + transform.up, transform.TransformDirection(Vector3.up) * 1000);
            if (hit)
            {
                hit.transform.gameObject.GetComponent<Damagable>().TakeDamage(Damage);
                print(hit.transform);
            }
        }


    }

    public class PlayerBaker: Baker<PlayerMono>
    {
                public override void Bake(PlayerMono playerMono)
                {
                    // add logic here later
                }
        
    }
}