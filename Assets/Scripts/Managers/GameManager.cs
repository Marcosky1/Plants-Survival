using UnityEngine;
using Assets.Scripts.GameEvents;
using Photon.Pun;

public class GameManager : NonPersistentSingleton<GameManager>
{
    [SerializeField] private bool _onStartEvent;
    [SerializeField] private bool _onFinishEvent;

    [SerializeField] private GameEvent onStartScene;
    [SerializeField] private GameEvent onFinishScene;

    private void Start()
    {
        if (_onStartEvent)
        {
            onStartScene?.Raise();
        }
    }
    private void OnDisable()
    {
        if (_onFinishEvent)
        {
            onFinishScene?.Raise();
        }
    }
    public void DebugSomething(string smt)
    {
        Debug.Log(smt);
    }
    public void DestroyGameObject(GameObject victim)
    {
        Destroy(victim);
    }
    public void PhotonDestroyGameObject(GameObject victim)
    {
        PhotonNetwork.Destroy(victim);
    }
    public void DestroyComponent(Component victim)
    {
        Destroy(victim);
    }
}