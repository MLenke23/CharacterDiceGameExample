using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
  
    public AudioClip [] Audios;
    [HideInInspector]
    public int ID;
    [HideInInspector]
    public int Pos;
    private AudioSource AS;
    private Vector3 posfija;
    private Animator anim;
    public Camera cam;
    public GameObject[] effects;
    public GameObject StatsContainer;//para moverlo

    public CharacterStats stats { get; set; }

    private void OnEnable()
    {
        stats= GetComponent<CharacterStats>();
        AS = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        posfija = transform.position;
    }
    public void setStats(string name, int life,int aF,int aM,int rF,int rM)
    {
        stats.IconCharacter = ID;
        stats.CharacterName = name;
        stats.actLife = stats.maxLife = life;
        stats.atkFisico = aF;
        stats.atkMagico = aM;
        stats.defFisico =rF;
        stats.defMagico = rM;

        stats.addLife(0);
    }
    

    private void OnMouseDown()
    {
        MainController.Instance.ShowStats(ID,true);
    }
    private void OnMouseExit()
    {
        MainController.Instance.ShowStats(ID, false);
    }

    public void Presentation()
    {
        cam.gameObject.SetActive(true);
        anim.SetBool("Presentation",true);
        AS.clip= Audios[0];
        AS.Play();
        StartCoroutine(EndAnimpresentaciones());
    }
    public void EndPresentation()
    {
        cam.gameObject.SetActive(false);
    }

    private IEnumerator EndAnimpresentaciones()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Presentation", false);

    }
    public void moveTo(int idPosTomove)
    {
        int move = Pos - idPosTomove;
        if (move == -1)//->
        {
            transform.position += new Vector3(1, 0, 0);
            StatsContainer.transform.position += new Vector3(1, 0, 0);
        }
        else
        if (move == 1)//<-
        {
            transform.position += new Vector3(-1, 0, 0);
            StatsContainer.transform.position += new Vector3(-1, 0, 0);
        }
        else
        if (move == -6)//down
        {
            transform.position += new Vector3(0, 0, -1);
            StatsContainer.transform.position += new Vector3(0, 0, -1);
        }
        else
        if (move == 6)//up
        {
            transform.position += new Vector3(0, 0, 1);
            StatsContainer.transform.position += new Vector3(0, 0, 1);
        }
        Pos += move*-1;

    }

    public void Pose(int p)
    {
        if (p == 0)
        {
            anim.SetBool("Attack1",false);
        }else
        if (p == 1)
        {
            anim.SetBool("Attack1", true);
        }
    }

    public void UseHability(int h)
    {
        if (h == 0)
        {
            if (effects != null)
            {
                var clone=Instantiate(effects[0]);
                
                clone.transform.localEulerAngles = transform.eulerAngles+new Vector3(0,-90,0);
                clone.transform.localPosition = transform.localPosition+new Vector3(.55f,1.25f,-.1f);
            }
            
        }
    }


}
