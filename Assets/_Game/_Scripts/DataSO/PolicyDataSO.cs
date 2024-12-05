using UnityEngine;

[CreateAssetMenu(menuName = "Data SO/Privacy Policy", fileName = "Policy Data")]
public class PolicyDataSO : ScriptableObject
{
    [SerializeField] [TextArea(2,2)] string _privacyPolicyLink;

    public string PrivacyPolicyURL => _privacyPolicyLink;
}
