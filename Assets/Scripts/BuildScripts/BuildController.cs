﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour {
    public Transform plate;
    public GameObject buildPlate;
    public float gridSize;
    public Material transparent;
    private BuildMode buildMode;
    private Transform transparentPlate;
    private AimController aimControl;
    private bool canBuild;
    private Dictionary<BuildMode, Vector3> scaleCollection;
    private Vector3 scaleVector;
    private bool buildModeOn;
    private int selectedBuildMode;
	// Use this for initialization
	void Start () {
        aimControl = GetComponentInParent<AimController>();
        buildModeOn = false;
        scaleCollection = new Dictionary<BuildMode, Vector3>();
        scaleCollection[BuildMode.BuildMode2x1] = new Vector3 (2, 1 );
        scaleCollection[BuildMode.BuildMode2x2] = new Vector3 (2, 2 );
        scaleCollection[BuildMode.BuildMode2x4] = new Vector3 (2, 4 );
        scaleCollection[BuildMode.BuildMode4x2] = new Vector3 (4, 2 );
       
    }
	
	// Update is called once per frame
	void Update () {
        ManageInput();
        if (buildModeOn)
        {
            SnapToGrid();
            if (Input.GetKeyUp(KeyCode.Mouse1) && canBuild)
            {
                Instantiate(buildPlate, transparentPlate.position, Quaternion.identity);
            }
        }

	}

    private void SnapToGrid()
    {
        float mousePosX = aimControl.mouseCoords.x;
        float mousePosY = aimControl.mouseCoords.y;
        float snapPointX = mousePosX + ((1 - mousePosX) % 1);
        float snapPointY = mousePosY + ((1 - mousePosY) % 1);
        float snapPointZ = 0;
        Vector3 position = new Vector3(snapPointX, snapPointY, snapPointZ);
        transparentPlate.position = position;
        Collider2D collider = Physics2D.OverlapBox(new Vector2(snapPointX, snapPointY), new Vector2(transparentPlate.localScale.x-1, transparentPlate.localScale.y-1), 0);

        if (collider != null)
        {
            canBuild = false;
   
            transparentPlate.gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else
        {
            canBuild = true;
            transparentPlate.gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 0.5f, 0.5f, 0.2f);
        }
    }

    private void GenerateBuild()
    {
        if (transparentPlate != null) Destroy(transparentPlate.gameObject);
        Vector3 position = new Vector3(0, 0, 0);
        transparentPlate = Instantiate(plate, position, Quaternion.identity);
        
    }

    private void ManageInput()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            if ((int)buildMode > 0) buildMode = (BuildMode)(int)buildMode - 1;
            else buildMode = (BuildMode)Enum.GetNames(typeof(BuildMode)).Length - 1;
            plate.localScale = scaleCollection[buildMode];
            buildPlate.transform.localScale = scaleCollection[buildMode];
            GenerateBuild();
            buildModeOn = true;
        }
        if (Input.GetKeyUp(KeyCode.F2))
        {
            if ((int)buildMode < Enum.GetNames(typeof(BuildMode)).Length - 1) buildMode = (BuildMode)(int)buildMode + 1;
            else buildMode = (BuildMode)0;
            plate.localScale = scaleCollection[buildMode];
            buildPlate.transform.localScale = scaleCollection[buildMode];
            GenerateBuild();
            buildModeOn = true;
        }



    }
}
