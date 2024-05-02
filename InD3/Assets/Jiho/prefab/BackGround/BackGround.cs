using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{


    [SerializeField] MeshRenderer BG;
    [SerializeField] float Speed;
    [SerializeField] float Offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveBackGround();
    }

    void moveBackGround()
    {
        Offset +=  Speed*Time.deltaTime;
        BG.material.mainTextureOffset = new Vector2(Offset, 0);
    }
}
