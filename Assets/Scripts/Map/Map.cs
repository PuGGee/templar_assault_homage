using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {

  private Dictionary<int, Dictionary<int, Transform>> _tiles;
  private Dictionary<int, Dictionary<int, Transform>> _actors;

  void Awake() {
    _tiles = new Dictionary<int, Dictionary<int, Transform>>();
    _actors = new Dictionary<int, Dictionary<int, Transform>>();
  }

  void Start() {
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

  public void add_actor(Vector2 grid_location, Transform transform) {
    var grid_x = (int)grid_location.x;
    var grid_y = (int)grid_location.y;
    if (!_actors.ContainsKey(grid_x)) {
      _actors[grid_x] = new Dictionary<int, Transform>();
    }
    _actors[grid_x][grid_y] = transform;
  }

  public void remove_actor_at(Vector2 grid_location) {
    if (get_actor_at(grid_location) == null) return;
    var grid_x = (int)grid_location.x;
    var grid_y = (int)grid_location.y;
    _actors[grid_x].Remove(grid_y);
  }

  public Transform get_actor_at(Vector2 grid_location) {
    var grid_x = (int)grid_location.x;
    var grid_y = (int)grid_location.y;
    if (_actors.ContainsKey(grid_x) && _actors[grid_x].ContainsKey(grid_y)) {
      return _actors[grid_x][grid_y];
    } else {
      return null;
    }
  }

  public void move_actor(Vector2 grid_location, Vector2 new_grid_location) {
    var actor = get_actor_at(grid_location);
    remove_actor_at(grid_location);
    add_actor(new_grid_location, actor);
  }

  public List<Transform> actors() {
    var result = new List<Transform>();
    foreach (var sub_dict in _actors.Values) {
      foreach (var transform in sub_dict.Values) {
        result.Add(transform);
      }
    }
    return result;
  }

  public List<Alien> aliens() {
    var result = new List<Alien>();
    foreach (var transform in actors()) {
      if (transform.GetComponent<Alien>()) {
        result.Add(transform.GetComponent<Alien>());
      }
    }
    return result;
  }
}
