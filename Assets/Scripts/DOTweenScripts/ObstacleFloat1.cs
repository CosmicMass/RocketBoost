using UnityEngine;
using DG.Tweening;

public class ObstacleFloat1 : MonoBehaviour
{

    [SerializeField] private Transform obstacle;
    [SerializeField] private float cycleLength = 2;
    [SerializeField] Ease easeType = Ease.InOutSine;

    Sequence seq;

    void Start()
    {
        //DOTween.Init();
        transform.DOLocalMove(new Vector3(0, 8, 0), cycleLength).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
        transform.DORotate(new Vector3(360, 0, 0), cycleLength * 0.5f,RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        //transform.DOScaleX(2, 1).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
    }


}
