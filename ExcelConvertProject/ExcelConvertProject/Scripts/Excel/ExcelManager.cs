using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace ExcelConvertProject.Scripts.Excel
{
    using Excel = Microsoft.Office.Interop.Excel;

    public class ExcelManager
    {
        // 1. 경로명 저장하는거 추가
        // 2. 배열로 생성할 것
        // 3. 파일 로드 작업


        private static ExcelManager _instance = new ExcelManager();
        public static ExcelManager Instance { get { return _instance; } }

        // excel data
        private string className;
        private string savepath;
        private string unityReadPath;

        List<string> dataName = new List<string>();
        List<string> typeData = new List<string>();
        Dictionary<int, List<string>> datainfo = new Dictionary<int, List<string>>();

        private void Clear()
        {
            dataName.Clear();
            typeData.Clear();
            foreach(KeyValuePair<int, List<string>>keydata in datainfo)
                keydata.Value.Clear();
            datainfo.Clear();
        }

        // file read
        public void ReadExcel(string path)
        {
            // path는 Excel파일의 전체 경로입니다.
            // 예. D:\test\test.xslx
            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;
            try
            {
                Clear();                
                excelApp = new Excel.Application();
                wb = excelApp.Workbooks.Add(path);
                // path 대신 문자열도 가능합니다
                // 예. Open(@"D:\test\test.xslx");
                
                ws = wb.Worksheets.get_Item(1) as Excel.Worksheet;

                savepath = path;
                className = ws.Name;

                // 첫번째 Worksheet를 선택합니다.
                Excel.Range rng = ws.UsedRange;   // '여기'
                                                  // 현재 Worksheet에서 사용된 셀 전체를 선택합니다.
                                                  
                object[,] data = rng.Value;
                

                // path line
                unityReadPath = data[1, 2].ToString();                                    
                                

                for (int r = 2; r <= data.GetLength(0); r++)
                {
                    for (int c = 1; c <= data.GetLength(1); c++)
                    {
                        if (data[r, c] == null)
                            continue;

                        string exceldata = data[r, c].ToString();

                        if (r == 2)     // 변수 이름
                        {
                            // data name
                            dataName.Add(exceldata);
                        }
                        else if( r == 3) // 타입 이름
                        {
                            typeData.Add(exceldata);
                        }
                        else
                        {
                            if (datainfo.ContainsKey(r) == false)
                            {
                                datainfo.Add(r, new List<string>());
                                datainfo[r].Add(exceldata);
                            }
                            else
                                datainfo[r].Add(exceldata);
                        }

                        exceldata = string.Empty;
                        // Data 빼오기
                        // data[r, c] 는 excel의 (r, c) 셀 입니다.
                        // data.GetLength(0)은 엑셀에서 사용되는 행의 수를 가져오는 것이고,
                        // data.GetLength(1)은 엑셀에서 사용되는 열의 수를 가져오는 것입니다.
                        // GetLength와 [ r, c] 의 순서를 바꿔서 사용할 수 있습니다.
                    }
                }
                ReleaseExcelObject(rng);
                
                wb.Close(0);
                excelApp.Quit();

            }
            catch (Exception ex)
            {
                ReleaseExcelObject(ws);
                ReleaseExcelObject(wb);
                ReleaseExcelObject(excelApp);
                throw ex;
            }
            finally
            {                
                ReleaseExcelObject(ws);
                ReleaseExcelObject(wb);
                ReleaseExcelObject(excelApp);
                System.Windows.Forms.MessageBox.Show("File Load Completed");
            }
        }

        static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        
      
        // create *.cs, *.byte (읽을 파일 + cs파일)
        public void CreateFile()
        {
            if (string.IsNullOrEmpty(savepath))
            {
                System.Windows.Forms.MessageBox.Show("Fail Save");
                return;
            }

            StringBuilder builder = new StringBuilder();
            int length = savepath.LastIndexOf('\\');
            string path = savepath.Remove(length, savepath.Length - length);
            string savefile = string.Format(@"{0}\{1}.cs", path, className);

            try
            {
                builder.AppendLine(string.Format("using System;"));
                builder.AppendLine(string.Format("using UnityEngine;"));
                builder.AppendLine(string.Format("using System.IO;"));
                builder.AppendLine(string.Format("using System.Collections.Generic;"));
                builder.AppendLine();
                builder.AppendLine();

                string structdata = string.Format("{0}Struct", className);
                // struct 
                builder.Append(string.Format("public struct {0}", structdata));                
                builder.AppendLine("{");

                for (int i = 0; i < typeData.Count; i++)
                {
                    builder.AppendLine(string.Format("\t public {0} {1};", typeData[i], dataName[i]));                    
                }                
                builder.AppendLine("}");

                // class
                builder.AppendLine();
                builder.AppendLine(string.Format("public partial class {0}", className));
                builder.AppendLine("{");

                builder.AppendLine(string.Format("\t\t public {0}()", className));
                builder.AppendLine("\t\t {");

                builder.AppendLine(string.Format("\t\t\t TextAsset textasset = null;"));
                builder.AppendLine(string.Format("#if UNITY_EDITOR"));
                builder.AppendLine(string.Format("\t\t\t textasset = Resources.Load(\"{0}/{1}\") as TextAsset;", unityReadPath, className));
                builder.AppendLine(string.Format("#else"));
                builder.AppendLine(string.Format("\t\t\t textasset = Resources.Load(\"{0}/{1}\") as TextAsset; // RM: will change asset bundle", unityReadPath, className));
                builder.AppendLine(string.Format("#endif"));

                builder.AppendLine(string.Format("\t\t\t if (textasset == null) \n\t\t\t {{\n\t\t\t\t Debug.Log(\"No Table Data {0} \"); \n\t\t\t\t return;\n\t\t\t }}", className));
                builder.AppendLine(string.Format("\t\t\t Stream s = new MemoryStream(textasset.bytes);"));
                builder.AppendLine(string.Format("\t\t\t BinaryReader br = new BinaryReader(s);"));


                builder.AppendLine(string.Format("\t\t\t for(int i = 0; i < _data.Length; i++)"));
                builder.AppendLine(string.Format("\t\t\t {{"));                

                builder.AppendLine(string.Format("\t\t\t\t {0} tempdata = new {0}();", structdata));
                for (int i = 0; i < typeData.Count; i++)
                    builder.AppendLine(string.Format("\t\t\t\t tempdata.{0} = {1};", dataName[i], GetReadTypeString(typeData[i])));

                builder.AppendLine(string.Format("\t\t\t\t _data[i] = tempdata;"));
                builder.AppendLine(string.Format("\t\t\t }}"));

                builder.AppendLine(string.Format("\t\t\t br.Close();"));
                builder.AppendLine(string.Format("\t\t\t s.Close();"));                
                builder.AppendLine("\t\t }");

                builder.AppendLine();
                builder.AppendLine(string.Format("\t\t private static {0} _instance = new {0}();", className));
                builder.AppendLine(string.Format("\t\t public static {0} Instance {1} get {1} return _instance; {2} {2}", className, '{', '}'));

                builder.AppendLine();
                builder.AppendLine(string.Format("\t\t private {0}[] _data = new {0}[{1}];", structdata, datainfo.Count));

                builder.AppendLine(string.Format("\t\t public {0} GetData(int index) {{", structdata));
                builder.AppendLine(string.Format("\t\t\t if(_data.Length <= index) {{"));                
                builder.AppendLine(string.Format("\t\t\t\t Debug.LogError(\"NO DATA = INDEX - \" + index.ToString());"));
                builder.AppendLine(string.Format("\t\t\t\t return new {0}();", structdata));
                builder.AppendLine(string.Format("\t\t\t }}"));
                builder.AppendLine(string.Format("\t\t\t return _data[index];"));
                builder.AppendLine("\t\t }");


                builder.AppendLine("}");                
                
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Error CS Save");
            }
            finally
            {                
                System.IO.File.WriteAllText(savefile, builder.ToString());
                CreateByteFile();
                Clear();
            }
            
        }

        // create byte
        private void CreateByteFile()
        {
            if (string.IsNullOrEmpty(savepath))
            {
                
                System.Windows.Forms.MessageBox.Show("Error No Path");
                return;
            }

            int length = savepath.LastIndexOf('\\');
            string path = savepath.Remove(length, savepath.Length - length);
            string savefile = string.Format(@"{0}\{1}.bytes", path, className);
            
            try
            {
                using (FileStream fs = new FileStream(savefile, System.IO.FileMode.OpenOrCreate))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        foreach (KeyValuePair<int, List<string>> keydata in datainfo)
                        {
                            // count
                            //bw.Write(keydata.Value.Count);

                            for (int i = 0; i < keydata.Value.Count; i++)
                            {
                                string dataType = typeData[i];
                                string dataValue = keydata.Value[i];

                                byte[] savedata = GetTypeValue(dataType, dataValue);

                                // save
                                bw.Write(savedata);
                            }
                        }
                    }
                }                    
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Error byte Save");
            }
            finally
            {
                System.Windows.Forms.MessageBox.Show("Complte *.cs and *.byte File");
            }
        }

        // type to byte
        private static byte[] GetTypeValue(string type, string value)
        {
            byte[] byteparsedata = null;
            if (type == "int")
            {
                int parsedata = int.Parse(value);
                byteparsedata = BitConverter.GetBytes(parsedata);
            }
            else if (type == "uint")
            {
                uint parsedata = uint.Parse(value);
                byteparsedata = BitConverter.GetBytes(parsedata);
            }
            else if (type == "float")
            {
                float parsedata = float.Parse(value);
                byteparsedata = BitConverter.GetBytes(parsedata);
            }
            else if (type == "byte")
            {
                byte parsedata = byte.Parse(value);
                byteparsedata = BitConverter.GetBytes(parsedata);
            }
            else if(type == "long")
            {
                long parsedata = long.Parse(value);
                byteparsedata = BitConverter.GetBytes(parsedata);
            }
            else if (type == "ulong")
            {
                ulong parsedata = ulong.Parse(value);
                byteparsedata = BitConverter.GetBytes(parsedata);
            }
            else if(type == "string")
            {
                byteparsedata = Encoding.UTF8.GetBytes(value);                
            }

            return byteparsedata;
        }

        private static string GetReadTypeString(string type)
        {
            if (type == "int")
            {
                return "br.ReadInt32()";
            }
            else if (type == "uint")
            {
                return "br.ReadUInt32()";
            }
            else if (type == "float")
            {
                return "br.ReadSingle()";
            }
            else if (type == "byte")
            {
                return "br.ReadByte()";
            }
            else if (type == "long")
            {
                return "br.ReadInt64()";
            }
            else if (type == "ulong")
            {
                return "br.ReadUInt64()";
            }
            else if (type == "string")
            {
                return "br.ReadString()";
            }

            return string.Empty;
        }
    }
    
}
