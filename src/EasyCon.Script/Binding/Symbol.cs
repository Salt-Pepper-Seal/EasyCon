using EasyCon.Script.Parsing;
using System.Collections.Immutable;

namespace EasyCon.Script.Binding;

abstract class Symbol
{
    public readonly string Name;
    protected Symbol(string name)
    {
        Name = name;
    }
}

public enum ValueType
{
    Void,
    Int,
    Bool,
    String,
    Array,
}

abstract class VariableSymbol : Symbol
{
    public readonly ValueType Type;
    public readonly bool IsReadOnly;
    internal object? Value { get; }

    protected VariableSymbol(string name, bool isReadOnly, ValueType valueType)
        : base(name)
    {
        Type = valueType;
        IsReadOnly = isReadOnly;
    }
}

sealed class GlobalVariableSymbol : VariableSymbol
{
    public GlobalVariableSymbol(string name, bool isReadOnly, ValueType valueType)
        : base(name, isReadOnly, valueType)
    {
    }
}

class LocalVariableSymbol : VariableSymbol
{
    public LocalVariableSymbol(string name, bool isReadOnly, ValueType valueType)
        : base(name, isReadOnly, valueType)
    {
    }
}

sealed class ParamSymbol : LocalVariableSymbol
{
    public int Ordinal { get; }

    public ParamSymbol(string name, ValueType valueType, int ordinal = 0)
        : base(name, true, valueType)
    {
        Ordinal = ordinal;
    }
}

sealed class FunctionSymbol : Symbol
{
    public readonly ImmutableArray<ParamSymbol> Paramters;
    public readonly FuncDeclBlock? Declaration;
    public readonly ValueType Type;

    public FunctionSymbol(string name, ImmutableArray<ParamSymbol> paramters, ValueType type, FuncDeclBlock? declaration = null)
        : base(name)
    {
        Paramters = paramters;
        Declaration = declaration;
        Type = type;
    }
}
