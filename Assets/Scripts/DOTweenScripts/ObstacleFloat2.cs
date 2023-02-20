using UnityEngine;
using DG.Tweening;

public class ObstacleFloat2 : MonoBehaviour
{

    [SerializeField] private Transform obstacle;
    [SerializeField] private float cycleLength = 2;
    [SerializeField] Ease easeType = Ease.InOutSine;

    Sequence seq;

    void Start()
    {
        //DOTween.Init();
        transform.DOLocalMove(new Vector3(0, 8, 0), cycleLength).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(new Vector3(0, 360, 0), cycleLength * 0.8f,RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        //transform.DOScaleY(10, 3).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
    }


}
