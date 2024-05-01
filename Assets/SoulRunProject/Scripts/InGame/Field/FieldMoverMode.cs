using UnityEngine;

namespace SoulRunProject.InGame
{
    public enum FieldMoverMode
    {
        [InspectorName("一定")]
        Normal,
        [InspectorName("ランダム")]
        Random,
        [InspectorName("ボスステージ")]
        Boss
    }
}