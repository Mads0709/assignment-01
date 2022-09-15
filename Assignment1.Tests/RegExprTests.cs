namespace Assignment1.Tests;

public class RegExprTests
{

    [Fact]

    public void splitLines(){

        //Arrange
        List<string> list = new List<string>();
        list.Add("Hej med dig ");
        list.Add("Du hedder Kaj");
    
        //Act
        var iteratorOfWords = RegExpr.SplitLine(list);

        //Assert
        iteratorOfWords.Should().BeEquivalentTo(new[] {"Hej", "med", "dig", "Du", "hedder", "Kaj"});
    }

    [Fact]

    public void ResolutionsSplit(){

        //Arrange
        List<string> list = new List<string>();
        list.Add("1920x1080");
        list.Add("1024x768");


        //Act
        var res = RegExpr.Resolutions(list);

        //Assert
        res.Should().BeEquivalentTo(new[] {(1920, 1080), (1024,768)});


    }

     [Fact]
    public void InnerText_One_Match()
    {
        // Given
        string html = "<html>Test</html><a>link</a>";
        string tag = "html";
    
        // When
        var listOfTagHTML = RegExpr.InnerText(html, tag);
    
        // Then
        listOfTagHTML.Should().BeEquivalentTo(new[]{"Test"});
    }
    
    [Fact]
    public void InnerText_with_tag_a_returns_innertext()
    {
        // Given
        string html = "<div><p>A <b>regular expression</b>, <b>regex</b> or <b>regexp</b> (sometimes called a <b>rational expression</b>) is, in <a href=\"https://en.wikipedia.org/wiki/Theoretical_computer_science\" title=\"Theoretical computer science\">theoretical computer science</a> and <a href=\"https://en.wikipedia.org/wiki/Formal_language\" title=\"Formal language\">formal language</a> theory, a sequence of <a href=\"https://en.wikipedia.org/wiki/Character_(computing)\" title=\"Character (computing)\">characters</a> that define a <i>search <a href=\"https://en.wikipedia.org/wiki/Pattern_matching\" title=\"Pattern matching\">pattern</a></i>. Usually this pattern is then used by <a href=\"https://en.wikipedia.org/wiki/String_searching_algorithm\" title=\"String searching algorithm\">string searching algorithms</a> for \"find\" or \"find and replace\" operations on <a href=\"https://en.wikipedia.org/wiki/String_(computer_science)\" title=\"String (computer science)\">strings</a>.</p></div>";
        string tag = "a";

        // When
        var listOfTagA = RegExpr.InnerText(html, tag);

        // Then
        listOfTagA.Should().BeEquivalentTo(new[]{"theoretical computer science", "formal language", "characters", "pattern", "string searching algorithms", "strings"});
    }

    [Fact]
    public void InnerText_With_Nested_Text()
    {
        // Given
        string html = "<div><p>The phrase <i>regular expressions</i> (and consequently, regexes) is often used to mean the specific, standard textual syntax for representing <u>patterns</u> that matching <em>text</em> need to conform to.</p></div>";
        string tag = "p";

        // When
        var listOfTagP = RegExpr.InnerText(html, tag);
        
        // Then
        listOfTagP.Should().BeEquivalentTo(new[]{"The phrase regular expressions (and consequently, regexes) is often used to mean the specific, standard textual syntax for representing patterns that matching text need to conform to."});
    }

    [Fact]
    public void URLs_From_HTML()
    {
        // Given
        string html = "<div><p>A <b>regular expression</b>, <b>regex</b> or <b>regexp</b> (sometimes called a <b>rational expression</b>) is, in <a href=\"https://en.wikipedia.org/wiki/Theoretical_computer_science\" title=\"Theoretical computer science\">theoretical computer science</a> and <a href=\"https://en.wikipedia.org/wiki/Formal_language\" title=\"Formal language\">formal language</a> theory, a sequence of <a href=\"https://en.wikipedia.org/wiki/Character_(computing)\" title=\"Character (computing)\">characters</a> that define a <i>search <a href=\"https://en.wikipedia.org/wiki/Pattern_matching\" title=\"Pattern matching\">pattern</a></i>. Usually this pattern is then used by <a href=\"https://en.wikipedia.org/wiki/String_searching_algorithm\" title=\"String searching algorithm\">string searching algorithms</a> for \"find\" or \"find and replace\" operations on <a href=\"https://en.wikipedia.org/wiki/String_(computer_science)\" title=\"String (computer science)\">strings</a>.</p></div>";
        
        // When
        var listOfUrlsWithTitles = RegExpr.Urls(html);

        // Then
        listOfUrlsWithTitles.Should().BeEquivalentTo(new[]{
            (new Uri("https://en.wikipedia.org/wiki/Theoretical_computer_science"), "Theoretical computer science"),
            (new Uri("https://en.wikipedia.org/wiki/Formal_language"), "Formal language"),
            (new Uri("https://en.wikipedia.org/wiki/Character_(computing)"), "Character (computing)"),
            (new Uri("https://en.wikipedia.org/wiki/Pattern_matching"), "Pattern matching"),
            (new Uri("https://en.wikipedia.org/wiki/String_searching_algorithm"), "String searching algorithm"),
            (new Uri("https://en.wikipedia.org/wiki/String_(computer_science)"), "String (computer science)")
        });
    }

    [Fact]
    public void Simple_URLs_From_HTML()
    {
        // Given
        string html = "<a href=\"https://www.google.com\" title=\"Google\">test</a>";
        
        // When
        var listOfUrlsWithTitles = RegExpr.Urls(html);

        // Then
        listOfUrlsWithTitles.Should().BeEquivalentTo(new[]{
            (new Uri("https://www.google.com"), "Google")
        });
    }

    [Fact]
    public void URLs_Where_Title_Has_Parentheses()
    {
        // Given
        string html = "<a href=\"https://www.google.com\" title=\"Google (Alphabet)\">Title with parentheses</a>";
        
        // When
        var listOfUrlsWithTitles = RegExpr.Urls(html);

        // Then
        listOfUrlsWithTitles.Should().BeEquivalentTo(new[]{
            (new Uri("https://www.google.com"), "Google (Alphabet)")
        });
    }

    [Fact]
    public void HTTP_URL_works()
    {
        // Given
        string html = "<a href=\"http://www.google.com\" title=\"Google (Alphabet)\">Test with HTTP</a>";
        
        // When
        var listOfUrlsWithTitles = RegExpr.Urls(html);

        // Then
        listOfUrlsWithTitles.Should().BeEquivalentTo(new[]{
            (new Uri("http://www.google.com"), "Google (Alphabet)")
        });
    }

    [Fact]
    public void URLS_With_Parentheses()
    {
        // Given
        string html = "<a href=\"https://www.google.com/(Test)\" title=\"Google (Alphabet)\">URLs with parentheses</a>";
        
        // When
        var listOfUrlsWithTitles = RegExpr.Urls(html);

        // Then
        listOfUrlsWithTitles.Should().BeEquivalentTo(new[]{
            (new Uri("https://www.google.com/(Test)"), "Google (Alphabet)")
        });
    }

    [Fact]
    public void URLS_With_Preceding_Tag()
    {
        // Given
        string html = "<a test=\"Test\" href=\"https://www.google.com/\" title=\"Google (Alphabet)\">Test with preceding tag</a>";
        
        // When
        var listOfUrlsWithTitles = RegExpr.Urls(html);

        // Then
        listOfUrlsWithTitles.Should().BeEquivalentTo(new[]{
            (new Uri("https://www.google.com/"), "Google (Alphabet)")
        });
    }

    [Fact]
    public void URLS_With_Middle_Tag()
    {
        // Given
        string html = "<a href=\"https://www.google.com/\" test=\"Test\" title=\"Google (Alphabet)\">Urls with middle tag</a>";
        
        // When
        var listOfUrlsWithTitles = RegExpr.Urls(html);

        // Then
        listOfUrlsWithTitles.Should().BeEquivalentTo(new[]{
            (new Uri("https://www.google.com/"), "Google (Alphabet)")
        });
    }

    [Fact]
    public void URLS_With_Posterior_Tag()
    {
        // Given
        string html = "<a href=\"https://www.google.com/\" title=\"Google (Alphabet)\" test=\"Test\">Test with posterior tag</a>";
        
        // When
        var listOfUrlsWithTitles = RegExpr.Urls(html);

        // Then
        listOfUrlsWithTitles.Should().BeEquivalentTo(new[]{
            (new Uri("https://www.google.com/"), "Google (Alphabet)")
        });
    }

    [Fact]
    public void SplitLine_Given_List_Of_Strings_Return_List_Of_Words()
    {
        List<string> list = new List<string>
        {
            "Elisabeth, du bar os paa din ryg",
            "paa skuldrene af dig stod vi",
            "Endnu engang maa livet gaa",
            "mod Europas fald",
            "paa maa og faa"
        };
        var output = RegExpr.SplitLine(list);

        List<string> words = new List<string>
        {
            "Elisabeth",
            "du",
            "bar",
            "os",
            "paa",
            "din",
            "ryg",
            "paa",
            "skuldrene",
            "af",
            "dig",
            "stod",
            "vi",
            "Endnu",
            "engang",
            "maa",
            "livet",
            "gaa",
            "mod",
            "Europas",
            "fald",
            "paa",
            "maa",
            "og",
            "faa"
        };

        Assert.Equal(words, output);
    }

    [Fact]
    public void List_With_Resolutions_Returns_Tupled()
    {
        List<string> resolutions = new List<string> {"1920x1080", "1024x768, 800x600, 640x480", "320x200,", "320x240, 800x600", "1280x960"};

        IEnumerable<(int height, int width)> output = RegExpr.Resolutions(resolutions);

        List<(int, int)> tuples = new List<(int, int)>
        {
            (1920, 1080),
            (1024, 768),
            (800, 600),
            (640, 480),
            (320, 200),
            (320, 240),
            (800, 600),
            (1280, 960)
        };
        Assert.Equal(tuples, output);
    }

    [Fact]
    public void InnerText_Given_HTML_a_Tag_Return_List()
    {
        string html =
            "<div><p>A <b>regular expression</b>, <b>regex</b> or <b>regexp</b> (sometimes called a <b>rational expression</b>) is, in <a href=\"https://en.wikipedia.org/wiki/Theoretical_computer_science\" title=\"Theoretical computer science\">theoretical computer science</a> and <a href=\"https://en.wikipedia.org/wiki/Formal_language\" title=\"Formal language\">formal language</a> theory, a sequence of <a href=\"https://en.wikipedia.org/wiki/Character_(computing)\" title=\"Character (computing)\">characters</a> that define a <i>search <a href=\"https://en.wikipedia.org/wiki/Pattern_matching\" title=\"Pattern matching\">pattern</a></i>. Usually this pattern is then used by <a href=\"https://en.wikipedia.org/wiki/String_searching_algorithm\" title=\"String searching algorithm\">string searching algorithms</a> for \"find\" or \"find and replace\" operations on <a href=\"https://en.wikipedia.org/wiki/String_(computer_science)\" title=\"String (computer science)\">strings</a>.</p></div>";
        var output = RegExpr.InnerText(html, "a");
        List<string> shouldBe = new List<string>
        {
            "theoretical computer science",
            "formal language",
            "characters",
            "pattern",
            "string searching algorithms",
            "strings"
        };
        Assert.Equal(shouldBe, output);
    }
    
    [Fact]
    public void InnerText_Given_HTML_a_Tag_Nested_Return_List()
    {
        string html =
           "<div><p>The phrase <i>regular expressions</i> (and consequently, regexes) is often used to mean the specific, standard textual syntax for representing <u>patterns</u> that matching <em>text</em> need to conform to.</p></div>";
        var output = RegExpr.InnerText(html, "p");
        List<string> shouldBe = new List<string>
        {
            "The phrase regular expressions (and consequently, regexes) is often used to mean the specific, standard textual syntax for representing patterns that matching text need to conform to."
        };
        Assert.Equal(shouldBe, output);
    }

    [Fact]
    public void URLs_Without_Title()
    {
        // Given
        string html = "<a href=\"https://www.google.com\">Inner Text</a>";
    
        // When
        var tuples = RegExpr.Urls(html);
    
        // Then
        tuples.Should().BeEquivalentTo(new[]{(new Uri("https://www.google.com"), "Inner Text")});
    }

    [Fact]
    public void URL_With_Title_Before_Href_Attribute()
    {
        // Given
        string html = "<a title=\"This is a title before link\" href=\"https://www.facebook.com\">Inner Text</a>";
    
        // When
        var tuples = RegExpr.Urls(html);
    
        // Then
        tuples.Should().BeEquivalentTo(new[]{(new Uri("https://www.facebook.com"), "This is a title before link")});
    }
}
