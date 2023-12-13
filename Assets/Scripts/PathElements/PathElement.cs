using System.Collections;
using System.Collections.Generic;
using Unity.Android.Gradle;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class PathElement : MonoBehaviour, IStateChangeObservable
{
    protected StateMachine _stateMachine = new StateMachine();
    protected List<IStateChangeListener> _listeners = new List<IStateChangeListener>();

    private Color _inkColor = PathElementState.NoColor;

    public TextMeshProUGUI _debugText;
    public GameObject _debugTextContainer;

    public Color InkColor {
        get => _inkColor;
        set {
            if (value == PathElementState.NoColor)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }

            Image[] images = gameObject.GetComponentsInChildren<Image>(true);
            if (images.Length > 1)
            {
                images[1].color = value;
            }
            _inkColor = value;
        }
    }

    public virtual void Subscribe(IStateChangeListener listener) => _listeners.Add(listener);

    public virtual void Unsubscribe(IStateChangeListener listener) => _listeners.Remove(listener);

    public virtual void ReportStateChanged() 
    {
        foreach (IStateChangeListener listener in _listeners)
        {
            listener.OnStateEnter(this, _stateMachine.CurrentState);
        }
    }

    // Здравствуй дорогой читатель, прибыл ты издалека?
    // Коль читаешь, значит хочешь знать ты всё наверняка?
    // Осуждаю тебя, путник, но однако помогу!
    // Метод сей приватным сделал чтоб решить одну беду:
    // Изменяя состоянье, ты, негодник озороной
    // Не хотел бы репу мучать, выбор делать сложный свой
    // И поэтому, чуть ниже, под собачий, волчий вой,
    // Непременно ты увидишь public метод золотой
    // За тебя он пусть решает - расслабляй свою мозгу!
    // Да благодари разраба, коль купаешься в жиру!
    public virtual bool ChangeState(PathElementState state)
    {
        return _stateMachine.ChangeState(state);
    }

    public virtual void ResetState(PathElementState newState)
    {
        _stateMachine.ResetState(newState);
    }

    public abstract void HandleTouch();

    public abstract void SetPaintableAround(params PathElement[] ignoredElements);

    public abstract void SetUnpaintableAround(params PathElement[] ignoredElements);

    protected void InitCollider()
    {
        Image image = GetComponent<Image>();
        Collider2D collider = GetComponent<Collider2D>();

        if (image != null && collider != null)
        {
            if (collider as BoxCollider2D)
            {
                BoxCollider2D boxCollider = (BoxCollider2D)collider;
                boxCollider.size = new Vector2(image.rectTransform.rect.width, image.rectTransform.rect.height);
            }
            else if (collider as CapsuleCollider2D)
            {
                CapsuleCollider2D boxCollider = (CapsuleCollider2D)collider;
                boxCollider.size = new Vector2(image.rectTransform.rect.width, image.rectTransform.rect.height);
            }
        }
        else
        {
            Debug.LogError("Image or Collider2D component not found on the GameObject.");
        }
    }

    protected void InitTextDebug(int fontSize)
    {
        _debugTextContainer = new GameObject("TextContainer");
        _debugTextContainer.transform.SetParent(transform, false);
        _debugText = _debugTextContainer.AddComponent<TextMeshProUGUI>();

        _debugText.fontSize = fontSize;
        _debugText.color = Color.white;
        _debugText.enableWordWrapping = false;
    }

    // Здесь и померли когда-то
    // Славных три богатыря
    // Чтобы в коде разобраться
    // Надо копоти подда..
}
