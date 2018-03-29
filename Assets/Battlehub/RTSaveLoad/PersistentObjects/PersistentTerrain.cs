#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
using Battlehub.RTSaveLoad;
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentTerrain : Battlehub.RTSaveLoad.PersistentObjects.PersistentBehaviour
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.Terrain o = (UnityEngine.Terrain)obj;
			o.terrainData = (UnityEngine.TerrainData)objects.Get(terrainData);
			o.treeDistance = treeDistance;
			o.treeBillboardDistance = treeBillboardDistance;
			o.treeCrossFadeLength = treeCrossFadeLength;
			o.treeMaximumFullLODCount = treeMaximumFullLODCount;
			o.detailObjectDistance = detailObjectDistance;
			o.detailObjectDensity = detailObjectDensity;
			o.heightmapPixelError = heightmapPixelError;
			o.heightmapMaximumLOD = heightmapMaximumLOD;
			o.basemapDistance = basemapDistance;
			o.lightmapIndex = lightmapIndex;
			o.realtimeLightmapIndex = realtimeLightmapIndex;
			o.lightmapScaleOffset = lightmapScaleOffset;
			o.realtimeLightmapScaleOffset = realtimeLightmapScaleOffset;
			o.castShadows = castShadows;
			o.reflectionProbeUsage = (UnityEngine.Rendering.ReflectionProbeUsage)reflectionProbeUsage;
			o.materialType = (UnityEngine.Terrain.MaterialType)materialType;
			o.materialTemplate = (UnityEngine.Material)objects.Get(materialTemplate);
			o.legacySpecular = legacySpecular;
			o.legacyShininess = legacyShininess;
			o.drawHeightmap = drawHeightmap;
			o.drawTreesAndFoliage = drawTreesAndFoliage;
			o.treeLODBiasMultiplier = treeLODBiasMultiplier;
			o.collectDetailPatches = collectDetailPatches;
			o.editorRenderFlags = (UnityEngine.TerrainRenderFlags)editorRenderFlags;
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Terrain o = (UnityEngine.Terrain)obj;
			terrainData = o.terrainData.GetMappedInstanceID();
			treeDistance = o.treeDistance;
			treeBillboardDistance = o.treeBillboardDistance;
			treeCrossFadeLength = o.treeCrossFadeLength;
			treeMaximumFullLODCount = o.treeMaximumFullLODCount;
			detailObjectDistance = o.detailObjectDistance;
			detailObjectDensity = o.detailObjectDensity;
			heightmapPixelError = o.heightmapPixelError;
			heightmapMaximumLOD = o.heightmapMaximumLOD;
			basemapDistance = o.basemapDistance;
			lightmapIndex = o.lightmapIndex;
			realtimeLightmapIndex = o.realtimeLightmapIndex;
			lightmapScaleOffset = o.lightmapScaleOffset;
			realtimeLightmapScaleOffset = o.realtimeLightmapScaleOffset;
			castShadows = o.castShadows;
			reflectionProbeUsage = (uint)o.reflectionProbeUsage;
			materialType = (uint)o.materialType;
			materialTemplate = o.materialTemplate.GetMappedInstanceID();
			legacySpecular = o.legacySpecular;
			legacyShininess = o.legacyShininess;
			drawHeightmap = o.drawHeightmap;
			drawTreesAndFoliage = o.drawTreesAndFoliage;
			treeLODBiasMultiplier = o.treeLODBiasMultiplier;
			collectDetailPatches = o.collectDetailPatches;
			editorRenderFlags = (uint)o.editorRenderFlags;
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			AddDependency(terrainData, dependencies, objects, allowNulls);
			AddDependency(materialTemplate, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.Terrain o = (UnityEngine.Terrain)obj;
			AddDependency(o.terrainData, dependencies);
			AddDependency(o.materialTemplate, dependencies);
		}

		public long terrainData;

		public float treeDistance;

		public float treeBillboardDistance;

		public float treeCrossFadeLength;

		public int treeMaximumFullLODCount;

		public float detailObjectDistance;

		public float detailObjectDensity;

		public float heightmapPixelError;

		public int heightmapMaximumLOD;

		public float basemapDistance;

		public int lightmapIndex;

		public int realtimeLightmapIndex;

		public UnityEngine.Vector4 lightmapScaleOffset;

		public UnityEngine.Vector4 realtimeLightmapScaleOffset;

		public bool castShadows;

		public uint reflectionProbeUsage;

		public uint materialType;

		public long materialTemplate;

		public UnityEngine.Color legacySpecular;

		public float legacyShininess;

		public bool drawHeightmap;

		public bool drawTreesAndFoliage;

		public float treeLODBiasMultiplier;

		public bool collectDetailPatches;

		public uint editorRenderFlags;

	}
}
