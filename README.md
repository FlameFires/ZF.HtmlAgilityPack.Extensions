# ZF.HtmlAgilityPack.Extensions
ZF.HtmlAgilityPack.Extensions is an extension package for HtmlAgilityPack. It makes it easier to use HtmlAgilityPack via xpath.


## Usage

```csharp
string html = "<p class=\"f4 my-3\">ZF.HtmlAgilityPack.Extensions is an extension package for HtmlAgilityPack.</p >";

HtmlDocument doc = html.LoadHtml();
HtmlNode htmlNode = doc.Xpath("//p[@class='f4 my-3']");
HtmlNodeCollection htmlNodes = doc.XpathList("//p[@class='f4 my-3']");

Assert.NotNull(htmlNode);
Assert.NotNull(htmlNodes);
Assert.True(htmlNodes.Count > 0);

var classes = doc.XpathStr("//p[@class='f4 my-3']/@class");
_testOutputHelper.WriteLine(classes); // f4 my-3
Assert.NotEmpty(classes);
```
