using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

  public Transform templar_transform;

  private Phase _phase;

  void Start() {
    set_position();
    spawn_templar(new Vector2(2, 1));
  }

  void Update() {
    if (Input.GetMouseButtonDown(0)) {
      Debug.Log(MapHelper.screen_to_grid_location(Input.mousePosition));
    }
  }

  private void set_position() {
    transform.localPosition = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
  }

  private Transform spawn_templar(Vector2 grid_location) {
    var templar = Instantiate(templar_transform) as Transform;
    templar.parent = transform;
    templar.localPosition = MapHelper.grid_to_world_location(grid_location);

    var script = templar.GetComponent<Templar>();
    script.vitality = 86;
    script.armour = 50;
    script.movement = 3;
    script.shots = 2;
    script.damage = 20;

    return templar;
  }
}
