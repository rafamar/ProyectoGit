using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
        private float _speed = 2.0f;
        void Start()
        {
        }

        void Update()
        {
            Vector3 vector = Vector3.zero;
            bool assigned = false;

            if (Input.GetKeyDown(KeyCode.P))
            {
                CommandManager.Instance.Play();
            }

            else if (Input.GetKeyDown(KeyCode.R))
            {
                CommandManager.Instance.Rewind();
            }
            else
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    vector += Vector3.up;
                    assigned = true;
                }

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    vector += Vector3.left;
                    assigned = true;
                }

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    vector += Vector3.right;
                    assigned = true;
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    vector += Vector3.down;
                    assigned = true;
                }

                //move command
                if (assigned)
                {
                    ICommand moveCommand = new MoveCommand(this.transform, _speed, vector);
                    moveCommand.Execute();
                    CommandManager.Instance.AddCommand(moveCommand);
                }
                else
                {
                    CommandManager.Instance.Rewind();
                }
            }
        }
    }
