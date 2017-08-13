using UnityEngine;
using System.Collections;

public class Templar : MonoBehaviour {

  public int vitality;
  public int armour;
  public int movement;

  public int shots;
  public int damage;

  private int _health;

  public Map map {
    get; set;
  }

  void Start() {
    _health = vitality;
  }

  public void move(Vector2 current_grid_location, Vector2 new_grid_location) {
    map.move_actor(current_grid_location, new_grid_location);
    transform.localPosition = MapHelper.grid_to_world_location(new_grid_location);
  }
}
