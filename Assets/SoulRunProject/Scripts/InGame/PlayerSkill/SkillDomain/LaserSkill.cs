using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using SoulRunProject.Common;
using UnityEngine;

namespace SoulRunProject.InGame
{
    public class LaserSkill : AbstractSkill
    {
        private readonly List<LaserController> _laserList = new();
        private float _timer;
        private UniTask.Awaiter _coolDownAwaiter;
        private LaserSkillData SkillData => (LaserSkillData)_skillData;
        private LaserSkillParameter RuntimeParameter => (LaserSkillParameter)_runtimeParameter;
        private int _seIndex = -1;
        public LaserSkill(AbstractSkillData skillData, in PlayerManager playerManager, in Transform playerTransform) : base(skillData, in playerManager, in playerTransform)
        {
        }

        public override void StartSkill()
        {
            for (int i = 0; i < RuntimeParameter.Amount; i++)
            {
                _laserList.Add(Object.Instantiate(SkillData.Original, _playerTransform));
            }
            Arrangement();
            Initialize();
        }

        public override void UpdateSkill(float deltaTime)
        {
            int endCount = 0;
            foreach (var laser in _laserList)
            {
                if (laser.Timer > 1 && RuntimeParameter.TurnCount > laser.TurnCount)
                {
                    laser.Timer = 0;
                    laser.TurnCount++;
                    laser.StartDirection = laser.EndDirection;
                    Vector3 endPos = _playerTransform.position;
                    if (laser.TurnSide)
                    {
                        endPos.x += RuntimeParameter.Width / 2;
                    }
                    else
                    {
                        endPos.x -= RuntimeParameter.Width / 2;
                    }
                    endPos.z += RuntimeParameter.Height * laser.TurnCount;
                    var laserPos = _playerTransform.position + laser.transform.localPosition;
                    laser.EndDirection = endPos - laserPos;
                    laser.TurnSide = !laser.TurnSide;
                }
                else if (RuntimeParameter.TurnCount <= laser.TurnCount)
                {
                    laser.gameObject.SetActive(false);
                    endCount++;
                    continue;
                }

                var matrix2 = Matrix4x4.TRS(Vector3.zero, _playerTransform.rotation, Vector3.one);
                //  プレイヤーが途中で回転してもいいようにベクトルに回転行列を掛ける。
                laser.transform.rotation = 
                    Quaternion.LookRotation(Vector3.Lerp(matrix2.MultiplyVector(laser.StartDirection), matrix2.MultiplyVector(laser.EndDirection), Mathf.Clamp01(laser.Timer)));
                laser.Timer += Time.deltaTime * RuntimeParameter.Speed;
                
                if (Physics.Raycast(laser.transform.position, laser.transform.forward, out RaycastHit hit))
                {
                    if (hit.collider.TryGetComponent(out DamageableEntity entity))
                    {
                        entity.Damage(RuntimeParameter.DamageOverTime * Time.deltaTime, useSE: false);
                    }
                }
            }

            try
            {
                if (_coolDownAwaiter.IsCompleted && endCount >= _laserList.Count)
                {
                    CriAudioManager.Instance.StopSE(_seIndex);
                    _coolDownAwaiter = CoolDown().GetAwaiter();
                }
            }
            catch
            {
                
            }
        }

        public override void OnLevelUp()
        {
            //  現在のパラメーターよりもリストのレーザーの数が多ければ同じになるまで減らす
            while (RuntimeParameter.Amount < _laserList.Count)
            {
                var endIndex = _laserList.Count - 1;
                Object.Destroy(_laserList[endIndex]);
                _laserList.RemoveAt(endIndex);
            }
            //  現在のパラメーターよりもリストのレーザーの数が少なければ同じになるまで増やす
            while (RuntimeParameter.Amount > _laserList.Count)
            {
                _laserList.Add(AddLaser());
            }
            //  整列させる
            Arrangement();
        }
        async UniTask CoolDown()
        {
            await UniTask.WaitForSeconds(RuntimeParameter.CoolTime, cancellationToken: 
                _playerManagerInstance.destroyCancellationToken);
            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < _laserList.Count; i++)
            {
                var startPos = _playerTransform.position;
                startPos.x -= RuntimeParameter.Width / 2;
                startPos.z += RuntimeParameter.Height;
                var targetPos = _playerTransform.position;
                targetPos.x += RuntimeParameter.Width / 2;
                targetPos.z += RuntimeParameter.Height * 2;
                var laserPos = _playerTransform.position + _laserList[i].transform.localPosition;
                _laserList[i].StartDirection = startPos - laserPos;
                _laserList[i].EndDirection = targetPos - laserPos;
                _laserList[i].Timer = -SkillData.Delay * i;
                _laserList[i].TurnCount = 2;
                _laserList[i].TurnSide = false;
                _laserList[i].gameObject.SetActive(true);
            }
            _seIndex = CriAudioManager.Instance.PlaySE("SE_Laser");
        }
        LaserController AddLaser()
        {
            var laser = Object.Instantiate(SkillData.Original, _playerTransform);
            var startPos = _playerTransform.position;
            startPos.x -= RuntimeParameter.Width / 2;
            startPos.z += RuntimeParameter.Height;
            var targetPos = _playerTransform.position;
            targetPos.x += RuntimeParameter.Width / 2;
            targetPos.z += RuntimeParameter.Height * 2;
            laser.StartDirection = startPos - laser.transform.position;
            laser.EndDirection = targetPos - laser.transform.position;
            return laser;
        }
        void Arrangement()
        {
            float angleDiff = 180f / _laserList.Count;
            for (int i = 0; i < _laserList.Count; i++)
            {
                Vector3 pos = _playerTransform.position;
                float angle = (180 + angleDiff * i) * Mathf.Deg2Rad;
                pos.x += SkillData.Radius * Mathf.Cos(angle);
                pos.y = _playerTransform.position.y + SkillData.OffsetY;
                pos.z += SkillData.Radius * Mathf.Sin(angle);
                var matrix = Matrix4x4.TRS(Vector3.zero, _playerTransform.rotation, Vector3.one);
                pos = matrix.MultiplyPoint3x4(pos);
                //  整列前の座標を保存しておく
                var prevPos = _laserList[i].transform.position;
                _laserList[i].transform.position = pos;
                //  整列前と後の差を出す
                var moveDiff = prevPos - pos;
                //  レーザーのベクトルに差を足す
                _laserList[i].StartDirection += moveDiff;
                _laserList[i].EndDirection += moveDiff;
            }
        }
    }
}