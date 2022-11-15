// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.Text.Json;

/*
 Ex 1 Citirea unei liste de liste de numere din
fisier

• Intr-un fisier text va fi salvata o lista
de liste de numere astfel: pe
fiecare linie va fi salvata cate o lista
de numere intregi
• Scrieti un program care sa citeasca
din fisier o astfel de lista a listelor
• Scrieti o functie care sa scrie in
fisier o astfel de lista a lsitelor

• Observatii:
• Fisierul va fi tratat ca unul text astfel
incat de pe fiecare linie sa fie citita
cate o lista de numere
• Nu serializati/deserializati intreaga
lista dintr-o singura instructiune
• Scopul acestui exercitiu este acela de
a exersa operatiile asupra fisierelor
text
 */



var path = @"C:\Users\seb\Desktop\fasttrackIT\Projects\Laborator 23\laborator23.txt";

List<int> list1 = new List<int> { 1, 2, 3, 4, 5 };
List<int> list2 = new List<int> { 6, 7, 8, 9, 10 };
List<int> list3 = new List<int> { 11, 12, 13, 14, 15 };
List<int> list4 = new List<int> { 16, 17, 18, 19, 20 };

List<List<int>> listOfLists = new List<List<int>>();
listOfLists.Add(list1);
listOfLists.Add(list2);
listOfLists.Add(list3);
listOfLists.Add(list4);


File.Delete(path);

//• Scrieti o functie care sa scrie in
//fisier o astfel de lista a lsitelor

WriteOnTxtFile(path, listOfLists);

//• Scrieti un program care sa citeasca
//din fisier o astfel de lista a listelor
ReadText(path);

static void WriteOnTxtFile(string path, List<List<int>> listOfLists)
{
    foreach (var list in listOfLists)
    {
        File.AppendAllText(path, ListSerializer(list));
        File.AppendAllText(path, "\n");
    }
}
static void ReadText(string path)
{
    foreach (var line in File.ReadLines(path))
    {
        Console.WriteLine(line);
    }

    //string text = File.ReadAllText(path);
    //Console.WriteLine(text);
}

static string ListSerializer(List<int> list)
{
    return JsonSerializer.Serialize(list);
}



/*
 Ex2 – tasks
• Scrieti o functie care va calcula in mod concurent suma tuturor
numerelor de la ex 1
• Pentru fiecare lista de numere din lista de numere se va lansa un task
individual!
• La finalul executiei, afisati fiecare suma numerelor
• Pentru a asigura corectitudinea si lipsa race-condition-urilor, rulati
functia de mai multe ori si observati rezultatul (10-100 ori)
 */


Ex2SumWithTaskForEveryLine(listOfLists);

static List<int> Ex2SumWithTaskForEveryLine(List<List<int>> listOfLists)
{
    List<int> resultList = new List<int>();

    Task<int> sum0 = Task.Factory.StartNew(() =>
    {
        var result = 0;
        foreach (var no in listOfLists[0])
        {
            result += no;
            Task.Delay(100);
            Console.Write(result + " ");
        }
        return result;
    });


    Task<int> sum1 = Task.Factory.StartNew(() =>
    {
        var result = 0;
        foreach (var no in listOfLists[1])
        {
            result += no;
            Task.Delay(1000);
            Console.Write(result + " ");
        }
        return result;
    });


    Task<int> sum2 = Task.Factory.StartNew(() =>
    {
        var result = 0;
        foreach (var no in listOfLists[2])
        {
            result += no;
            Task.Delay(10000);
            Console.Write(result + " ");
        }
        return result;
    });
    resultList.Add(sum2.Result);

    Task<int> sum3 = Task.Factory.StartNew(() =>
    {
        var result = 0;
        foreach (var no in listOfLists[3])
        {
            result += no;
            Task.Delay(1000);
            Console.Write(result + " ");
        }
        return result;
    });

    resultList.Add(sum0.Result);
    resultList.Add(sum1.Result);
    resultList.Add(sum2.Result);
    resultList.Add(sum3.Result);

    Console.WriteLine($"{sum0.Result} {sum1.Result} {sum2.Result} {sum3.Result}");

    return resultList;
};



static List<int> SumOfEveryLine(List<List<int>> listOfLists)
{
    List<int> resultList = new List<int>();
    foreach (var list in listOfLists)
    {
        Task<int> sum = Task.Factory.StartNew(() =>
        {
            var result = 0;
            foreach (var no in list)
            {
                result += no;
            }
            return result;
        });
        resultList.Add(sum.Result);
    }
    return resultList;
};



Console.WriteLine(ListSerializer(SumOfEveryLine(listOfLists)));


/*
 Ex3 – tasks
• Scrieti o functie care va calcula in
mod concurent suma fiecarei liste
individuale de numere din lista
citita la exercitiul 1
• Pentru fiecare lista de numere din
lista listelor de numere se va lansa un
task individual!
• La finalul executiei, afisati fiecare
suma in parte
• Calculati ulterior, in mod
concurrent, suma sumelor si
afisati-o!
• Observatii
• Metoda Task.WaitAll poate primi ca
parametru si un array de task-uri
• Pentru a obtine un array dintr-o lista
puteti folosi metoda .toarray
 */

Ex3SumWithTaskForEveryLine(listOfLists);

static List<int> Ex3SumWithTaskForEveryLine(List<List<int>> listOfLists)
{
    List<int> resultList = new List<int>();

    Task<int> sum0 = Task.Factory.StartNew(() =>
    {
        var result = 0;
        foreach (var no in listOfLists[0])
        {
            result += no;
            Console.Write(result + " ");
        }
        return result;
    });


    Task<int> sum1 = Task.Factory.StartNew(() =>
    {
        var result = 0;
        foreach (var no in listOfLists[1])
        {
            result += no;
            Console.Write(result + " ");
        }
        return result;
    });


    Task<int> sum2 = Task.Factory.StartNew(() =>
    {
        var result = 0;
        foreach (var no in listOfLists[2])
        {
            result += no;
            Console.Write(result + " ");
        }
        return result;
    });

    Task<int> sum3 = Task.Factory.StartNew(() =>
    {
        var result = 0;
        foreach (var no in listOfLists[3])
        {
            result += no;
            Console.Write(result + " ");
        }
        return result;
    });

    Task.WaitAll(sum0, sum1, sum2, sum3);
    Console.WriteLine();
    Console.WriteLine(sum0.Result + sum1.Result + sum2.Result + sum3.Result);

    resultList.Add(sum0.Result);
    resultList.Add(sum1.Result);
    resultList.Add(sum2.Result);
    resultList.Add(sum3.Result);

    Console.WriteLine($"{sum0.Result} {sum1.Result} {sum2.Result} {sum3.Result}");

    return resultList;
};




