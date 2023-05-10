using NGpt.Domain.Edit;

namespace NGpt.Test;

public class EditTests
{
    Edit _edit;

    public EditTests()
    {
        _edit = new Edit("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", "org-UpMJfYAwK3diGzF1OVSVLb1e");
    }

    [Fact]
    public void ShouldModifyText()
    {
        var response = _edit.EditText("To bee or not to be.", "fix spelling");

        Assert.Equal("To be or not to be.\n", response);
    }

    [Fact]
    public void ShouldModifyCode()
    {
        var response = _edit.EditCode("Console.WriteLine(\"Test\");", "Change it to dispay text: \"aaa\"");

        Assert.Equal("Console.WriteLine(\"aaa\");\n", response);        
    }

    [Fact]
    public void ShouldEditContent()
    {
        var request = new EditRequest(EditModel.TextDavinciEdit001, "Change it to dispay text: \"aaa\"")
        {
            Input = "Console.WriteLine(\"Test\");",
            Temperature = 0,
            N = 1,
            TopP = 1
        };

        var response = _edit.EditContent(request);

        Assert.Equal("Console.WriteLine(\"aaa\");\n", response.Choices.First().Text);
    }
}
