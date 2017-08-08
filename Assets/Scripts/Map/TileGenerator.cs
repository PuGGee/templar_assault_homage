using UnityEngine;
using System.Collections;

public class TileGenerator : MonoBehaviour {

  public TextAsset map_file;
  public Transform tile_prefab;

  public Sprite empty_tile;
  public Sprite wall_tile;

  public const int TILE_SIZE = 10;

  void Start() {
    string[] lines = map_file.text.Split("\n" [0]);
    for (int i = 0; i < lines.Length; i++) {
      process_line(i, lines[i]);
    }
  }

  private void process_line(int y, string line) {
    for (int i = 0; i < line.Length; i++ ) {
      render_tile(new Vector2(i, y), sprite_for(line[i].ToString()));
    }
  }

  private void render_tile(Vector2 location, Sprite sprite) {
    Transform tile_transform = Instantiate(tile_prefab) as Transform;
    tile_transform.localPosition = location * TILE_SIZE;
    SpriteRenderer sprite_renderer = tile_transform.GetComponent<SpriteRenderer>();
    sprite_renderer.sprite = sprite;
  }

  private Sprite sprite_for(string token) {
    if (token == "x") {
      return wall_tile;
    } else if (token == " ") {
      return empty_tile;
    } else {
      return null;
    }
  }
}
