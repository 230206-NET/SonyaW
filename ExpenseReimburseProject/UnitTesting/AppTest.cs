using UI;
using Xunit;

namespace UnitTesting;

public class AppTest {

    [Fact]
    public void EmailValidationSuccessful()
    {
        Assert.Equal(UI.MainMenu.ValidateAndReturnEmail("hello"), "");
        Assert.Equal(UI.MainMenu.ValidateAndReturnEmail("hello@"), "");
        Assert.Equal(UI.MainMenu.ValidateAndReturnEmail("hello@gmail"), "");
        Assert.Equal(UI.MainMenu.ValidateAndReturnEmail("hello@gmail."), "");
        Assert.Equal(UI.MainMenu.ValidateAndReturnEmail("hello@gmail.com"), "hello@gmail.com");
    }

    [Fact]
    public void NameValidationSuccessful()
    {
        Assert.Equal(UI.MainMenu.ValidateAndReturnName("hello"), "");
        Assert.Equal(UI.MainMenu.ValidateAndReturnName("hello "), "");
        Assert.Equal(UI.MainMenu.ValidateAndReturnName("hello world"), "Hello World");
    }

}