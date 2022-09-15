namespace Assignment1;
using System.Text.RegularExpressions;
public static class RegExpr
{
    public static IEnumerable<string> SplitLine(IEnumerable<string> lines){
        foreach(var line in lines){
            var strRegex = @"\b[a-zA-Z0-9]+\b"; //\b matcher slut og start bogstav 
            foreach(var word in Regex.Matches(line, strRegex)){
                yield return word.ToString();
            }
        }
    }

    public static IEnumerable<(int width, int height)> Resolutions(IEnumerable<string> resolutions){
        foreach(string res in resolutions){
            var strRegex = @"(?<x>[0-9]+)x(?<y>[0-9]+)"; //group by x and y, numbers from 0-9, the + is so it looks for more than just one number, the x is so it onæy looks for an x
            foreach(Match resSplit in Regex.Matches(res, strRegex)){
            int x = Int32.Parse(resSplit.Groups["x"].Value);
            int y = Int32.Parse(resSplit.Groups["y"].Value);

            yield return (x,y);
        }
    }
}

    public static IEnumerable<string> InnerText(string html, string tag) {
        var matchpattern = "<"+tag+"[^>]*>(?<innertext>.*?)</"+tag+">";
        var removepattern = "<.*?>";

        foreach (Match match in Regex.Matches(html, matchpattern)) {
            string matchtext = match.Groups["innertext"].Value;
            yield return Regex.Replace(matchtext, removepattern, "");
        }
    }

     public static IEnumerable<(Uri url, string title)> Urls(string html) {
        var pattern = "<a(( *href=\"(?<url>.*?)\" *| *title=\"(?<title>.*?)\" *)| *[^<>]*?=\".*?\" *?)*>(?<innertext>.*?)</a>";
        // var pattern = "<a[.((?:href=\")https?://[\\w\\./]*)()]*>(?<innerText>[^<]*)</a>";
        // var pattern = "<a(?<attributes>.*)>(?<innerText>.*)</a>";

        foreach (Match match in Regex.Matches(html, pattern)) {
            // yield return (new Uri(match.ToString()), match.ToString());
            yield return (new Uri(match.Groups["url"].Value), match.Groups["title"].Value != "" ? match.Groups["title"].Value : match.Groups["innertext"].Value);
            // yield return (new Uri(match.Groups["url"].Value), match.Groups["title"].Value);
        }
    }
}