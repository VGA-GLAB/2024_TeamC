using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class ExcelAssetAttribute : Attribute
{
	public string ExcelName { get; set; }
}