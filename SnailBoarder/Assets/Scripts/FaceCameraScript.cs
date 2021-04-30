using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCameraScript : MonoBehaviour
{
    GameObject cam;

    float timer = 0.0f;
    float maxTime = 3f;

    Vector3 initPos;
    Vector3 endPos;

    SpriteRenderer spriteRenderer;
    Color colorSprite;

    void Start()
    {
        cam = GameObject.Find("Camera");
        Destroy(gameObject, maxTime);

        initPos = transform.position;
        endPos = initPos + new Vector3(0.0f, 0.03f, 0.0f);

        spriteRenderer = GetComponent<SpriteRenderer>();
        colorSprite = spriteRenderer.color;
    }

    void Update()
    {
        timer += Time.deltaTime;
        Vector3 rotVector = cam.transform.position - transform.position;

        if (cam.transform.rotation.eulerAngles.y > 90f && cam.transform.rotation.eulerAngles.y < 270f)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;

        //Debug.Log(cam.transform.rotation.eulerAngles.y);

        transform.rotation = Quaternion.LookRotation(rotVector) * Quaternion.Euler(0, 180, 0);

        float t = timer / maxTime;
        float offsetY = Mathf.Lerp(initPos.y, endPos.y, t) - initPos.y;
        transform.position = new Vector3(transform.position.x, transform.position.y + offsetY, transform.position.z);
        colorSprite.a = Mathf.Lerp(1, 0, t);
        spriteRenderer.color = colorSprite;
    }
}
