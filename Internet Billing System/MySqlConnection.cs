using System;

internal class MySqlConnection
{
    private object toString;

    public MySqlConnection(object toString)
    {
        this.toString = toString;
    }

    public string ConnectionString { get; internal set; }

    internal void Close()
    {
        throw new NotImplementedException();
    }

    internal object Dispose()
    {
        throw new NotImplementedException();
    }

    internal void Open()
    {
        throw new NotImplementedException();
    }
}