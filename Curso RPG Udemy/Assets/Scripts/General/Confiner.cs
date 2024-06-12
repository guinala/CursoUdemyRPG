using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Confiner : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            camera.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            camera.gameObject.SetActive(false);
        }
    }
}
