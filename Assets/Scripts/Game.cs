using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {

  public Transform map_transform;

  public Transform tile_gen;

  private Phase _phase;

  private const float MAP_MOVE = 0.2f;

  public Text stage_text;

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

  public void next_phase() {
    if (_phase.can_progress()) {
      _phase = _phase.next_phase();
    } else {
      _phase.keypress("n");
    }
    if (_phase.can_progress()) {
      stage_text.text = "Next Phase";
    } else {
      stage_text.text = "Continue";
    }
  }

  void Update() {
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

    if (Input.GetKey("t")) {
      map_transform.position -= new Vector3(0, MAP_MOVE, 0);
      tile_gen.position -= new Vector3(0, MAP_MOVE, 0);
    }
    if (Input.GetKey("f")) {
      map_transform.position -= new Vector3(-MAP_MOVE, 0, 0);
      tile_gen.position -= new Vector3(-MAP_MOVE, 0, 0);
    }
    if (Input.GetKey("h")) {
      map_transform.position -= new Vector3(MAP_MOVE, 0, 0);
      tile_gen.position -= new Vector3(MAP_MOVE, 0, 0);
    }
    if (Input.GetKey("g")) {
      map_transform.position -= new Vector3(0, -MAP_MOVE, 0);
      tile_gen.position -= new Vector3(0, -MAP_MOVE, 0);
    }
  }

  private void set_position() {
    transform.localPosition = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
  }
}
