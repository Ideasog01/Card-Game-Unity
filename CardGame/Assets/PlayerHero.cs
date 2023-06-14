using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHero : MonoBehaviour
{
    public enum HeroType { ShelteringThrone };

    [SerializeField]
    private HeroType heroType;

    [Header("Tribute")]

    [SerializeField]
    private Sprite tributeArt;

    [SerializeField]
    private int tributeMaxProgress;

    [Header("Ability")]

    [SerializeField]
    private Sprite abilityArt;

    [Header("Display References")]

    [SerializeField]
    private Image abilityIcon;

    [SerializeField]
    private Image tributeIcon;

    [SerializeField]
    private TextMeshProUGUI tributeProgressText;

    private int _tributeProgress;

    public HeroType HeroTypeRef
    {
        get { return heroType; }
    }

    public int TributeMaxProgress
    {
        get { return tributeMaxProgress; }
    }

    public int TributeProgress
    {
        get { return _tributeProgress; }
        set { _tributeProgress = value; }
    }

    public TextMeshProUGUI TributeProgressText
    {
        get { return tributeProgressText; }
    }

    public void PlayerHeroAwake()
    {
        abilityIcon.sprite = abilityArt;
        tributeIcon.sprite = tributeArt;
    }

    public void HideTribute()
    {
        tributeProgressText.text = "";
        tributeIcon.color = Color.gray;
    }
}
