using UnityEngine;
using DG.Tweening;

public class ObstacleOsc : MonoBehaviour
{
    [SerializeField] Ease easeType = Ease.InOutSine;
    [SerializeField] Vector3 desiredPosition;
    [SerializeField] float cycleLength;

    void Start()
    {
        //DOTween.Init();
        transform.DOMove(desiredPosition, cycleLength).SetLoops(-1,LoopType.Yoyo).SetEase(easeType);
        transform.DOLocalRotate(new Vector3(0, 360, 0), cycleLength, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        //transform.DOScaleX(2, 1).SetEase(easeType).SetLoops(-1, LoopType.Yoyo);
    }


}
