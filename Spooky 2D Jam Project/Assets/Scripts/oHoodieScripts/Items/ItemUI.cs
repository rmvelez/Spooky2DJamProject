using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{

    public Image selectImage;
    public Image itemImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        selectImage.color = new Color(1, 0.5f, 0);
    }

    public void Deselect()
    {
        selectImage.color = new Color(1, 1, 1);

    }
}
