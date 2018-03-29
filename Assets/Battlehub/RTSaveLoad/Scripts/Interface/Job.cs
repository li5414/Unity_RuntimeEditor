using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


namespace Battlehub.RTSaveLoad
{
    public interface IJob
    {
        void Submit(Action<Action> job, Action completed);
    }

    public class Job : MonoBehaviour, IJob
    {
        public class JobContainer
        {
            public object Lock = new object();

            public bool IsCompleted;

            private Action<Action> m_job;

            private Action m_completed;

            public JobContainer(Action<Action> job, Action completed)
            {
                m_job = job;
                m_completed = completed;
            }

            private void ThreadFunc(object arg)
            {
                m_job(() =>
                {
                    lock (Lock)
                    {
                        IsCompleted = true;
                    }
                });   
            }

            public void Run()
            {
                ThreadPool.QueueUserWorkItem(ThreadFunc);
            }

            public void RaiseCompleted()
            {
                m_completed();
            }
        }

        private List<JobContainer> m_jobs = new List<JobContainer>();
        
        public void Submit(Action<Action> job, Action completed)
        {
            JobContainer jc = new JobContainer(job, completed);
            m_jobs.Add(jc);
            jc.Run();
        }

        private void Update()
        {
            for(int i = m_jobs.Count - 1; i >= 0; --i)
            {
                JobContainer jc = m_jobs[i];
                lock(jc.Lock)
                {
                    if(jc.IsCompleted)
                    {
                        try
                        {
                            jc.RaiseCompleted();
                        }
                        finally
                        {
                            m_jobs.RemoveAt(i);
                        }
                    }
                }
            }
        }
    }

}

