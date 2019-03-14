[System.Serializable]
public struct HexCoordinates
{
    private int _x;
    public int X
    {
        get { return _x; }
    }

    private int _z;
    public int Z
    {
        get { return _z; }
    }
    
    public int Y
    {
        get {return -X - Z;}
    }
    
    public HexCoordinates(int x, int z)
    {
        _x = x;
        _z = z;
    }
    
    public static HexCoordinates FromOffsetCoordinates (int x, int z) {
        return new HexCoordinates(x - z / 2, z);
    }
    
    public override string ToString()
    {
        return "(" + X + ", " + Y + ", " + Z + ")";
    }
    
    public string ToStringOnSeparateLines()
    {
        return X + "\n" + Y + "\n" + Z;
    }
}
