using System;
using System.Collections.Generic;

namespace LoadVIS.Database;

public partial class IndexScript
{
    public string DatabaseName { get; set; } = null!;

    public string TableName { get; set; } = null!;

    public string IndexName { get; set; } = null!;

    public string? CreateIndexScript { get; set; }

    public long? UserSeeks { get; set; }

    public long? UserScans { get; set; }

    public long? UserLookups { get; set; }

    public long? UserUpdates { get; set; }

    public DateTime? LastUserSeek { get; set; }

    public DateTime? LastUserScan { get; set; }

    public DateTime? LastUserLookup { get; set; }
}
