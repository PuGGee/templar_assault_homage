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
    foreach (var tile in map.tiles()) {
      tile.OnClick += click;
    }
    set_position();
    spawn_templar(new Vector2(2, 1));
    spawn_alien(new Vector2(4, 6));
  }

  void click(Vector2 grid_location) {
    _phase.click(grid_location);
  }

  void Update() {
    if (Input.GetKeyDown("n")) {
      if (_phase.can_progress()) {
        _phase = _phase.next_phase();
        foreach (var templar in map.templars()) {
          templar.reset_turn();
        }
      } else {
        _phase.keypress("n");
      }
    }

    if (Input.GetKeyDown("w")) {
      _phase.keypress("w");
    }
    if (Input.GetKeyDown("a")) {
      _phase.keypress("a");
    }
    if (Input.GetKeyDown("s")) {
      _phase.keypress("s");
    }
    if (Input.GetKeyDown("d")) {
      _phase.keypress("d");
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
