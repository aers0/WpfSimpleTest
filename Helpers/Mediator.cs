using System;
using System.Collections.Generic;

namespace WpfSimpleTest.Helpers
{
    public enum ViewModelMessages { SelectedTab = 0 };
    public sealed class Mediator
    {
        static readonly Mediator instance = new Mediator();

        Dictionary<ViewModelMessages, Action<object>> internalList
            = new Dictionary<ViewModelMessages, Action<object>>();

        public static Mediator Instance
        {
            get
            {
                return instance;
            }
        }

        public void Register(Action<object> callback,
            ViewModelMessages message)
        {
            internalList.Add(message, callback);
        }

        public void NotifyColleagues(ViewModelMessages message, object args)
        {
            if (internalList.ContainsKey(message))
            {
                foreach (Action<object> callback in internalList.Values)
                    callback(args);
            }
        }

    }
}
