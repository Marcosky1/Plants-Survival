using Assets.Scripts.GameEvents;
using Photon.Pun;
using UnityEngine;

public class PhotonIdentity : MonoBehaviour
{
    [SerializeField] private PhotonView view;
    
    public GameEvent IsMine;
    public GameEvent IsNotMine;

    private void Start()
    {
        if (view.IsMine)
        {
            IsMine.Raise();
        }
        else
        {
            IsNotMine.Raise();
        }
    }
}