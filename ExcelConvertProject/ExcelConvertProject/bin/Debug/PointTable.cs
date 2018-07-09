using System;
using UnityEngine;
using System.IO;
using System.Collections.Generic;


public struct PointTableStruct{
	 public int easy;
	 public int normal;
	 public int hard;
}

public partial class PointTable
{
		 public PointTable()
		 {
			 TextAsset textasset = null;
#if UNITY_EDITOR
			 textasset = Resources.Load("table/PointTable") as TextAsset;
#else
			 textasset = Resources.Load("table/PointTable") as TextAsset; // RM: will change asset bundle
#endif
			 if (textasset == null) 
			 {
				 Debug.Log("No Table Data PointTable "); 
				 return;
			 }
			 Stream s = new MemoryStream(textasset.bytes);
			 BinaryReader br = new BinaryReader(s);
			 for(int i = 0; i < _data.Length; i++)
			 {
				 PointTableStruct tempdata = new PointTableStruct();
				 tempdata.easy = br.ReadInt32();
				 tempdata.normal = br.ReadInt32();
				 tempdata.hard = br.ReadInt32();
				 _data[i] = tempdata;
			 }
			 br.Close();
			 s.Close();
		 }

		 private static PointTable _instance = new PointTable();
		 public static PointTable Instance { get { return _instance; } }

		 private PointTableStruct[] _data = new PointTableStruct[1];
		 public PointTableStruct GetData(int index) {
			 if(_data.Length <= index) {
				 Debug.LogError("NO DATA = INDEX - " + index.ToString());
				 return new PointTableStruct();
			 }
			 return _data[index];
		 }
}
