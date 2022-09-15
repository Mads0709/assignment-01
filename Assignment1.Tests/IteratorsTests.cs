namespace Assignment1.Tests;

public class IteratorsTests
{
    [Fact]
    public void StreamOfTs(){

        //Arrange
        List<List<string>> l1 = new List<List<string>>();  

        List<string> list = new List<string>();
        list.Add("T");
        list.Add("T");

        l1.Add(list);

        //Act
        var evens = Iterators.Flatten(l1);

        //Assert
          evens.Should().BeEquivalentTo(new[] { "T", "T"});
    }

    [Fact]

    public void Predicate(){
        //Arrange
        List<int> l1 = new List<int>();
        l1.Add(2);
        l1.Add(5);

        Predicate<int> even = Even;

        bool Even(int i) => i % 2 == 0;

        //Act
        var evens = Iterators.Filter(l1, even);

        //Assert
         evens.Should().BeEquivalentTo(new[] {2});


    }
}



