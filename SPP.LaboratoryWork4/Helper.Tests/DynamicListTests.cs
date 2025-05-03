namespace Helper.Tests;

public class DynamicListTests
{
    [Fact]
    public void Constructor_WithCapacity_SetsListCapacity()
    {
        var list = new DynamicList<int>(50);
        
        Assert.Equal(50, list.Capacity);
        Assert.Equal(0, list.Length);
    }

    [Fact]
    public void Constructor_Default_SetsListDefaultCapacity()
    {
        var list = new DynamicList<int>();
        
        Assert.Equal(16, list.Capacity);
        Assert.Equal(0, list.Length);
    }

    [Fact]
    public void Add_WithinCapacity_AddsItemsWithoutResizing()
    {
        const int a = 1;
        const int b = 2;
        const int c = 3;
        var list = new DynamicList<int>(3) { a, b, c };

        Assert.Equal(3, list.Capacity);
        Assert.Equal(3, list.Length);
        Assert.Equal(a, list[0]);
        Assert.Equal(b, list[1]);
        Assert.Equal(c, list[2]);
    }

    [Fact]
    public void Add_ExceedingCapacity_AddsItemsWithDoublingSize()
    {
        const int a = 1;
        const int b = 2;
        const int c = 3;
        const int d = 4;
        var list = new DynamicList<int>(3) { a, b, c, d };

        Assert.Equal(6, list.Capacity);
        Assert.Equal(4, list.Length);
        Assert.Equal(a, list[0]);
        Assert.Equal(b, list[1]);
        Assert.Equal(c, list[2]);
        Assert.Equal(d, list[3]);
    }

    [Fact]
    public void Remove_ExistingItem_ReturnsTrue()
    {
        const int a = 1;
        var list = new DynamicList<int>(2) { a };

        var result = list.Remove(a);
        
        Assert.Equal(2, list.Capacity);
        Assert.Equal(0, list.Length);
        Assert.True(result);
    }
    
    [Fact]
    public void Remove_NonExistingItem_ReturnsFalse()
    {
        const int a = 1;
        const int b = 2;
        var list = new DynamicList<int>(2) { a };

        var result = list.Remove(b);
        
        Assert.Equal(2, list.Capacity);
        Assert.Equal(1, list.Length);
        Assert.False(result);
    }
    
    [Fact]
    public void RemoveAt_ValidIndex_ReturnsTrue()
    {
        const int a = 1;
        const int b = 2;
        const int c = 3;
        var list = new DynamicList<int>(3) { a, b, c };

        var result = list.RemoveAt(a);
        
        Assert.Equal(3, list.Capacity);
        Assert.Equal(2, list.Length);
        Assert.Equal(a, list[0]);
        Assert.NotEqual(b, list[1]);
        Assert.Equal(c, list[1]);
        Assert.True(result);
    }
    
    [Fact]
    public void RemoveAt_InvalidIndex_ReturnsFalse()
    {
        const int a = 1;
        const int b = 2;
        var list = new DynamicList<int>(2) { a, b };

        Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(-1));
        Assert.Throws<IndexOutOfRangeException>(() => list.RemoveAt(2));
    }

    [Fact]
    public void Clear_RemovesAllItems()
    {
        var a = "a";
        var b = "b";
        var list = new DynamicList<string>() { a, b };
        
        list.Clear();
        
        Assert.Equal(16, list.Capacity);
        Assert.Equal(0, list.Length);
    }
}