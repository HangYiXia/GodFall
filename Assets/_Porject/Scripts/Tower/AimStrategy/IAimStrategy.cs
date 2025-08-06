using UnityEngine;

public interface IAimStrategy
{
    /// <summary>
    /// ��ʼ����׼���ԣ���Tower������ʱ����
    /// </summary>
    /// <param name="tower">���ô˲��Ե�Tower�ű�����</param>
    void Initialize(Tower tower);

    /// <summary>
    /// ִ����׼�߼���ÿ֡��Tower���ҵ�Ŀ������
    /// </summary>
    /// <param name="target">��ǰҪ��׼��Ŀ��</param>
    void Aim(Transform target);
}