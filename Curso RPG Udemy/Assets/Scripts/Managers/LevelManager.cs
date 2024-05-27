using System.Collections;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private Transform _spawnPoint;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if (_character.CharacterHealth.Defeated)
            {
                _character.transform.localPosition = _spawnPoint.position;
                _character.RestoreCharacter();
            }
        }
        
    }

}
