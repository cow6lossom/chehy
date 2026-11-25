using UnityEngine;

using System.Collections;
using MoreMountains.Feedbacks;

namespace Key._00.Scripts.Skill {
    public class LotusFlowerAnimation : MonoBehaviour {
        [SerializeField] private MMF_Player dissolveFeedback;
        [SerializeField] private ParticleSystem dissolveParticle;
        [SerializeField] private Material dissolveMask;
        [SerializeField] private float particleRate = 70f;

        private Animator _animator;
        private SpriteRenderer _sr;
        private Material _material;
        private Material _maskMaterial;
        private Texture2D _maskTexture;
        private Sprite _sprite;
        private Color[] _pixels;
        private ParticleSystem _particle;
        private float _previousDissolve;
        private float _particleCount;
        
        
        public bool IsEnd { get; private set; }

        private readonly int deadHash = Animator.StringToHash("DEAD");
        private readonly int catchHash = Animator.StringToHash("CATCH");
        

        private void Awake() {
            _animator = GetComponent<Animator>();
            _sr = GetComponent<SpriteRenderer>();
            _material = _sr.material;
            _material.SetFloat("_Dissolve", 0f);
            _maskMaterial = new Material(dissolveMask);
            _maskTexture = new Texture2D(32, 32, TextureFormat.RGBAFloat, false, true);
            IsEnd = false;
        }

        public void SetDead() {
            if (_animator.GetBool(deadHash)) return;

            IsEnd = false;
            _previousDissolve = 0f;
            _particleCount = 1f;
            _material.SetFloat("_Dissolve", 0f);
            dissolveFeedback.Initialization();
            _particle = Instantiate(dissolveParticle, _sr.transform.position, dissolveParticle.transform.rotation);
            _particle.gameObject.SetActive(true);
            _particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            ParticleSystem.MainModule main = _particle.main;
            main.startDelay = 0f;
            main.simulationSpeed = 0.4f;
            ParticleSystem.EmissionModule emission = _particle.emission;
            emission.enabled = false;
            ParticleSystemRenderer sr = _particle.GetComponent<ParticleSystemRenderer>();
            sr.sortingLayerID = _sr.sortingLayerID;
            sr.sortingOrder = _sr.sortingOrder + 1;
            Destroy(_particle.gameObject, dissolveFeedback.TotalDuration + main.startLifetime.constantMax / main.simulationSpeed + 0.1f);
            _animator.SetBool(deadHash, true);
            dissolveFeedback.PlayFeedbacks();
            _particle.Play(false);
            EmitDissolveParticles(true);
        }

        public void SetCatch() {
            _animator.SetBool(catchHash, true);
        }

        public void AnimationEnd() {
            if (_animator.GetBool(deadHash)) {
                StartCoroutine(WaitForDissolve());
                return;
            }

            IsEnd = true;
        }

        private IEnumerator WaitForDissolve() {
            while (dissolveFeedback.IsPlaying) {
                yield return null;
            }

            IsEnd = true;
        }

        private void LateUpdate() {
            if (!_animator.GetBool(deadHash) || IsEnd) return;

            EmitDissolveParticles();
        }

        private void EmitDissolveParticles(bool start = false) {
            if (_sprite != _sr.sprite) {
                _sprite = _sr.sprite;
                Rect rect = _sprite.textureRect;
                _maskMaterial.SetVector("_SampleRect", new Vector4(rect.x / _sprite.texture.width, rect.y / _sprite.texture.height,
                    rect.width / _sprite.texture.width, rect.height / _sprite.texture.height));
                _maskMaterial.SetFloat("_NoiseScale", _material.GetFloat("_NoiseScale"));
                _maskMaterial.SetFloat("_NoiseStrength", _material.GetFloat("_NoiseStrength"));
                RenderTexture target = RenderTexture.GetTemporary(32, 32, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
                RenderTexture previous = RenderTexture.active;
                Graphics.Blit(_sprite.texture, target, _maskMaterial);
                RenderTexture.active = target;
                _maskTexture.ReadPixels(new Rect(0, 0, 32, 32), 0, 0, false);
                _maskTexture.Apply(false);
                RenderTexture.active = previous;
                RenderTexture.ReleaseTemporary(target);
                _pixels = _maskTexture.GetPixels();
            }

            float dissolve = _material.GetFloat("_Dissolve");
            float previousEdge = Mathf.Lerp(1.08f, -0.08f, _previousDissolve);
            float edge = Mathf.Lerp(1.08f, -0.08f, dissolve);
            if (start) {
                edge = float.MinValue;
                for (int i = 0; i < _pixels.Length; i++) {
                    if (_pixels[i].a < 0.1f) continue;

                    edge = Mathf.Max(edge, _pixels[i].r);
                }

                edge -= 0.001f;
                previousEdge = edge + 0.035f;
            }

            previousEdge = Mathf.Min(previousEdge, edge + 0.035f);
            Rect spriteRect = _sprite.textureRect;
            Vector2 offset = _sprite.textureRectOffset - _sprite.pivot;
            _particleCount = Mathf.Min(_particleCount + particleRate * Time.deltaTime, particleRate * 0.1f);

            int available = 0;
            for (int i = 0; i < _pixels.Length; i++) {
                if (_pixels[i].a < 0.1f || _pixels[i].r > previousEdge || _pixels[i].r <= edge) continue;

                available++;
            }

            int count = Mathf.Min((int)_particleCount, available);
            _particleCount -= count;

            for (int i = 0; i < _pixels.Length && count > 0; i++) {
                if (_pixels[i].a < 0.1f || _pixels[i].r > previousEdge || _pixels[i].r <= edge) continue;

                if (Random.Range(0, available) >= count) {
                    available--;
                    continue;
                }

                available--;
                count--;
                Vector3 pos = new Vector3(((i % 32 + 0.5f) / 32f * spriteRect.width + offset.x) / _sprite.pixelsPerUnit,
                    ((i / 32 + 0.5f) / 32f * spriteRect.height + offset.y) / _sprite.pixelsPerUnit, 0f);
                if (_sr.flipX) pos.x = -pos.x;
                if (_sr.flipY) pos.y = -pos.y;
                pos = _sr.transform.TransformPoint(pos);
                ParticleSystem.MainModule main = _particle.main;
                if (main.simulationSpace == ParticleSystemSimulationSpace.Local) {
                    pos = _particle.transform.InverseTransformPoint(pos);
                }
                else if (main.simulationSpace == ParticleSystemSimulationSpace.Custom) {
                    pos = main.customSimulationSpace.InverseTransformPoint(pos);
                }

                ParticleSystem.EmitParams emit = new ParticleSystem.EmitParams();
                emit.position = pos;
                emit.applyShapeToPosition = false;
                _particle.Emit(emit, 1);
            }

            _previousDissolve = dissolve;
        }

        private void OnDestroy() {
            Destroy(_material);
            Destroy(_maskMaterial);
            Destroy(_maskTexture);
        }
    }
}
