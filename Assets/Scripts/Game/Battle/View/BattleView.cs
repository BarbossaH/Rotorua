using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Battle
{
    public interface IBattleView
    {
        UniTask PlayBattleStartAnimationAsync();
    }

    public class BattleView : MonoBehaviour, IBattleView
    {
        private Animator _animator;

        private void Awake()
        {
            // _animator = GetComponent<Animator>();
        }
        public async UniTask PlayBattleStartAnimationAsync()
        {
            await UniTask.SwitchToMainThread();
            await UniTask.WaitUntil(() => gameObject.activeInHierarchy);

            Debug.Log("BattleView, and the animation is playing");
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            Debug.Log("BattleView, and the animation Finished");

            #region example code for learning
            //play the battle start animation
            // _animator.Play("animation name");

            // await UniTask.WaitUntil(() =>
            // {
            //     var state = _animator.GetCurrentAnimatorStateInfo(0);
            //     return state.IsName("animation name");
            // });

            // await UniTask.WaitUntil(() =>
            // {
            //     var state = _animator.GetCurrentAnimatorStateInfo(0);
            //     return state.normalizedTime > 1 && !_animator.IsInTransition(0);
            // });

            // Debug.Log($"Animator 动画 {"animation name"} 播放完成");
            #endregion
        }
    }
}