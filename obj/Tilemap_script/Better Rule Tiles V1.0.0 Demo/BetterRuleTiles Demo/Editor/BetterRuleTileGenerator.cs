#if UNITY_EDITOR
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Tilemaps;
using VinTools.BetterRuleTiles;

namespace VinToolsEditor.BetterRuleTiles
{
    public class BetterRuleTileGenerator
    {
        public static void GenerateTiles(BetterRuleTileContainer container)
        {
            List<BetterRuleTile> tiles = new List<BetterRuleTile>();

            //generate tiles
            for (int i = 0; i < container.Tiles.Count; i++)
            {
                tiles.Add(GenerateTile(container, container.Tiles[i].UniqueID));
            }

            //assign the other tiles
            for (int i = 0; i < tiles.Count; i++)
            {
                //set tiles to their correct place
                foreach (var item in tiles) tiles[i].otherTiles[item.UniqueID - 1] = item;
            }

            //delete unused previous tiles 
            container.DeleteUnusedTileObjects();
            //create Tiles
            foreach (var tile in tiles) container.SaveObjectToAsset(tile);

            //create palette
            //CreatePalette(container);

            //highlight object
            Selection.activeObject = container;
            EditorGUIUtility.PingObject(container);
        }

        static BetterRuleTile GenerateTile(BetterRuleTileContainer container, int UniqueID)
        {
            BetterRuleTile tile = container._tileObjects.Find(t => t.UniqueID == UniqueID);
            if (tile == null) tile = ScriptableObject.CreateInstance<BetterRuleTile>();

            var templateTile = container.Tiles.Find(t => t.UniqueID == UniqueID);

            tile.name = templateTile.Name;
            tile.UniqueID = UniqueID;
            tile.otherTiles = new BetterRuleTile[container._tiles.Max(t => t.UniqueID)];
            tile.m_DefaultColliderType = templateTile.ColliderType;

            tile.m_TilingRules = GenerateRules(container, UniqueID);
            tile.m_DefaultSprite = templateTile.DefaultSprite;
            tile.m_DefaultGameObject = templateTile.DefaultGameObject;

            return tile;
        }
        static List<RuleTile.TilingRule> GenerateRules(BetterRuleTileContainer container, int UniqueID)
        {
            //create new tiling rule list
            List<RuleTile.TilingRule> tilingRules = new List<RuleTile.TilingRule>();

            //find the tile object
            var tileOf = container.Tiles.Find(t => t.UniqueID == UniqueID);

            foreach (var item in container.Grid.FindAll(t => t.TileID == UniqueID))
            {
                //ignore tile if has no sprite
                if (item.Sprite == null) continue;
                item.NeighborPositions = item.NeighborPositions.OrderBy(t => t.y * -10000 + t.x).ToList();
                List<Vector3Int> NeighborPositions = new List<Vector3Int>();
                foreach (var neighbor in item.NeighborPositions) NeighborPositions.Add((Vector3Int)container.EditorToUnityCoord(new Vector2Int(neighbor.x, neighbor.y)));   

                //create tiling
                int[] neighbors = new int[NeighborPositions.Count];

                //set neighbors
                for (int i = 0; i < neighbors.Length; i++)
                {
                    //neighbors[i] = GetNeighborRule(container, container.Grid.Find(t => t.Position == new Vector2Int(item.Position.x + NeighborPositions[i].x, item.Position.y - NeighborPositions[i].y)), UniqueID);
                    neighbors[i] = GetNeighborRule(container, container.Grid.Find(t => t.Position == item.Position + container.EditorToUnityCoord((Vector2Int)NeighborPositions[i])), UniqueID);
                }

                //create sprites array
                List<Sprite> sprites = new List<Sprite>();
                if (item.OutputSprite == RuleTile.TilingRuleOutput.OutputSprite.Single || item.IncludeSpriteInOutput || item.Sprites.Length <= 0) sprites.Add(item.Sprite);
                if (item.OutputSprite != RuleTile.TilingRuleOutput.OutputSprite.Single) sprites.AddRange(item.Sprites);

                //create tiling rule
                RuleTile.TilingRule tilingRule = new RuleTile.TilingRule();
                tilingRule.m_NeighborPositions = NeighborPositions;
                tilingRule.m_Neighbors = neighbors.ToList();
                tilingRule.m_Sprites = sprites.ToArray();
                tilingRule.m_ColliderType = item.UseDefaultSettings ? tileOf.ColliderType : item.ColliderType;
                tilingRule.m_GameObject = item.UseDefaultSettings ? tileOf.DefaultGameObject : item.GameObject;
                tilingRule.m_Output = item.OutputSprite;
                tilingRule.m_RuleTransform = item.Transform;
                if (item.OutputSprite == RuleTile.TilingRuleOutput.OutputSprite.Random)
                {
                    tilingRule.m_PerlinScale = item.NoiseScale;
                    tilingRule.m_RandomTransform = item.RandomTransform;
                }
                if (item.OutputSprite == RuleTile.TilingRuleOutput.OutputSprite.Animation)
                {
                    tilingRule.m_MaxAnimationSpeed = item.MaxAnimationSpeed;
                    tilingRule.m_MinAnimationSpeed = item.MinAnimationSpeed;
                }
                //tilingRule.m_Id

                //add tiling rule to list
                tilingRules.Add(tilingRule);
            }

            //return list
            tilingRules = RemoveDuplicates(tilingRules);
            tilingRules = SortRules(tilingRules);
            return tilingRules;
        }
        static List<RuleTile.TilingRule> RemoveDuplicates(List<RuleTile.TilingRule> tilingRules)
        {
            //for loop, but lets me remove things from the list
            int i = 0;
            while (i < tilingRules.Count)
            {
                for (int b = tilingRules.Count - 1; b > i; b--)
                {
                    if (CheckSame(tilingRules[i], tilingRules[b])) tilingRules.RemoveAt(b);
                }

                //increase index
                i++;
            }

            //return
            return tilingRules;
        }

        static bool CheckSame(RuleTile.TilingRule tr1, RuleTile.TilingRule tr2)
        {
            if (tr1.m_Neighbors.Count != tr2.m_Neighbors.Count) return false;
            if (tr1.m_NeighborPositions.Count != tr2.m_NeighborPositions.Count) return false;
            if (tr1.m_Sprites.Length != tr2.m_Sprites.Length) return false;

            for (int i = 0; i < tr1.m_Neighbors.Count; i++) if (tr1.m_Neighbors[i] != tr2.m_Neighbors[i]) return false;
            for (int i = 0; i < tr1.m_NeighborPositions.Count; i++) if (tr1.m_NeighborPositions[i] != tr2.m_NeighborPositions[i]) return false;
            for (int i = 0; i < tr1.m_Sprites.Length; i++) if (tr1.m_Sprites[i] != tr2.m_Sprites[i]) return false;

            return true;
        }
        static List<RuleTile.TilingRule> SortRules(List<RuleTile.TilingRule> tilingRules) => tilingRules.OrderByDescending(t => GetNumberOfNeighbors(t)).ToList();
        static int GetNumberOfNeighbors(RuleTile.TilingRule tr)
        {
            int num = 0;
            for (int i = 0; i < tr.m_Neighbors.Count; i++)
            {
                if (tr.m_Neighbors[i] != BetterRuleTile.Neighbor.Ignore) num++;
                if (tr.m_Neighbors[i] > 0) num++;
            }
            return num;
        }
        static int GetNeighborRule(BetterRuleTileContainer container, BetterRuleTileContainer.GridCell cell, int TileID)
        {
            if (cell == null) return BetterRuleTile.Neighbor.Ignore;
            if (cell.TileID == TileID) return BetterRuleTile.Neighbor.This;
            if (cell.TileID == -3) return BetterRuleTile.Neighbor.NotThis;
            if (cell.TileID == -4) return BetterRuleTile.Neighbor.Any;
            if (cell.TileID == -2) return BetterRuleTile.Neighbor.Empty;

            if (container.Tiles.Exists(t => t.UniqueID == cell.TileID)) return cell.TileID;

            return BetterRuleTile.Neighbor.Ignore;
        }
    }
}

#endif