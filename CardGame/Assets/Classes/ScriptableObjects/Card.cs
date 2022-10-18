using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card")]
public class Card : ScriptableObject
{
    public enum CardType { Mana, Creature, Spell, Enchantment, Prophecy, Structure, Equipment };

    public enum Range { NormalReach, FarReach, InfiniteReach };

    public enum CardEffect { Deathrattle, Battlecry, Frenzy, Avenge };

    [Header("Card Details")]

    [SerializeField]
    private string _cardName;

    [SerializeField]
    private string _cardDescription;

    [SerializeField]
    private CardType _cardType;

    [SerializeField]
    private Sprite _cardArt;

    [SerializeField]
    private string[] _cardTags;

    [Header("Mana")]

    [SerializeField]
    private int _manaCost;

    [SerializeField]
    private ManaType _manaType;

    [SerializeField]
    private int _manaGain;

    [Header("Creature")]

    [SerializeField]
    private int _creatureAttack;

    [SerializeField]
    private int _creatureHealth;

    [SerializeField]
    private Range _creatureReach;

    [SerializeField]
    private CardEffect[] creatureEffects;

    //Enchantment

    #region CardDetails

    public string CardName
    {
        get { return _cardName; }
        set { _cardName = value; }
    }

    public string CardDescription
    {
        get { return _cardDescription; }
        set { _cardDescription = value; }
    }

    public Sprite CardArt
    {
        get { return _cardArt; }
        set { _cardArt = value; }
    }

    public string[] CardTags
    {
        get { return _cardTags; }
        set { _cardTags = value; }
    }

    public CardType Object_cardType
    {
        get { return _cardType; }
        set { _cardType = value; }
    }

    #endregion

    #region Mana 

    public int ManaCost
    {
        get { return _manaCost; }
        set { _manaCost = value; }
    }

    #endregion

    #region Mana CardType

    public enum ManaType { astral, unholy, neutral, wild, infernal };

    public ManaType ObjectManaType
    {
        get { return _manaType; }
        set { _manaType = value; }
    }

    public int ManaGain
    {
        get { return _manaGain; }
        set { _manaGain = value; }
    }

    #endregion

    #region Creature CardType

    public int CreatureAttack
    {
        get { return _creatureAttack; }
        set { _creatureAttack = value; }
    }

    public int CreatureHealth
    {
        get { return _creatureHealth; }
        set { _creatureHealth = value; }
    }

    public Range CreatureReach
    {
        get { return _creatureReach; }
        set { _creatureReach = value; }
    }

    #endregion


}

////Custom inspector starts here
//#if UNITY_EDITOR
//[CustomEditor(typeof(Card))]
//public class EnumInspectorEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();

//        CreateInspectorLabel("Card Type", TextAnchor.MiddleLeft, true, Color.white);
//        var cardScript = target as Card;
//        cardScript.Object_cardType = (Card.CardType)EditorGUILayout.EnumPopup(cardScript.Object_cardType);

//        //Card Details

//        CreateInspectorLabel("Card Details", TextAnchor.MiddleCenter, true, Color.white);
//        cardScript.CardName = EditorGUILayout.TextField("Card Name", cardScript.CardName);
//        cardScript.CardDescription = EditorGUILayout.TextField("Card Description", cardScript.CardDescription, GUILayout.Width(800), GUILayout.Height(100), GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(false));

//        CreateInspectorLabel("Card Art", TextAnchor.MiddleLeft, false, Color.grey);
//        Object cardArtObj = EditorGUILayout.ObjectField(cardScript.CardArt, typeof(Sprite), false, GUILayout.Width(75), GUILayout.Height(75));
//        cardScript.CardArt = (Sprite)cardArtObj;


//        cardScript.ObjectManaType = (Card.ManaType)EditorGUILayout.EnumPopup("Mana Type", cardScript.ObjectManaType);

//        if (cardScript.Object_cardType != Card.CardType.Mana)
//        {
//            cardScript.ManaCost = EditorGUILayout.IntField("Mana Cost", cardScript.ManaCost);
//        }
//        else
//        {
//            cardScript.ManaGain = EditorGUILayout.IntField("Mana Gain", cardScript.ManaGain);
//        }

//        if(cardScript.Object_cardType == Card.CardType.Creature)
//        {
//            CreateInspectorLabel("Creature Properties", TextAnchor.MiddleCenter, true, Color.white);

//            cardScript.CreatureAttack = EditorGUILayout.IntField("Creature Attack", cardScript.CreatureAttack);
//            cardScript.CreatureHealth = EditorGUILayout.IntField("Creature Health", cardScript.CreatureHealth);
//            cardScript.CreatureReach = (Card.Range)EditorGUILayout.EnumFlagsField("Creature Reach", cardScript.CreatureReach);

//            SerializedProperty creatureEffectsProperty = serializedObject.FindProperty("creatureEffects");

//            EditorGUI.BeginChangeCheck();
//            EditorGUILayout.PropertyField(creatureEffectsProperty, true);
//            if (EditorGUI.EndChangeCheck()) //End Array inspector dropped down
//            {
//                serializedObject.ApplyModifiedProperties();
//            }
//        }

//        if(cardScript.Object_cardType == Card.CardType.Spell)
//        {
//            CreateInspectorLabel("Spell Properties", TextAnchor.MiddleCenter, true, Color.white);
//        }

//        if (cardScript.Object_cardType == Card.CardType.Enchantment)
//        {
//            CreateInspectorLabel("Enchantment Properties", TextAnchor.MiddleCenter, true, Color.white);
//        }

//        if (cardScript.Object_cardType == Card.CardType.Prophecy)
//        {
//            CreateInspectorLabel("Prophecy Properties", TextAnchor.MiddleCenter, true, Color.white);
//        }

//        if (cardScript.Object_cardType == Card.CardType.Equipment)
//        {
//            CreateInspectorLabel("Equipment Properties", TextAnchor.MiddleCenter, true, Color.white);
//        }

//        if (cardScript.Object_cardType == Card.CardType.Structure)
//        {
//            CreateInspectorLabel("Structure Properties", TextAnchor.MiddleCenter, true, Color.white);
//        }

//        EditorUtility.SetDirty(cardScript);
//    }

//    private void CreateInspectorLabel(string label, TextAnchor alignment, bool bold, Color color)
//    {
//        GUIStyle guiStyle = new GUIStyle();
//        guiStyle.alignment = alignment;
//        guiStyle.normal.textColor = color;

//        if(bold)
//        {
//            guiStyle.fontStyle = FontStyle.Bold;
//        }

//        EditorGUILayout.LabelField(label, guiStyle);
//    }
//}
//#endif