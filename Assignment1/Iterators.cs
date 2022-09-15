namespace Assignment1;


public static class Iterators
{
    public static IEnumerable<T> Flatten<T>(IEnumerable<IEnumerable<T>> items) {

        foreach(IEnumerable<T> list in items){
            foreach(T item in list)
            yield return item;
            }
        }

    public static IEnumerable<T> Filter<T>(IEnumerable<T> items, Predicate<T> predicate){

        foreach(T item in items){
            if(predicate(item)){ //check if it's a predicate
                yield return item;
            }
        }
    }
}
     