using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

  private Dictionary<int, Dictionary<int, Transform>> _tiles;

  void Start() {
    _tiles = new Dictionary<int, Dictionary<int, Transform>>();
    transform.localPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 20));
  }

  public void add_tile(Vector2 grid_location, Transform transform) {
    var grid_x = (int)grid_location.x;
    var grid_y = (int)grid_location.y;
    if (!_tiles.ContainsKey(grid_x)) {
      _tiles[grid_x] = new Dictionary<int, Transform>();
    }
    _tiles[grid_x][grid_y] = transform;
  }

  public Transform get_tile_at(Vector2 grid_location) {
    var grid_x = (int)grid_location.x;
    var grid_y = (int)grid_location.y;
    return _tiles[grid_x][grid_y];
  }
}
