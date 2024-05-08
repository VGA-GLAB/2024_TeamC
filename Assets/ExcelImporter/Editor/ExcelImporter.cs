using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

/// <summary>
/// AssetPostProcessorは、インポートしたパイプラインをフックし、アセットをインポートする前と後にスクリプトを実行することが可能になる
public class ExcelImporter : AssetPostprocessor
{

	/// <summary> アセットをインポート完了後に呼ばれる </summary>
	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		bool imported = false;
		foreach(string path in importedAssets)
		{
			if(Path.GetExtension(path) == ".xls" || Path.GetExtension(path) == ".xlsx") 
			{
				var types = FindAllExcelAssetTypes();
				var excelName = Path.GetFileNameWithoutExtension(path);
				if(excelName.StartsWith("~$")) continue;
				Type type = types.Find(i => i.Name == excelName);
				if(type == null) continue;
				ImportExcel(path, type);
				imported = true;
			}
		}
		if(imported) 
		{
			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}

	/// <summary> ExcelAsset属性のついたファイルを探す </summary>
	static List<Type> FindAllExcelAssetTypes()
	{
		var list = new List<Type>();
		foreach(var assembly in AppDomain.CurrentDomain.GetAssemblies())
		{
			foreach(var type in assembly.GetTypes())
			{
				if(type.IsDefined(typeof(ExcelAssetAttribute),false))
					list.Add(type);
			}
		}
		return list;
	}
	
	static void ImportExcel(string excelPath, Type type)
	{
		UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(Path.ChangeExtension(excelPath , ".asset")　, type);
		IWorkbook book = LoadBook(excelPath);

		var assetFields = type.GetFields();
		int sheetCount = 0;

		foreach (var assetField in assetFields)
		{
			ISheet sheet =  book.GetSheet(assetField.Name);
			if(sheet == null) continue;

			Type fieldType = assetField.FieldType;
			if(! fieldType.IsGenericType || (fieldType.GetGenericTypeDefinition() != typeof(List<>))) continue;

			Type[] types = fieldType.GetGenericArguments();
			Type entityType = types[0];
		
			object entities = GetEntityListFromSheet(sheet, entityType);
			assetField.SetValue(asset, entities);
			sheetCount++;
		}

		EditorUtility.SetDirty(asset);
	}
	static IWorkbook LoadBook(string excelPath)
	{
		using(FileStream stream = File.Open(excelPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			if (Path.GetExtension(excelPath) == ".xls") return new HSSFWorkbook(stream);
			else return new XSSFWorkbook(stream);
		}
	}

	/// <summary> 行からEntityを作成する </summary>
	static object CreateEntityFromRow(IRow row, List<string> columnNames, Type entityType, string sheetName)
	{
		var entity = Activator.CreateInstance(entityType);

		for (int i = 0; i < columnNames.Count; i++)
		{
			FieldInfo entityField = entityType.GetField(
				columnNames[i],
				BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic 
			);
			if (entityField == null) continue;
			if (!entityField.IsPublic && entityField.GetCustomAttributes(typeof(SerializeField), false).Length == 0) continue;

			ICell cell = row.GetCell(i);
			if (cell == null) continue;

			try
			{
				object fieldValue = CellToFieldObject(cell, entityField);
				entityField.SetValue(entity, fieldValue);
			}
			catch
			{
				throw new Exception(string.Format("Invalid excel cell type at row {0}, column {1}, {2} sheet.", row.RowNum, cell.ColumnIndex, sheetName));
			}
		}
		return entity;
	}

	static object CellToFieldObject(ICell cell, FieldInfo fieldInfo, bool isFormulaEvalute = false)
	{
		var type = isFormulaEvalute ? cell.CachedFormulaResultType : cell.CellType;

		switch(type)
		{
			case CellType.String:
				if (fieldInfo.FieldType.IsEnum) return Enum.Parse(fieldInfo.FieldType, cell.StringCellValue);
				else return cell.StringCellValue;
			case CellType.Boolean:
				return cell.BooleanCellValue;
			case CellType.Numeric:
				return Convert.ChangeType(cell.NumericCellValue, fieldInfo.FieldType);
			case CellType.Formula:
				if (isFormulaEvalute)
				{
					Debug.Log("式を値に変換できませんでした。");
					return null;
				}
				Debug.Log("式を値に変換しました。");
				//式の中の式に対応するために再帰処理をしている？
				return CellToFieldObject(cell, fieldInfo, true); 
			default:
				if(fieldInfo.FieldType.IsValueType)
				{
					return Activator.CreateInstance(fieldInfo.FieldType);
				}
				return null;
		}
	}



	static object GetEntityListFromSheet(ISheet sheet, Type entityType)
	{
		List<string> excelColumnNames = GetFieldNamesFromSheetHeader(sheet);
		excelColumnNames = excelColumnNames.Select(x => x.Trim()).ToList();
		Type listType = typeof(List<>).MakeGenericType(entityType);
		MethodInfo listAddMethod = listType.GetMethod("Add", new Type[]{entityType});
		object list = Activator.CreateInstance(listType);

		// row of index 0 is header
		for (int i = 1; i <= sheet.LastRowNum; i++)
		{
			IRow row = sheet.GetRow(i);
			if(row == null) break;

			ICell entryCell = row.GetCell(0); 
			if(entryCell == null || entryCell.CellType == CellType.Blank) break;

			// skip comment row
			if(entryCell.CellType == CellType.String && entryCell.StringCellValue.StartsWith("#")) continue;

			var entity = CreateEntityFromRow(row, excelColumnNames, entityType, sheet.SheetName);
			listAddMethod.Invoke(list, new object[] { entity });
		}
		return list;
	}
	
	/// <summary>
	/// ExcelSheetからヘッダーを読み込み
	/// </summary>
	static List<string> GetFieldNamesFromSheetHeader(ISheet sheet)
	{
		IRow headerRow = sheet.GetRow(0);

		var fieldNames = new List<string>();
		for (int i = 0; i < headerRow.LastCellNum; i++)
		{
			var cell = headerRow.GetCell(i);
			if(cell == null || cell.CellType == CellType.Blank) break;
			fieldNames.Add(cell.StringCellValue);
		}
		return fieldNames;
	}
	

}
