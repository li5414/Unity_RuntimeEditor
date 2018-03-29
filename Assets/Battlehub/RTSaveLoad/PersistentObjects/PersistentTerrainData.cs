#define RT_USE_PROTOBUF
#if RT_USE_PROTOBUF
using ProtoBuf;
#endif
/*This is auto-generated file. Tools->Runtime SaveLoad->Create Persistent Objects 
If you want prevent overwriting, drag this file to another folder.*/

namespace Battlehub.RTSaveLoad.PersistentObjects
{
	#if RT_USE_PROTOBUF
	[ProtoContract(AsReferenceDefault = true, ImplicitFields = ImplicitFields.AllFields)]
	#endif
	[System.Serializable]
	public class PersistentTerrainData : Battlehub.RTSaveLoad.PersistentObjects.PersistentObject
	{
		public override object WriteTo(object obj, System.Collections.Generic.Dictionary<long, UnityEngine.Object> objects)
		{
			obj = base.WriteTo(obj, objects);
			if(obj == null)
			{
				return null;
			}
			UnityEngine.TerrainData o = (UnityEngine.TerrainData)obj;
			o.heightmapResolution = heightmapResolution;
			o.size = size;
			o.thickness = thickness;
			o.wavingGrassStrength = wavingGrassStrength;
			o.wavingGrassAmount = wavingGrassAmount;
			o.wavingGrassSpeed = wavingGrassSpeed;
			o.wavingGrassTint = wavingGrassTint;
			o.detailPrototypes = Write(o.detailPrototypes, detailPrototypes, objects);
			o.treeInstances = treeInstances;
			o.treePrototypes = Write(o.treePrototypes, treePrototypes, objects);
			o.alphamapResolution = alphamapResolution;
			o.baseMapResolution = baseMapResolution;
			o.splatPrototypes = Write(o.splatPrototypes, splatPrototypes, objects);
			return o;
		}

		public override void ReadFrom(object obj)
		{
			base.ReadFrom(obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.TerrainData o = (UnityEngine.TerrainData)obj;
			heightmapResolution = o.heightmapResolution;
			size = o.size;
			thickness = o.thickness;
			wavingGrassStrength = o.wavingGrassStrength;
			wavingGrassAmount = o.wavingGrassAmount;
			wavingGrassSpeed = o.wavingGrassSpeed;
			wavingGrassTint = o.wavingGrassTint;
			detailPrototypes = Read(detailPrototypes, o.detailPrototypes);
			treeInstances = o.treeInstances;
			treePrototypes = Read(treePrototypes, o.treePrototypes);
			alphamapResolution = o.alphamapResolution;
			baseMapResolution = o.baseMapResolution;
			splatPrototypes = Read(splatPrototypes, o.splatPrototypes);
		}

		public override void FindDependencies<T>(System.Collections.Generic.Dictionary<long, T> dependencies, System.Collections.Generic.Dictionary<long, T> objects, bool allowNulls)
		{
			base.FindDependencies(dependencies, objects, allowNulls);
			FindDependencies(detailPrototypes, dependencies, objects, allowNulls);
			FindDependencies(treePrototypes, dependencies, objects, allowNulls);
			FindDependencies(splatPrototypes, dependencies, objects, allowNulls);
		}

		protected override void GetDependencies(System.Collections.Generic.Dictionary<long, UnityEngine.Object> dependencies, object obj)
		{
			base.GetDependencies(dependencies, obj);
			if(obj == null)
			{
				return;
			}
			UnityEngine.TerrainData o = (UnityEngine.TerrainData)obj;
			GetDependencies(detailPrototypes, o.detailPrototypes, dependencies);
			GetDependencies(treePrototypes, o.treePrototypes, dependencies);
			GetDependencies(splatPrototypes, o.splatPrototypes, dependencies);
		}

		public int heightmapResolution;

		public UnityEngine.Vector3 size;

		public float thickness;

		public float wavingGrassStrength;

		public float wavingGrassAmount;

		public float wavingGrassSpeed;

		public UnityEngine.Color wavingGrassTint;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentDetailPrototype[] detailPrototypes;

		public UnityEngine.TreeInstance[] treeInstances;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentTreePrototype[] treePrototypes;

		public int alphamapResolution;

		public int baseMapResolution;

		public Battlehub.RTSaveLoad.PersistentObjects.PersistentSplatPrototype[] splatPrototypes;

	}
}
