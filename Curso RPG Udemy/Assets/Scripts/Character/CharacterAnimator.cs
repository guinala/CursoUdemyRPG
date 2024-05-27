using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private string layerIdle;
    [SerializeField] private string layerWalking;

    private Animator _animator;
    private CharacterMovement _characterMovement;
    private readonly int direccionX = Animator.StringToHash("X");
    private readonly int direccionY = Animator.StringToHash("Y");
    private readonly int defeated = Animator.StringToHash("Defeated");


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterMovement = GetComponent<CharacterMovement>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLayer();

        if(_characterMovement.inMovement == false)
        {
            return; //Si se produce el return, no continua
        }

        _animator.SetFloat(direccionX, _characterMovement.DireccionMovimiento.x);
        _animator.SetFloat(direccionY, _characterMovement.DireccionMovimiento.y);
    }

    private void ActiveLayer(string layerName)
    {
        for (int i = 0; i < _animator.layerCount; i++)
        {
            _animator.SetLayerWeight(i, 0); //Peso 0 significa desactivar
        }

        _animator.SetLayerWeight(_animator.GetLayerIndex(layerName), 1); //Peso 1 significa activar
    }

    private void UpdateLayer()
    {
        if (_characterMovement.inMovement)
        {
            ActiveLayer(layerWalking);
        }

        else
        {
            ActiveLayer(layerIdle);
        }
    }

    public void ReviveCharacter()
    {
        ActiveLayer(layerIdle);
        _animator.SetBool(defeated, false);
    }

    private void Defeated()
    {
        if(_animator.GetLayerWeight(_animator.GetLayerIndex(layerIdle)) == 1)
             _animator.SetBool(defeated, true);
    }

    private void OnEnable()
    {
        CharacterHealth.DefeatedEvent += Defeated;
    }

    private void OnDisable()
    {
        CharacterHealth.DefeatedEvent -= Defeated;
    }

    
}
