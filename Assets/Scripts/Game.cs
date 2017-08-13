using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

  public Transform templar_transform;
  public Transform map_transform;

  private Phase _phase;

  private Map map {
    get {
      return map_transform.GetComponent<Map>();
    }
  }

  void Awake() {
    _phase = new MovementPhase(map);
  }

  void Start() {
    set_position();
    spawn_templar(new Vector2(2, 1));
  }

  void Update() {
    if (Input.GetMouseButtonDown(0)) {
      var grid_location = MapHelper.screen_to_grid_location(Input.mousePosition);
      _phase.click(grid_location);
    }
  }

  private void set_position() {
    transform.localPosition = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
  }

  private Transform spawn_templar(Vector2 grid_location) {
    var templar = Instantiate(templar_transform) as Transform;
    map.add_actor(grid_location, templar);
    templar.parent = transform;
    templar.localPosition = MapHelper.grid_to_world_location(grid_location);

    var script = templar.GetComponent<Templar>();
    script.vitality = 86;
    script.armour = 50;
    script.movement = 3;
    script.shots = 2;
    script.damage = 20;
    script.map = map_transform.GetComponent<Map>();

    return templar;
  }
}
