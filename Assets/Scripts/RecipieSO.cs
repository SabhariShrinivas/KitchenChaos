using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipie", menuName = "Recipie")]
public class RecipieSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectSOList;
    public string recipieName;
}
