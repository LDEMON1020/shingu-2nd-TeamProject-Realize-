using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/DialogueData")]
public class DialogueDataSO : ScriptableObject
{
    [Header("ĳ���� ����")]
    public string characterName = "ĳ����";                     //��ȭâ�� ǥ�õ� ĳ���� �̸�
    public Sprite characterImage;                               // ĳ���� �� �̹���

    [Header("��ȭ ����")]
    [TextArea(3, 0)]                                             //Inspector���� ���� �� �Է� �����ϰ� ����
    public List<string> dialogueLines = new List<string>();     //��ȭ ����� (������� ��µ�)
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}