using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Reflection;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using UnityEditor;
using UnityEngine;

namespace SoulRunProject
{
    public class ExcelAssetMenu
    {
        private static UnityEngine.Object[] _selectedAssets;
        private static string _objectPath;
        private static ScriptableObject _object; 
        
        /// <summary>
        /// 実行可能でないなら、無効にする
        /// </summary>
        /// <returns></returns>
        [MenuItem("Assets/MyCustom/ScriptableObjectToExcel", validate = true, priority = 1)]
        static bool Validate()
        {
            _selectedAssets = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);
            if(_selectedAssets.Length != 1) return false;
            _objectPath = AssetDatabase.GetAssetPath(_selectedAssets[0]);
            _object = AssetDatabase.LoadAssetAtPath<ScriptableObject>(_objectPath);
            if (_object == null) return false;
            return  _object.GetType().IsDefined(typeof(ExcelAssetAttribute));
        }
        
        /// <summary>
        /// menuから実行されるクラス
        /// </summary>
        [MenuItem("Assets/MyCustom/ScriptableObjectToExcel" , priority = 1)]
        static void CreateExcel()
        {
            var path = Path.GetDirectoryName(_objectPath);
            var assetFields = _object.GetType().GetFields( );
            
            IWorkbook book = null;
            book = new XSSFWorkbook();
            var excelPath = Path.ChangeExtension(Path.Combine(path , _object.GetType().Name ), "xlsx");
            
            if (File.Exists(excelPath))
            {
                Debug.LogError("すでにExcelファイルが存在しています");
                return;
            }
            
            foreach (var assetField in assetFields)
            {
                Type fieldType = assetField.FieldType;
                if(! fieldType.IsGenericType || (fieldType.GetGenericTypeDefinition() != typeof(List<>))) continue;
                
                //作成したいオブジェクト型を持ってくる List<T>　のTを探している
                Type[] types = fieldType.GetGenericArguments();
                Type entityType = types[0];
                var sheet = book.CreateSheet( assetField.Name );
                IRow row = sheet.CreateRow(0);
                var entityFields = entityType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                for (int i = 0; i < entityFields.Length; i++)
                {
                    if (entityFields[i].GetCustomAttributes(typeof(SerializeField), false).Length == 0) continue;
                    row.CreateCell(i).SetCellValue(entityFields[i].Name);
                }
            }
            using(FileStream stream = File.Create(excelPath))
            {
                book.Write( stream );
            }
            book.Close();
            AssetDatabase.Refresh();
        }
        

    }
    
    
}
