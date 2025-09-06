using System;

namespace Buhoborec.Application.Common.Interfaces;

public interface IDateTime
{
    DateTime UtcNow { get; }
}
