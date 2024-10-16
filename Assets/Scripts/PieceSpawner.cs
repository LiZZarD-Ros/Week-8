using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{

    public PieceType type;
    private Piece currentPiece;

    public void Spawn()
    {
       int amtTypes = 0;
        switch (type)
        {
            case PieceType.jump:
                amtTypes = LevelManager.Instance.jumps.Count;
                break;
            case PieceType.slide:
                amtTypes = LevelManager.Instance.slides.Count;
                break;
            case PieceType.longblock:
                amtTypes = LevelManager.Instance.longblocks.Count;
                break;
            case PieceType.ramp:
                amtTypes = LevelManager.Instance.ramps.Count;
                break;
        }
        currentPiece = LevelManager.Instance.GetPiece(type, Random.Range(0, amtTypes));
        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);
    }

    public void Despawn()
    {
        currentPiece.gameObject.SetActive(false);
    }
}
