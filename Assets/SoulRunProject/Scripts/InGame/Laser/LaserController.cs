using UnityEngine;

namespace SoulRunProject.InGame
{
    public class LaserController : MonoBehaviour
    {
        #region SoulRun
        private int _turnCount = 2;
        public Vector3 StartDirection { get; set; }
        public Vector3 EndDirection { get; set; }
        public float Timer { get; set; }
        public bool TurnSide { get; set; }
        #endregion
        #region Hovl_Studio
        [SerializeField] GameObject _hitEffect;
        [SerializeField] float _hitOffset = 0;
        [SerializeField] bool _useLaserRotation = false;

        [SerializeField] float _maxLength;
        private LineRenderer _laser;

        [SerializeField] float _mainTextureLength = 1f;
        [SerializeField] float _noiseTextureLength = 1f;
        private Vector4 _length = new Vector4(1,1,1,1);
        //private Vector4 LaserSpeed = new Vector4(0, 0, 0, 0); {DISABLED AFTER UPDATE}
        //private Vector4 LaserStartSpeed; {DISABLED AFTER UPDATE}
        //One activation per shoot
        private bool _laserSaver = false;
        private bool _updateSaver = false;

        private ParticleSystem[] _effects;
        private ParticleSystem[] _hit;
        #endregion
        public int TurnCount
        {
            get => _turnCount;
            set => _turnCount = value;
        }

        private void Start()
        {
            //Get LineRender and ParticleSystem components from current prefab;  
            _laser = GetComponent<LineRenderer>();
            _effects = GetComponentsInChildren<ParticleSystem>();
            _hit = _hitEffect.GetComponentsInChildren<ParticleSystem>();
            //if (Laser.material.HasProperty("_SpeedMainTexUVNoiseZW")) LaserStartSpeed = Laser.material.GetVector("_SpeedMainTexUVNoiseZW");
            //Save [1] and [3] textures speed
            //{ DISABLED AFTER UPDATE}
            //LaserSpeed = LaserStartSpeed;
        }

        private void Update()
        {
              //if (Laser.material.HasProperty("_SpeedMainTexUVNoiseZW")) Laser.material.SetVector("_SpeedMainTexUVNoiseZW", LaserSpeed);
        //SetVector("_TilingMainTexUVNoiseZW", Length); - old code, _TilingMainTexUVNoiseZW no more exist
        _laser.material.SetTextureScale("_MainTex", new Vector2(_length[0], _length[1]));                    
        _laser.material.SetTextureScale("_Noise", new Vector2(_length[2], _length[3]));
        //To set LineRender position
        if (_laser != null && _updateSaver == false)
        {
            _laser.SetPosition(0, transform.position);
            RaycastHit hit; //DELETE THIS IF YOU WANT USE LASERS IN 2D
            //ADD THIS IF YOU WANNT TO USE LASERS IN 2D: RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, MaxLength);       
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _maxLength))//CHANGE THIS IF YOU WANT TO USE LASERRS IN 2D: if (hit.collider != null)
            {
                //End laser position if collides with object
                _laser.SetPosition(1, hit.point);

                    _hitEffect.transform.position = hit.point + hit.normal * _hitOffset;
                if (_useLaserRotation)
                    _hitEffect.transform.rotation = transform.rotation;
                else
                    _hitEffect.transform.LookAt(hit.point + hit.normal);

                foreach (var AllPs in _effects)
                {
                    if (!AllPs.isPlaying) AllPs.Play();
                }
                //Texture tiling
                _length[0] = _mainTextureLength * (Vector3.Distance(transform.position, hit.point));
                _length[2] = _noiseTextureLength * (Vector3.Distance(transform.position, hit.point));
                //Texture speed balancer {DISABLED AFTER UPDATE}
                //LaserSpeed[0] = (LaserStartSpeed[0] * 4) / (Vector3.Distance(transform.position, hit.point));
                //LaserSpeed[2] = (LaserStartSpeed[2] * 4) / (Vector3.Distance(transform.position, hit.point));
                //Destroy(hit.transform.gameObject); // destroy the object hit
                //hit.collider.SendMessage("SomeMethod"); // example
                /*if (hit.collider.tag == "Enemy")
                {
                    hit.collider.GetComponent<HittedObject>().TakeDamage(damageOverTime * Time.deltaTime);
                }*/
            }
            else
            {
                //End laser position if doesn't collide with object
                var EndPos = transform.position + transform.forward * _maxLength;
                _laser.SetPosition(1, EndPos);
                _hitEffect.transform.position = EndPos;
                foreach (var AllPs in _hit)
                {
                    if (AllPs.isPlaying) AllPs.Stop();
                }
                //Texture tiling
                _length[0] = _mainTextureLength * (Vector3.Distance(transform.position, EndPos));
                _length[2] = _noiseTextureLength * (Vector3.Distance(transform.position, EndPos));
                //LaserSpeed[0] = (LaserStartSpeed[0] * 4) / (Vector3.Distance(transform.position, EndPos)); {DISABLED AFTER UPDATE}
                //LaserSpeed[2] = (LaserStartSpeed[2] * 4) / (Vector3.Distance(transform.position, EndPos)); {DISABLED AFTER UPDATE}
            }
            //Insurance against the appearance of a laser in the center of coordinates!
            if (_laser.enabled == false && _laserSaver == false)
            {
                _laserSaver = true;
                _laser.enabled = true;
            }
        }     
        }
    }
}