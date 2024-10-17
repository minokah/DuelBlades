using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeController : MonoBehaviour
{
    public CharacterInfo characterInfo;
    StatusArea statusArea;

    public bool usingCharge = false;
    public AudioSource chargeAudio, chargeEndAudio;
    private float useCooldown = 2f;

    // Start is called before the first frame update
    void Start()
    {
        statusArea = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().UI.StatusArea;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterInfo.health <= 0) usingCharge = false;
        else if (Input.GetKeyDown(KeyCode.Q) && useCooldown > 2f)
        {
            if (characterInfo.charge > 0)
            {
                usingCharge = !usingCharge;

                // Activate charge
                if (usingCharge)
                {
                    if (!chargeAudio.isPlaying) chargeAudio.Play();
                }
                // No charge :(
                else
                {
                    if (!chargeEndAudio.isPlaying) chargeEndAudio.Play();
                }

                useCooldown = 0;
            }
        }

        if (usingCharge)
        {
            characterInfo.weaponMultiplier = 2;

            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ChargeParticles"))
            {
                obj.GetComponent<ParticleSystem>().enableEmission = true;
            }

            if (characterInfo.charge > 0) characterInfo.charge -= characterInfo.drainRate * Time.deltaTime;
            else
            {
                usingCharge = false;
                characterInfo.charge = 0;
                if (!chargeEndAudio.isPlaying) chargeEndAudio.Play();
                characterInfo.weaponMultiplier = 1;
                foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ChargeParticles"))
                {
                    obj.GetComponent<ParticleSystem>().enableEmission = false;
                }
            }
        }
        else
        {
            characterInfo.weaponMultiplier = 1;

            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("ChargeParticles"))
            {
                obj.GetComponent<ParticleSystem>().enableEmission = false;
            }
            /*
            if (characterInfo.charge < characterInfo.chargeMax) characterInfo.charge += Time.deltaTime;
            else characterInfo.charge = characterInfo.chargeMax;
            */
        }

        statusArea.SetChageAmount((float)characterInfo.charge / characterInfo.chargeMax);
        useCooldown += Time.deltaTime;
    }

    public void Die()
    {
        if (usingCharge)
        {
            chargeAudio.Stop();
            chargeEndAudio.Play();
        }

        usingCharge = false;
    }
}
