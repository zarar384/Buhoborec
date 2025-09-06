using System;
using Buhoborec.Domain.Enums;

namespace Buhoborec.Domain.Entities;

public class WorkLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserId { get; set; } = null!;
    public DateTime Timestamp { get; set; }
    public WorkLogType Type { get; set; }
}
