using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#endif

namespace VinTools.BetterRuleTiles
{
    [CreateAssetMenu(menuName = "2D/Tiles/Better Rule Tile Container", fileName = "Better Rule Tile")]
    public class BetterRuleTileContainer : ScriptableObject
    {
        #region Variables
        public EditorSettings settings = new EditorSettings();
        [Space]
        public List<BetterRuleTile> _tileObjects = new List<BetterRuleTile>();
        public List<GridCell> _grid = new List<GridCell>();
        public List<TileData> _tiles = new List<TileData>();

        public List<GridCell> Grid { get => _grid; }
        public List<TileData> Tiles { get => _tiles; }

        #region Classes
        [System.Serializable]
        public class TileData
        {
            public int UniqueID;

            public string Name;
            public Texture2D Texture;

            public Color Color;
            public TileShape TextureShape;
            public bool MirrorTexX;
            public bool MirrorTexY;

            public Sprite DefaultSprite;
            public Tile.ColliderType ColliderType = Tile.ColliderType.Sprite;
            public GameObject DefaultGameObject;

            public TileData(string name, Color color, int id)
            {
                Name = name;
                Color = color;
                UniqueID = id;
            }
        }

        [System.Serializable]
        public class GridCell
        {
            public Vector2Int Position;
            public Sprite Sprite;
            public int TileID;
            public bool UseDefaultSettings = true;
            public bool IsModified;

            public GameObject GameObject;
            public Sprite[] Sprites;
            public Tile.ColliderType ColliderType = Tile.ColliderType.Sprite;
            public RuleTile.TilingRuleOutput.OutputSprite OutputSprite = RuleTile.TilingRuleOutput.OutputSprite.Single;
            public RuleTile.TilingRuleOutput.Transform Transform = RuleTile.TilingRuleOutput.Transform.Fixed;
            public bool IncludeSpriteInOutput = false;

            public float NoiseScale = .5f;
            public RuleTile.TilingRuleOutput.Transform RandomTransform = RuleTile.TilingRuleOutput.Transform.Fixed;

            public float MinAnimationSpeed = 1f;
            public float MaxAnimationSpeed = 1f;

            public List<Vector3Int> NeighborPositions;

            public GridCell(Vector2Int pos)
            {
                Position = pos;
                NeighborPositions = DefaultNeighborPositions();
            }

            //make a copy
            public GridCell(GridCell copy)
            {
                Position = copy.Position;
                Sprite = copy.Sprite;
                TileID = copy.TileID;
                UseDefaultSettings = copy.UseDefaultSettings;

                Sprites = copy.Sprites;
                OutputSprite = copy.OutputSprite;
                Transform = copy.Transform;
                GameObject = copy.GameObject;
                ColliderType = copy.ColliderType;
                IncludeSpriteInOutput = copy.IncludeSpriteInOutput;

                NoiseScale = copy.NoiseScale;
                RandomTransform = copy.RandomTransform;

                MinAnimationSpeed = copy.MinAnimationSpeed;
                MaxAnimationSpeed = copy.MaxAnimationSpeed;

                NeighborPositions = copy.NeighborPositions;
            }

            public bool CheckModified()
            {
                if (!UseDefaultSettings)
                {
                    if (ColliderType != Tile.ColliderType.Sprite) return true;
                    if (OutputSprite != RuleTile.TilingRuleOutput.OutputSprite.Single) return true;
                }
                if (Transform != RuleTile.TilingRuleOutput.Transform.Fixed) return true;

                //TODO Maybe count for hexagonal tiles
                if (NeighborPositions.Count != 8 && NeighborPositions.Count != 6) return true;
                if (NeighborPositions.Min(t => t.x) < -1) return true;
                if (NeighborPositions.Max(t => t.x) < -1) return true;
                if (NeighborPositions.Min(t => t.y) > 1) return true;
                if (NeighborPositions.Max(t => t.y) > 1) return true;

                return false;
            }
        }

        [System.Serializable]
        public class EditorSettings
        {
            public float _zoomAmount = 1;
            public Vector2Int _gridCellSize = new Vector2Int(32, 32);
            public Vector2 _tileRenderOffset = Vector2.zero;
            public Vector2 _gridOffset = Vector2.zero;

            public bool _lockWindows = true;
            public bool _hideSprites = false;
            public bool _showRuler = true;
            public bool _showModified = false;
            public bool _renderSmallGrid = false;
            public float _zoomTreshold = .35f;
            public int _drawerSize = 6;
        }
        public enum TileShape
        {
            Square = 0,
            [InspectorName("1x1 Slope")] Slope_1x1 = 10,
            [InspectorName("2x1 Slope Bottom")] Slope_2x1_Bottom = 11,
            [InspectorName("2x1 Slope Top")] Slope_2x1_Top = 12,
        }

        public static List<Vector3Int> DefaultNeighborPositions()
        {
            return new List<Vector3Int>()
                    {
                        new Vector3Int(-1, 1, 0),
                        new Vector3Int(0, 1, 0),
                        new Vector3Int(1, 1, 0),
                        new Vector3Int(-1, 0, 0),
                        new Vector3Int(1, 0, 0),
                        new Vector3Int(-1, -1, 0),
                        new Vector3Int(0, -1, 0),
                        new Vector3Int(1, -1, 0),
                    };
        }
        public Vector2Int EditorToUnityCoord(Vector2Int neighborCoord) => Functions.TransformPoint(neighborCoord, new Vector2Int(1, 0), new Vector2Int(0, -1));
        #endregion
        #endregion

#if UNITY_EDITOR
        #region Set methods
        public void SetSprite(Vector2Int p, Sprite s)
        {
            RecordObject($"Added sprite ({this.name})");
            bool n = false;

            //get or create cell
            GridCell cell = _grid.Find(t => t.Position == p);
            if (cell == null)
            {
                cell = new GridCell(p);
                n = true;
            }

            //set sprite
            if (cell.Sprite == s) return;
            cell.Sprite = s;

            //add cell
            if (n) _grid.Add(cell);

            SaveAsset();
        }
        public void SetTile(Vector2Int p, int id)
        {
            if (id < -10) return;

            RecordObject($"Set tile ({this.name})");
            bool n = false;

            //get or create cell
            GridCell cell = _grid.Find(t => t.Position == p);
            if (cell == null)
            {
                cell = new GridCell(p);
                n = true;
            }

            //set tile
            if (cell.TileID == id) return;
            cell.TileID = id;

            //add cell
            if (n) _grid.Add(cell);

            SaveAsset();
        }

        public void RemoveSprite(Vector2Int p)
        {
            RecordObject($"Deleted sprite ({this.name})");

            GridCell cell = _grid.Find(t => t.Position == p);
            if (cell != null)
            {
                cell.Sprite = null;

                if (cell.Sprite == null && cell.TileID == 0) _grid.Remove(cell);

                SaveAsset();
            }
        }
        public void RemoveTile(Vector2Int p)
        {
            RecordObject($"Deleted tile ({this.name})");

            GridCell cell = _grid.Find(t => t.Position == p);
            if (cell != null)
            {
                cell.TileID = 0;

                if (cell.Sprite == null && cell.TileID == 0) _grid.Remove(cell);

                SaveAsset();
            }
        }

        public void CreateTile()
        {
            RecordObject($"Created new tile ({this.name})");

            //get an id which is not used yet
            int tileId = 1;
            while (_tiles.Exists(t => t.UniqueID == tileId)) tileId++;

            _tiles.Add(new TileData("New Tile", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), tileId));

            _tempTex = null;

            SaveAsset();
        }

        public void ModifyTile(int index, string name, Color color, Texture2D texture, Sprite sprite, Tile.ColliderType collider, GameObject gameObject, TileShape shape, bool mirrorX, bool mirrorY)
        {
            RecordObject($"Modified tile ({this.name})");

            Tiles[index].Name = name;
            Tiles[index].Color = color;
            Tiles[index].Texture = texture;
            Tiles[index].DefaultSprite = sprite;
            Tiles[index].ColliderType = collider;
            Tiles[index].DefaultGameObject = gameObject;
            Tiles[index].TextureShape = shape;
            Tiles[index].MirrorTexX = mirrorX;
            Tiles[index].MirrorTexY = mirrorY;

            _tempTex = null;
            SaveAsset();
        }
        public void DeleteTile(TileData tile)
        {
            RecordObject($"Deleted tile ({this.name})");
            //Debug.Log($"Deleting tile: {tile.Name}");

            Tiles.Remove(tile);

            _tempTex = null;
            SaveAsset();
        }

        public void MoveArea(Rect area, Vector2Int moveBy)
        {
            RecordObject($"Moved selection ({this.name})");

            //get tiles in the area
            var tiles = _grid.Where(t => area.Contains(t.Position)).ToArray();
            //remove tiles temporarily
            _grid.RemoveAll(t => area.Contains(t.Position));

            //change their position
            for (int i = 0; i < tiles.Length; i++) tiles[i].Position += moveBy;

            //put them back
            for (int i = 0; i < tiles.Length; i++)
            {
                var cell = _grid.Find(t => t.Position == tiles[i].Position);

                if (cell != null) cell = tiles[i];
                else _grid.Add(tiles[i]);
            }

            //save
            SaveAsset();
        }
        public void DeleteArea(Rect area)
        {
            RecordObject($"Deleted selection ({this.name})");

            //remove
            _grid.RemoveAll(t => area.Contains(t.Position));

            //save
            SaveAsset();
        }
        #endregion

        #region Get methods
        public string GetTileName(int i) => GetTileNames()[i];
        public string[] GetTileNames()
        {
            string[] names = new string[_tiles.Count];

            for (int i = 0; i < _tiles.Count; i++)
            {
                names[i] = _tiles[i].Name;
            }

            return names;
        }
        public string GetUniqueTileName(int id) => _tiles.Find(t => t.UniqueID == id).Name;
        public Color GetTileColor(int i) => GetTileColors()[i];
        public Color[] GetTileColors()
        {
            Color[] colors = new Color[_tiles.Count];

            for (int i = 0; i < _tiles.Count; i++)
            {
                colors[i] = _tiles[i].Color;
            }

            return colors;
        }


        public Texture2D[] GetTileTextures()
        {
            CheckTextures();
            return _tempTex;
        }
        public Texture2D GetTileTexture(int i) => GetTileTextures()[i];

        private Texture2D[] _tempTex = null;
        public void SetTileTextures()
        {
            Texture2D[] textures = new Texture2D[_tiles.Count];

            for (int i = 0; i < _tiles.Count; i++)
            {
                //set tile texture
                if (_tiles[i].Texture != null) textures[i] = _tiles[i].Texture;
                //else 
                else
                {
                    switch (_tiles[i].TextureShape)
                    {
                        case TileShape.Square: textures[i] = Functions.CreateFilledTexture(_tiles[i].Color, 64, 64); break;
                        case TileShape.Slope_1x1: textures[i] = Functions.CreateSlopeTexture(_tiles[i].Color, 1, 0, 64, 64); break;
                        case TileShape.Slope_2x1_Bottom: textures[i] = Functions.CreateSlopeTexture(_tiles[i].Color, .5f, 0, 64, 64); break;
                        case TileShape.Slope_2x1_Top: textures[i] = Functions.CreateSlopeTexture(_tiles[i].Color, 1, .5f, 64, 64); break;
                    }
                    
                    //mirror if needed
                    textures[i] = Functions.MirrorTexture(textures[i], _tiles[i].MirrorTexX, _tiles[i].MirrorTexY);
                }
            }

            _tempTex = textures;

        }
        void CheckTextures()
        {
            if (_tempTex == null || (_tempTex.Length != _tiles.Count)) SetTileTextures();
        }

        public Texture2D GetTileTex(int id)
        {
            var tile = _tiles.Find(t => t.UniqueID == id);

            if (tile == null) return new Texture2D(1, 1);
            else return GetTileTexture(_tiles.IndexOf(tile));
        }
        public bool DoesTileExist(int id) => (_tiles.Exists(t => t.UniqueID == id) || (id < -1 && id > -5));
        #endregion

        #region UnityEditor
        void SaveAsset() => EditorUtility.SetDirty(this);
        public void RecordObject(string action)
        {
            //Debug.Log("Record");
            Undo.RecordObject(this, action);
        }

        public void CreateNewTileObject() => CreateNewTileObject("RuleTile");
        public void CreateNewTileObject(string name)
        {
            RecordObject($"Created tile objects ({this.name})");

            BetterRuleTile obj = ScriptableObject.CreateInstance<BetterRuleTile>();
            obj.name = name;

            _tileObjects.Add(obj);

            AssetDatabase.AddObjectToAsset(obj, this);
            AssetDatabase.SaveAssets();

            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(obj);
        }
        public void SaveObjectToAsset(BetterRuleTile tile)
        {
            RecordObject($"Created better rule tile ({this.name})");

            if (!_tileObjects.Exists(t => t.UniqueID == tile.UniqueID))
            {
                _tileObjects.Add(tile);
                AssetDatabase.AddObjectToAsset(tile, this);
            }

            AssetDatabase.SaveAssets();

            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(tile);
        }

        public void DeleteAllTileObjects()
        {
            RecordObject($"Deleted all child tiles ({this.name})");

            for (int i = _tileObjects.Count - 1; i >= 0; i--)
            {
                DeleteTileObject(_tileObjects[i], false);
            }

            AssetDatabase.SaveAssets();
        }
        public void DeleteUnusedTileObjects()
        {
            RecordObject($"Deleted unused objects ({this.name})");

            for (int i = _tileObjects.Count - 1; i >= 0; i--)
            {
                if (!_tiles.Exists(t => t.UniqueID == _tileObjects[i].UniqueID)) DeleteTileObject(_tileObjects[i]);
            }
        }
        void DeleteTileObject(BetterRuleTile tile, bool save = true)
        {
            _tileObjects.Remove(tile);
            Undo.DestroyObjectImmediate(tile);

            if (save) AssetDatabase.SaveAssets();
        }


        [ContextMenu("Delete All Objects")]
        public void DeleteAllNestedObjects()
        {
            var objs = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this));

            foreach (var item in objs)
            {
                if (item != null && item is not BetterRuleTileContainer) Undo.DestroyObjectImmediate(item);
            }

            _tileObjects.Clear();

            AssetDatabase.SaveAssets();
        }
        #endregion
#endif
    }
}
