using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Enemy;

public class EnemyMove : EnemyBase
{
    [Header("Enemy Path")]
    public LineRenderer line;
    public float speed = 5f;
    public float rotateSpeed = 0.2f;

    private int _currentPointIndex = 0; // ponto atual do caminho
    private Vector3[] _positions;       // array de pontos da linha

    private void Start()
    {
        _positions = new Vector3[line.positionCount]; 
        line.GetPositions(_positions);      // copia os pontos do Line Renderer para um array
        transform.position = _positions[0]; // garante que o objeto começa no primeiro ponto
    }

    private void Update()
    {
        if (_positions.Length == 0) return;

        Vector3 direction = (_positions[_currentPointIndex] - transform.position).normalized; // direção até o ponto atual

        // se tiver direção válida, rotaciona suavemente usando DOTween
        if(direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.DORotateQuaternion(targetRotation, rotateSpeed);
            PlayAnimationByTrigger(Animation.AnimationType.RUN);
        }

        // move em direção ao ponto atual
        transform.position = Vector3.MoveTowards(transform.position, _positions[_currentPointIndex], speed * Time.deltaTime);

        // se chegou no ponto atual, avança pro próximo
        if(Vector3.Distance(transform.position, _positions[_currentPointIndex]) < 0.1f)
        {
            _currentPointIndex++;
            if(_currentPointIndex >= _positions.Length)
            {
                _currentPointIndex = 0;  // reinicia o loop
            }
        }
    }
}
