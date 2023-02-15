using Xunit;

namespace Tester;
using Connect4;

public class C4Test {

    [Fact]
    public void testing(){
        Assert.Equal(returnValPlus10(15), 25);
        Assert.Equal(returnValPlus10(6), 16);
        Assert.Equal(returnValPlus10(8), 18);

    }

    public int returnValPlus10(int val) {
        return val + 10;
    }

    [Fact]
    public void testC4(){
        int[,] temp = new int[2,2];
        Assert.Equal(Connect4.MainMenu.promptCol(1), 1);
    }

}