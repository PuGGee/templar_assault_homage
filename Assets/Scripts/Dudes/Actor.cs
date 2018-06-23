using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {

  public Map map {
    get; set;
  }

  public virtual void move(Vector2 current_grid_location, Vector2 new_grid_location) {
    map.move_actor(current_grid_location, new_grid_location);
    transform.localPosition = MapHelper.grid_to_world_location(new_grid_location);
  }
  
  public void die() {
    map.remove_actor_at(map.actor_location(transform));
    Destroy(gameObject);
  }
}
