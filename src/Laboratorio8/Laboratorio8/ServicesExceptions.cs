using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Laboratorio8
{
    [Serializable]
    public class ServicesExceptions : Exception
    {
        public class ServicesExceptionsDetails
        {
            public Dictionary<string, StringBuilder> Errori { get; set; }

            public void AddSummary(string message)
            {
                AddError(string.Empty, message);
            }

            public void AddKey(string key)
            {
                AddError(key, string.Empty);
            }

            public void AddError(string key, string message)
            {
                if (!Errori.ContainsKey(key)) Errori[key] = new StringBuilder();
                Errori[key].AppendLine(message);
            }

            [DebuggerStepThrough]
            public ServicesExceptionsDetails()
            {
                Errori = new Dictionary<string, StringBuilder>();
            }

            public bool HasErrors
            {
                get
                {
                    return Errori.Count() > 0;
                }
            }
        }

        private ServicesExceptionsDetails Exceptions { get; set; }

        public Dictionary<string, StringBuilder> Errori
        {
            get { return Exceptions.Errori; }
        }

        [DebuggerStepThrough]
        public ServicesExceptions(ServicesExceptionsDetails exceptions)
            : base()
        {
            Exceptions = exceptions;
        }

        [DebuggerStepThrough]
        public ServicesExceptions(string message)
            : base(message)
        {
            Exceptions = new ServicesExceptionsDetails();
            Exceptions.AddSummary(message);
        }

        [DebuggerStepThrough]
        public ServicesExceptions(string key, string message)
            : base(message)
        {
            Exceptions = new ServicesExceptionsDetails();
            Exceptions.AddError(key, message);
        }
    }

    [Serializable]
    public class ServicesAlertExceptions : Exception
    {

        [DebuggerStepThrough]
        public ServicesAlertExceptions(string message)
            : base(message)
        { }
    }

}