using UnityEngine;

public interface IAimStrategy
{
    /// <summary>
    /// 初始化瞄准策略，由Tower在启动时调用
    /// </summary>
    /// <param name="tower">调用此策略的Tower脚本引用</param>
    void Initialize(Tower tower);

    /// <summary>
    /// 执行瞄准逻辑，每帧由Tower在找到目标后调用
    /// </summary>
    /// <param name="target">当前要瞄准的目标</param>
    void Aim(Transform target);
}