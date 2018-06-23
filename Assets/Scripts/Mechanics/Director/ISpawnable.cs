using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ISpawnable {

  List<Vector2> spawn_locations();
  
  bool location_spawnable(Vector2 grid_location);
  bool location_pathable(Vector2 grid_location);

  void spawn(Vector2 grid_location);
}
