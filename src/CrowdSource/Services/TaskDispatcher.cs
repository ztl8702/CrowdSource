﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowdSource.Models.CoreModels;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace CrowdSource.Services
{
    /// <summary>
    /// Used to control concurrency in dispatching tasks.
    /// Should be a singleton
    /// </summary>
    public class TaskDispatcher : ITaskDispatcher
    {
        // ToDo -> Doing -> ToReview -> Reviewing -> Done
        //                      ^                     |
        //                      |                     |
        //                      ----------------------- 

        private Queue<QueueMember> _queueToDo = new Queue<QueueMember>();
        private HashSet<QueueMember> _setDoing = new HashSet<QueueMember>();
        private Queue<QueueMember> _queueToReview = new Queue<QueueMember>();
        private HashSet<QueueMember> _setReviewing = new HashSet<QueueMember>();
        private readonly TimeSpan DoingTimeOut = new TimeSpan(0,15,0); // 15min
        private readonly TimeSpan ReviewTimeout = new TimeSpan(0, 5, 0); //5min

        // Lock for thread safety
        // Because ASP.NET MVC will handle requests in multiple threads
        readonly object _locker = new object();

        private readonly ILogger<TaskDispatcher> _logger;

        public TaskDispatcher(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TaskDispatcher>();
            _logger.LogInformation("Starting a TaskDispatcher Instance");

            for (int i = 1; i<=100; ++i)
            {
                var t = new Group();
                t.GroupId = i;
                _queueToDo.Enqueue(new QueueMember(t));
            }

            _logger.LogInformation($"Queue length: {_queueToDo.Count}");

            ScheduleTimers();
        }

        public Group GetNextToDo()
        {
            lock (_locker)
            {
                if (_queueToDo.Count > 0)
                {
                    var todo = _queueToDo.Dequeue();
                    _setDoing.Add(new QueueMember(todo.group));
                    return todo.group;
                } else
                {
                    return null;
                }
            }
        }

        public Group GetNextReview()
        { throw new NotImplementedException(); }

        private void CleanUpDoing()
        {
            lock (_locker)
            {
                foreach (var member in _setDoing)
                {
                    if ((member.added - DateTime.Now) > DoingTimeOut)
                    {
                        _setDoing.Remove(member);
                        _queueToDo.Enqueue(new QueueMember(member.group));
                    }
                }
            }
        }

        private void CleanUpReviewing()
        {
            lock (_locker)
            {
                foreach (var member in _setReviewing)
                {
                    if ((member.added - DateTime.Now) > ReviewTimeout)
                    {
                        _setReviewing.Remove(member);
                        _queueToReview.Enqueue(new QueueMember(member.group));
                    }
                }
            }
        }

        private void LoadToDoFromDB()
        {

        }

        public void Done(Group group)
        {
            lock (_locker)
            {
                var member = _setDoing.Where(m => m.group.GroupId == group.GroupId).Single();
                if (member != null)
                {
                    _setDoing.Remove(member);
                    _queueToReview.Enqueue(new QueueMember(member.group));
                }
            }
        }

        public void DoneReview(Group group)
        {
            lock (_locker)
            {
                var member = _setReviewing.Where(m => m.group.GroupId == group.GroupId).Single();
                if (member != null)
                {
                    _setReviewing.Remove(member);
                 
                }
            }
        }


        private void ScheduleTimers()
        {
            var aTimer = new Timer(a => {
                _logger.LogInformation("Timer");
                CleanUpDoing();
                CleanUpReviewing();
            },null,0,10000);
        }
    }

    public interface ITaskDispatcher
    {
        Group GetNextReview();
        /// <summary>
        /// 取得下一个需要识别的词条。
        /// </summary>
        /// <returns></returns>
        Group GetNextToDo();
        /// <summary>
        /// 标示一个词条识别完毕。
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        void Done(Group group);
        void DoneReview(Group group);
    }
    class QueueMember
    {
        public Group group { get; set; }
        public DateTime added { get; set; }
        public QueueMember(Group group)
        {
            this.group = group;
            this.added = DateTime.Now;
        }

    }
}