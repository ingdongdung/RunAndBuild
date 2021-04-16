using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SingleTonObject<T> : MonoBehaviour where T : SingleTonObject<T>
{
    public static T Instance
    {
        get
        {
            lock (m_PadLock)
            {
                if (instance != null)
                    return instance;

                instance = (T)typeof(T).GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
                return instance;
            }
        }
    }
    protected SingleTonObject() { }

    //////////////////////////////////////////////////
    // private
    private static T instance;
    private static object m_PadLock = new object();
}
