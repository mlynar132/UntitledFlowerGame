using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( CanvasGroup ) )]
public class UIFade : MonoBehaviour
{
    [SerializeField, Range( 0.001f, 10f )] private float _fadeInTime;
    [SerializeField, Range( 0.001f, 10f )] private float _fadeOutTime;
    [SerializeField, Min( 0 )] private float _holdTime;

    private float _timer;
    private CanvasGroup _canvasGroup;

    private void OnEnable( )
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine( FadeIn() );
    }

    private IEnumerator FadeIn( )
    {
        _timer = 0;

        while ( _canvasGroup.alpha < 1 )
        {
            yield return null;
            _timer += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp( 0, 1, _timer / _fadeInTime );
        }

        yield return new WaitForSeconds( _holdTime );
        
        StartCoroutine( FadeOut() );
    }

    private IEnumerator FadeOut( )
    {
        _timer = 0;

        while ( _canvasGroup.alpha > 0 )
        {
            yield return null;
            _timer += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp( 1, 0, _timer / _fadeInTime );
        }
    }
}