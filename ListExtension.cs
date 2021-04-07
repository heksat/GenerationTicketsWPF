using System;
using System.Collections.Generic;
using System.Text;

namespace GenerationTicketsWPF
{
    public static class ListExtension
    {
        public static Type GetTypeElement<T>(this List<T> plist)
        {
            return typeof(T);
        }
    }
}
