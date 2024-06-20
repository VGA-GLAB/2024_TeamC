using TMPro;
using UnityEngine;

namespace SoulRunProject
{
    [ExecuteAlways, RequireComponent(typeof(TMP_Text))]
    public class TMPAnimationWave : MonoBehaviour
    {
        //アニメーションの振幅
        [SerializeField] private float amp;
        //アニメーションの速度
        [SerializeField] private float speed;
        //アニメーションの長さ
        [SerializeField] private int length;

        private TMP_Text _tmpText;
        private TMP_TextInfo _tmpInfo;

        private void Start()
        {
            _tmpText = GetComponent<TMP_Text>();
        }
    
    
        private void Update()
        {
            UpdateAnimation();
        }

        private void UpdateAnimation()
        {
            _tmpText.ForceMeshUpdate(true);
            _tmpInfo = _tmpText.textInfo;

            var count = Mathf.Min(_tmpInfo.characterCount, _tmpInfo.characterInfo.Length);
            for (int i = 0; i < count; i++)
            {
                var charInfo = _tmpInfo.characterInfo[i];
                if (!charInfo.isVisible)
                    continue;

                int matIndex = charInfo.materialReferenceIndex;
                int vertIndex = charInfo.vertexIndex;

                Vector3[] verts = _tmpInfo.meshInfo[matIndex].vertices;

                float ofs = 0.5f * i;
                float sinWave = Mathf.Sin((ofs + Time.realtimeSinceStartup * Mathf.PI * speed) / length) * amp;
                verts[vertIndex + 0].y += sinWave;
                verts[vertIndex + 1].y += sinWave;
                verts[vertIndex + 2].y += sinWave;
                verts[vertIndex + 3].y += sinWave;
            }

            for (int i = 0; i < _tmpInfo.materialCount; i++)
            {
                if (_tmpInfo.meshInfo[i].mesh == null) { continue; }

                _tmpInfo.meshInfo[i].mesh.vertices = _tmpInfo.meshInfo[i].vertices;
                _tmpText.UpdateGeometry(_tmpInfo.meshInfo[i].mesh, i);
            }
        }
    }
}