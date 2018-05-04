using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

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
}
