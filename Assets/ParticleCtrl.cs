using Coffee.UIExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorParticle : MonoBehaviour
{
    UIParticle part;
    [SerializeField]Texture2D cursorDefault;
    [SerializeField] Texture2D cursorClick;
    // Start is called before the first frame update
    private void Awake()
    {
        cursorClick = Resources.Load<Texture2D>("Cursor_Click");
        cursorDefault = Resources.Load<Texture2D>("Cursor_Default");
    }
    void Start()
    {
        part = GetComponent<UIParticle>();
        Cursor.SetCursor(cursorDefault, new Vector2(-1,1), CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            transform.position = Input.mousePosition;
            Cursor.SetCursor(cursorClick, new Vector2(-1, 1), CursorMode.ForceSoftware);
            part.Play();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Cursor.SetCursor(cursorDefault, new Vector2(-1, 1), CursorMode.ForceSoftware);
        }
    }
}
