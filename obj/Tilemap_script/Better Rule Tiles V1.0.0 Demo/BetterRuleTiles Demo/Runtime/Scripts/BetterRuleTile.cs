using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

namespace VinTools.BetterRuleTiles
{
    [CreateAssetMenu(menuName = "VinTools/Custom Tiles/Better Rule Tile")]
    public class BetterRuleTile : RuleTile<BetterRuleTile.Neighbor>
    {
        public int UniqueID;
        public BetterRuleTile[] otherTiles;

        [Tooltip("Displays a logwarning when a property was not found with the desired key")]
        public bool DebugMode = false;

        public class Neighbor : RuleTile.TilingRule.Neighbor
        {
            new public const int This = 0;
            public const int Ignore = -1;
            public const int Empty = -2;
            new public const int NotThis = -3;
            public const int Any = -4;
        }

        public override bool RuleMatch(int neighbor, TileBase tile)
        {
            if (tile is RuleOverrideTile ot)
                tile = ot.m_InstanceTile;

            switch (neighbor)
            {
                case Neighbor.This: return tile == this;
                case Neighbor.NotThis: return tile != this;
                case Neighbor.Any: return tile != null;
                case Neighbor.Empty: return tile == null;
                case Neighbor.Ignore: return true;
                default:
                    if (neighbor > 0) return tile == otherTiles[neighbor - 1];
                    break;
            }
            return true;
        }
    }
}