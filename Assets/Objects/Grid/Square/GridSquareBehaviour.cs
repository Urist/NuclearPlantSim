using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSquareBehaviour : MonoBehaviour
{
    /// Constants

    const string BACKGROUND_SORT_LAYER = "Background";
    const string OVERLAY_SORT_LAYER = "Overlay";

    /// Editor set variables

    public Sprite devSprite;
    public Sprite occupationSprite;

    /// Script set variables

    private GridState _state;
    public GridState State
    {
        get
        {
            return _state;
        }
        set
        {
            if (_state != value)
            {
                _state = value;

                SpriteRenderer gridSprite = GetComponent<SpriteRenderer>();

                switch (_state)
                {
                    case GridState.Dev:
                        gridSprite.sprite = devSprite;
                        gridSprite.color = Color.white;
                        gridSprite.sortingLayerName = BACKGROUND_SORT_LAYER;
                        break;
                    case GridState.Occupation:
                        gridSprite.sprite = occupationSprite;
                        gridSprite.color = _occupied ? Color.red : Color.green;
                        gridSprite.sortingLayerName = OVERLAY_SORT_LAYER;
                        break;
                    default:
                        Debug.Log($"GridSquare cannot render unknown GridState '{_state.ToString()}'", this);
                        break;
                }
            }
        }
    }

    private bool _occupied;
    public bool Occupied
    {
        get
        {
            return _occupied;
        }
        set
        {
            if (_state == GridState.Occupation
                && _occupied != value)
            {
                SpriteRenderer gridSprite = GetComponent<SpriteRenderer>();
                gridSprite.color = value ? Color.red : Color.green;
            }

            _occupied = value;
        }
    }
}
