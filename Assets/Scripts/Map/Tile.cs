using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

  public delegate void ClickedAction(Vector2 location);
  public event ClickedAction OnClick;

  public string type {
    get; set;
  }

  public Vector2 grid_location {
    get; set;
  }

  void OnMouseDown() {
    if (OnClick != null) {
      OnClick(grid_location);
    }
  }
}
