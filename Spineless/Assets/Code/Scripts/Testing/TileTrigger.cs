using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTrigger : MonoBehaviour

{
    [SerializeField] private GameObject displayedTile;
    [SerializeField] private GameObject coveredTile;
    [SerializeField] private GameObject tileFog;
    [SerializeField] private ParticleSystem landingEffect;
    void Start()
    {
        coveredTile.SetActive(true);
        coveredTile.SetActive(true);
        displayedTile.SetActive(false);
    }
    public void FlipTile()
    {
        landingEffect.Play();
        coveredTile.SetActive(false);
        displayedTile.SetActive(true);
        tileFog.SetActive(false);
    }
}
