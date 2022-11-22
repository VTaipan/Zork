using UnityEngine;
using Zork.Common;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnityOutputService : MonoBehaviour, IOutputService
{
    [SerializeField]
    private TextMeshProUGUI TextLinePrefab;

    [SerializeField]
    private Image NewLinePrefab;

    [SerializeField]
    private Transform ContentTransform;

    [SerializeField]
    [Range(0, 100)]
    private int MaxEntries;

    public void Write(object obj) => ParseandWriteLine(obj.ToString());


    public void Write(string message) => ParseandWriteLine(message);


    public void WriteLine(object obj) => ParseandWriteLine(obj.ToString());


    public void WriteLine(string message) => ParseandWriteLine(message);


    private void ParseandWriteLine(string message)
    {
        //if (_entries.Count >= MaxEntries)
        //{
        //    _entries.Dequeue();
        //}
        var textLine = Instantiate(TextLinePrefab, ContentTransform);
        textLine.text = message;
        _entries.Enqueue(textLine.gameObject);
    }

    private Queue<GameObject> _entries = new Queue<GameObject>();
}
