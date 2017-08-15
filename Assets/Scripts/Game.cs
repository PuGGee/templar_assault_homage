using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

  public Transform templar_transform;
  public Transform alien_transform;
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
    spawn_alien(new Vector2(4, 6));
  }

  void Update() {
    if (Input.GetMouseButtonDown(0)) {
      var grid_location = MapHelper.screen_to_grid_location(Input.mousePosition);
      _phase.click(grid_location);
    }
    if (Input.GetKeyDown("n")) {
      _phase = _phase.next_phase();
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
    script.map = map_transform.GetComponent<Map>();

    return templar;
  }

  private Transform spawn_alien(Vector2 grid_location) {
    var alien = Instantiate(alien_transform) as Transform;
    map.add_actor(grid_location, alien);
    alien.parent = transform;
    alien.localPosition = MapHelper.grid_to_world_location(grid_location);

    var script = alien.GetComponent<Alien>();
    script.map = map_transform.GetComponent<Map>();

    return alien;
  }
}
