using UnityEngine;
using System.Collections;

public class TileGenerator : MonoBehaviour {

  public TextAsset map_file;
  public Transform tile_prefab;
  public Transform tile_container;
  public Map map;

  public Transform templar_transform;
  public Transform alien_transform;

  public Sprite empty_tile;
  public Sprite wall_tile;

  public const int TILE_SIZE = 1;

  void Awake() {
    string[] lines = map_file.text.Split("\n" [0]);
    for (int i = 0; i < lines.Length; i++) {
      process_line(lines.Length - i - 2, lines[i]);
    }
  }

  private void process_line(int y, string line) {
    for (int i = 0; i < line.Length; i++ ) {
      render_tile(new Vector2(i, y), line[i].ToString());
    }
  }

  private void render_tile(Vector2 location, string tile_type) {
    Transform tile_transform = Instantiate(tile_prefab) as Transform;
    tile_transform.parent = tile_container;
    tile_transform.localPosition = MapHelper.grid_to_world_location(location);

    var script = tile_transform.GetComponent<Tile>();
    script.type = tile_type == "t" ? " " : tile_type;
    script.grid_location = location;

    SpriteRenderer sprite_renderer = tile_transform.GetComponent<SpriteRenderer>();
    sprite_renderer.sprite = sprite_for(tile_type);

    map.add_tile(location, tile_transform);

    if (tile_type == "t") {
      spawn_templar(location);
    }
  }

  private Sprite sprite_for(string token) {
    if (token == "x") {
      return wall_tile;
    } else if (token == " " || token == "t") {
      return empty_tile;
    } else {
      return null;
    }
  }

  private Transform spawn_templar(Vector2 grid_location) {
    var templar = Instantiate(templar_transform) as Transform;
    map.add_actor(grid_location, templar);
    templar.parent = transform;
    templar.localPosition = MapHelper.grid_to_world_location(grid_location);

    var script = templar.GetComponent<Templar>();
    script.map = map;

    return templar;
  }

  private Transform spawn_alien(Vector2 grid_location) {
    var alien = Instantiate(alien_transform) as Transform;
    map.add_actor(grid_location, alien);
    alien.parent = transform;
    alien.localPosition = MapHelper.grid_to_world_location(grid_location);

    var script = alien.GetComponent<Alien>();
    script.map = map;

    return alien;
  }
}
