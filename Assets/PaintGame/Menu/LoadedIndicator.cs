using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PaintGame;

public class LoadedIndicator : MonoBehaviour {

    [SerializeField]
    private string canvasName;

	void Start () {
        PGServiceLocator.MenuManager.SetCanvas(canvasName);
        Destroy(gameObject);
	}
}
