using System;
using UnityEngine;
using System.Collections.Generic;

// this class is needed to update the UI from the async callbacks
internal class RunOnUiThread : MonoBehaviour
{
    internal static RunOnUiThread worker;
    private Queue<Action> jobs = new Queue<Action>();

    void Awake()
    {
        worker = this;
    }

    void Update()
    {
        while (jobs.Count > 0)
            jobs.Dequeue().Invoke();
    }

    public static void AddJob(Action newJob)
    {
        worker.jobs.Enqueue(newJob);
    }
}
