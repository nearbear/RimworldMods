using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace RunescapeLevelUp.Animation
{
    public class Graphic_LevelUp : Graphic_Collection
    {
        private readonly int ticksPerFrame = 3;

        private int currentFrame = -1;
        private int ticksPrev = 0;

        public void Play()
        {
            currentFrame = 0;
        }

        public override Material MatSingle
        {
            get
            {
                return subGraphics[currentFrame].MatSingle;
            }
		}

        public void Init()
        {
            Init(new GraphicRequest(typeof(Graphic_LevelUp), "Things/LevelUp", ShaderDatabase.TransparentPostLight, Vector2.one, Color.white, Color.white, null, 0, null, null));
        }

		public override void DrawWorker(Vector3 loc, Rot4 rot, ThingDef thingDef, Thing thing, float extraRotation)
        {
            if (thingDef == null)
            {
                Mod.Log("Graphic_Animated with null thingDef");
                return;
            }
            if (subGraphics == null || subGraphics.Length == 0)
            {
                Mod.Log("Graphic_Animated has no subgraphics");
                return;
            }
            if (currentFrame == -1 || currentFrame >= subGraphics.Length - 1)
            {
                return;
            }
            int ticksCurrent = Find.TickManager.TicksGame;
            if (ticksCurrent >= ticksPrev + ticksPerFrame)
            {
                ticksPrev = ticksCurrent;
                currentFrame++;
            }

            Vector2 size = new Vector2(7f, 7f);

            Vector3 position = loc;
            position += new Vector3(-0.1f * size.x, 0f, 0f);

            Graphic graphic = subGraphics[currentFrame];
            Graphics.DrawMesh(MeshPool.GridPlane(size), position, Quaternion.identity, graphic.MatSingle, 0);
        }
    }
}
