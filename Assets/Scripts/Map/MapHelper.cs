using UnityEngine;
using System.Collections;

public class MapHelper {

  private const int PIXELS = 30;

  public static Vector2 grid_to_world_location(Vector2 grid_location) {
    float x = grid_location.x * TileGenerator.TILE_SIZE + TileGenerator.TILE_SIZE * 0.5f;
    float y = grid_location.y * TileGenerator.TILE_SIZE + TileGenerator.TILE_SIZE * 0.5f;
    return new Vector2(x, y);
  }
}
