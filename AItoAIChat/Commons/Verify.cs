using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AItoAIChat.Commons;
internal static class Verify
{
    public static void NotNull([NotNull] object? obj, [CallerArgumentExpression("obj")] string paramName = "")
    {
        _ = obj ?? throw new ArgumentNullException(paramName);
    }
}
