using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Hikanyan_Assets.ShaderGraph
{
    sealed class PostEffectPass : ScriptableRenderPass
    {
        public Material material;
        private bool isEnabled = true;

        public void SetEnabled(bool value)
        {
            isEnabled = value;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData data)
        {
            if (material == null || !isEnabled) return;

            var cmd = CommandBufferPool.Get("PostEffect");
            Blit(cmd, ref data, material, 0);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    public sealed class PostEffectAddFeature : ScriptableRendererFeature
    {
        public Material material;

        private PostEffectPass _pass;

        public override void Create()
        {
            _pass = new PostEffectPass
            {
                material = material,
                renderPassEvent = RenderPassEvent.AfterRendering
            };
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData data)
        {
            renderer.EnqueuePass(_pass);
        }

        public void SetEffectEnabled(bool value)
        {
            if (_pass != null)
            {
                _pass.SetEnabled(value);
            }
        }
    }
}