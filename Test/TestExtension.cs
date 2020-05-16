using System.Collections.Generic;
using UnityEngine;


public sealed class TestExtension
{
    #region Methods

    private void NameMethod()
    {
        List<int> list = new List<int>();
        List<int> list2 = new List<int>();

        list.Add(5);
        5.AddList(list).AddList(list2);

        bool test = "true".TryBool();

        var myClass = new MyClass { A = 7 };
        var c = myClass.DeepCopy();
        c.A = 5;
        Debug.Log(myClass.A);
    }

    #endregion

    class MyClass
    {
        public int A;
    }
}
