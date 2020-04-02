using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{

    [SerializeField] private bool scrolling = true, parallax = true;
    [SerializeField] private float backgroundSize;
    [SerializeField] private float parallaxSpeed;
    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex; 
    private float lastCameraX;

    private void Start() {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;

        
    }

    

    private void Update() {
        if (parallax) {
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * parallaxSpeed);
        }
        lastCameraX = cameraTransform.position.x;
        
        if (scrolling) {
            if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
                ScrollLeft();
            if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
                ScrollRight();
        }
    }

    private void ScrollLeft() {
        int lastRight = rightIndex;

        float yPosBefore = layers[leftIndex].position.y;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        Vector3 pos = layers[rightIndex].position;
        pos.y = yPosBefore;
        layers[rightIndex].position = pos;

        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }

    private void ScrollRight() {
        int lastLeft = leftIndex;
        float yPosBefore = layers[leftIndex].position.y;
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        Vector3 pos = layers[leftIndex].position;
        pos.y = yPosBefore;
        layers[leftIndex].position = pos;

        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
            leftIndex = 0;
    }
}
