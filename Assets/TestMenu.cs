using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InFlammis.Victoria
{
    public class TestMenu : MonoBehaviour
    {
        [SerializeField] Button button;

        void Awake()
        {
            button.onClick.AddListener(() => Debug.Log("BUTTON PRESSED"));
        }
    }
}
