using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Hikanyan_Assets.ShaderGraph
{
    sealed class PostEffectPass : ScriptableRenderPass
    {
        public Material Material;
        private bool isEnabled = true;

        private RTHandle source;
        private RTHandle destination;

        public PostEffectPass()
        {
            // RTHandleを初期化します
            destination = RTHandles.Alloc("_TemporaryRenderTarget", name: "_TemporaryRenderTarget");
        }

        public void SetEnabled(bool value)
        {
            isEnabled = value;
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData data)
        {
            source = data.cameraData.renderer.cameraColorTargetHandle;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData data)
        {
            if (Material == null || !isEnabled) return;

            var cmd = CommandBufferPool.Get("PostEffect");

            // Blitter APIを使ってBlit処理を行います
            Blitter.BlitCameraTexture(cmd, source, destination, Material, 0);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        /// <summary>
        /// カメラのクリーンアップ時に呼び出されます
        /// </summary>
        public override void OnCameraCleanup(CommandBuffer cmd)
        {
        }

        /// <summary>
        /// フレーム終了時に呼び出されます
        /// </summary>
        /// <param name="cmd"></param>
        public override void FrameCleanup(CommandBuffer cmd)
        {
            // RTHandleを解放します
            if (destination == null) return;
            RTHandles.Release(destination);
            destination = null;
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
                Material = material,
                renderPassEvent = RenderPassEvent.AfterRendering // RenderPassEventのタイミングを設定
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