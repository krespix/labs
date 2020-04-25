using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Laba11;

namespace Laba11
{
    internal class Program
    {
        private const string MainMenu = "1.Task1 List<> \n2.Task2 SortedDictionary \n3.List<> and SortedDictionary \n0.Exit";

        private const string TaskMenu =
            "Выберите пункт меню: \n1. Добавить элемент \n2. Удалить элемент \n3. Кол-во элементов заданного класса \n4. Печать элементов опр. класса \n5. Нахождение класса с самым большим кол-вом объектов \n" +
            "6. Вывести коллекцию (foreach) \n7. Клонирование коллекции \n8. Сортировка и поиск примерной цене\n0. Выход в главное меню";

        private const string SideMenu = "1.Main menu\n2.Exit";
        private const string ExcistError = "Hashtable doesn't excist!";
        private const string KeyDosntExcist = "Key doesn't excist!";
        private const string KeyAlreadyExcist = "Key already excist!";

        public static void Main(string[] args)
        {
            int userChoice;
            int size;
            do
            {
                Console.WriteLine(MainMenu);
                userChoice = InputNumber("", 1, 3);
                Console.Clear();
                switch (userChoice)
                {
                    case 1:
                        size = InputNumber("Введите кол-во элементов", 0, 1000000000);
                        List<Goods> list = new List<Goods>(size);
                        for (int i = 0; i < size; i++)
                        {
                            list.Add(CreateRandomGood());
                        }
                        MenuTask1(list);
                        break;
                    
                    case 2:
                        size = InputNumber("Введите кол-во элементов", 0, 1000000000);
                        SortedDictionary<string, Goods> sortedDictionary = new SortedDictionary<string, Goods>();
                        for (int i = 0; i < size; i++)
                        {
                            sortedDictionary.Add("key number" + i, CreateRandomGood());
                        }
                        MenuTask2(sortedDictionary);
                        break;
                    
                    case 3:
                        size = InputNumber("Введите кол-во элементов", 0, 1000000000);
                        TestCollection collection = new TestCollection(size);
                        MenuTask3(collection);
                        break;
                }

            } while (userChoice != 0);
        }

        public static Goods CreateRandomGood()
        {
            Random random = new Random();
            int numberOfGood = random.Next(1, 4);
            if (numberOfGood == 1)
                return new Product();
            if (numberOfGood == 2)
                return new Toy();
            return new MilkProduct();
        }

        public static int InputNumber(string text, int left, int right)
        {
            int number = 0;
            var ok = false;
            Console.WriteLine(text);
            do
            {
                try
                {
                    number = Int32.Parse(Console.ReadLine());
                    if (number <= right && number >= left)
                        ok = true;
                    else
                    {
                        Console.WriteLine("Ошибка! Введено число за пределами границ!");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка! Неверно введено число!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка! Некорректный ввод!");
                }
            } while (!ok);

            return number;
        }

        public static void MenuTask1(List<Goods> list)
        {
            int userChoice;
            Console.Clear();

            do
            {
                Console.WriteLine(TaskMenu);
                userChoice = InputNumber("", 0, 8);
                Console.Clear();

                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine("Выберите тип продукта: \n1)Product \n2)Toy \n3)MilkProduct");
                        int choose = InputNumber("", 1, 3);

                        switch (choose)
                        {
                            case 1:
                                list.Add(new Product());
                                break;
                            case 2:
                                list.Add(new Toy());
                                break;
                            case 3:
                                list.Add(new MilkProduct());
                                break;
                        }

                        Console.WriteLine("Элемент добавлен.");
                        break;

                    case 2:
                        int case2 = InputNumber("Введите номер удаляемого элемента", 1, list.Count);
                        list.RemoveAt(case2 - 1);
                        Console.WriteLine($"Элемент с номером {case2} удален");
                        break;

                    case 3:
                        Console.WriteLine("Выберите тип продукта: \n1)Product \n2)Toy \n3)MilkProduct");
                        int case3 = InputNumber("", 1, 3);
                        int counter = 0;

                        switch (case3)
                        {
                            case 1:
                                foreach (var good in list)
                                {
                                    if (good is Product)
                                        counter++;
                                }

                                break;
                            case 2:
                                foreach (var good in list)
                                {
                                    if (good is Toy)
                                        counter++;
                                }

                                break;
                            case 3:
                                foreach (var good in list)
                                {
                                    if (good is MilkProduct)
                                        counter++;
                                }

                                break;
                        }

                        Console.WriteLine($"Количество элементов заданного типа: {counter}");
                        break;

                    case 4:
                        Console.WriteLine("Выберите тип продукта: \n1)Product \n2)Toy \n3)MilkProduct");
                        int case4 = InputNumber("", 1, 3);

                        foreach (var good in list)
                        {
                            if (good is Product && case4 == 1)
                                Console.WriteLine(good.ToString());
                            if (good is Toy && case4 == 2)
                                Console.WriteLine(good.ToString());
                            if (good is MilkProduct && case4 == 3)
                                Console.WriteLine(good.ToString());
                        }

                        break;

                    case 5:
                        int prodCount = 0;
                        int toyCount = 0;
                        int milkPordCount = 0;

                        foreach (var good in list)
                        {
                            if (good is Product)
                                prodCount++;
                            if (good is MilkProduct)
                                milkPordCount++;
                            if (good is Toy)
                                toyCount++;
                        }

                        int max = Math.Max(prodCount, Math.Max(milkPordCount, toyCount));

                        if (max == milkPordCount)
                            Console.WriteLine($"Количество MilkProduct - max : {milkPordCount}");
                        else if (max == toyCount)
                            Console.WriteLine($"Количество Toy - max : {toyCount}");
                        else if (max == prodCount)
                            Console.WriteLine($"Количество Porduct - max : {prodCount}");
                        break;

                    case 6:
                        foreach (var VARIABLE in list)
                        {
                            Console.WriteLine(VARIABLE.ToString());
                        }

                        break;

                    case 7:
                        var copy = new List<Goods>(list.Count);

                        foreach (var good in list)
                        {
                            copy.Add((Goods) good.Clone());
                        }

                        Console.WriteLine("Коллекция успешно скопирована");
                        break;

                    case 8:
                        list.Sort();
                        Console.WriteLine("Сортировка выполнена");
                        int price = InputNumber("Введите примерную цену(поиск товаров с ценой +-50):", 50, 9999999);
                        int count = 0;
                        //find
                        foreach (var good in list)
                        {
                            if (Math.Abs(good.Cost - price) <= 50)
                            {
                                count++;
                                Console.WriteLine($"Совпадение {count}: \n{good.ToString()}");
                            }
                        }

                        if (count == 0)
                        {
                            Console.WriteLine("Совпадений по цене не найдено");
                        }

                        break;

                    case 0:
                        break;

                }
            } while (userChoice != 0);
        }

        public static void MenuTask2(SortedDictionary<string, Goods> sortedDictionary)
        {
            int userChoice;
            string key;
            Console.Clear();

            do
            {
                Console.WriteLine(TaskMenu);
                userChoice = InputNumber("", 0, 8);
                Console.Clear();

                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine("Выберите тип продукта: \n1)Product \n2)Toy \n3)MilkProduct");
                        int choose = InputNumber("", 1, 3);
                        do
                        {
                            key = Console.ReadLine();
                        } while (sortedDictionary.ContainsKey(key));

                        switch (choose)
                        {
                            case 1:
                                sortedDictionary.Add(key, new Product());
                                break;
                            case 2:
                                sortedDictionary.Add(key, new Toy());
                                break;
                            case 3:
                                sortedDictionary.Add(key, new MilkProduct());
                                break;
                        }
                        
                        break;
                    
                    case 2:
                        do
                        {
                            key = Console.ReadLine();
                        } while (!sortedDictionary.ContainsKey(key));

                        sortedDictionary.Remove(key);
                        Console.WriteLine("Объект удален");
                        break;
                    
                    case 3:
                        Console.WriteLine("Выберите тип продукта: \n1)Product \n2)Toy \n3)MilkProduct");
                        int case3 = InputNumber("", 1, 3);
                        int counter = 0;

                        switch (case3)
                        {
                            case 1:
                                foreach (var good in sortedDictionary)
                                {
                                    if (good.Value is Product)
                                        counter++;
                                }

                                break;
                            case 2:
                                foreach (var good in sortedDictionary)
                                {
                                    if (good.Value is Toy)
                                        counter++;
                                }

                                break;
                            case 3:
                                foreach (var good in sortedDictionary)
                                {
                                    if (good.Value is MilkProduct)
                                        counter++;
                                }

                                break;
                        }

                        Console.WriteLine($"Количество элементов заданного типа: {counter}");
                        break;
                    
                    case 5:
                        int prodCount = 0;
                        int toyCount = 0;
                        int milkPordCount = 0;

                        foreach (var good in sortedDictionary)
                        {
                            if (good.Value is Product)
                                prodCount++;
                            if (good.Value is MilkProduct)
                                milkPordCount++;
                            if (good.Value is Toy)
                                toyCount++;
                        }

                        int max = Math.Max(prodCount, Math.Max(milkPordCount, toyCount));

                        if (max == milkPordCount)
                            Console.WriteLine($"Количество MilkProduct - max : {milkPordCount}");
                        else if (max == toyCount)
                            Console.WriteLine($"Количество Toy - max : {toyCount}");
                        else if (max == prodCount)
                            Console.WriteLine($"Количество Porduct - max : {prodCount}");
                        break;
                    
                    case 6:
                        foreach (var kv in sortedDictionary)
                        {
                            Console.WriteLine($"{kv.Key}) {kv.Value.ToString()}");
                        }
                        
                        break;
                    
                    case 7:
                        var copy = new SortedDictionary<string, Goods>();

                        foreach (var good in sortedDictionary)
                        {
                            copy.Add(good.Key, (Goods)good.Value.Clone());
                        }

                        Console.WriteLine("Коллекция успешно скопирована");
                        break;
                    
                    case 8:
                        Console.WriteLine("Коллекция уже отсортирована");
                        int price = InputNumber("Введите примерную цену(поиск товаров с ценой +-50):", 50, 9999999);
                        int count = 0;
                        //find
                        foreach (var good in sortedDictionary)
                        {
                            if (Math.Abs(good.Value.Cost - price) <= 50)
                            {
                                count++;
                                Console.WriteLine($"Совпадение {count}: \n{good.Value.ToString()}");
                            }
                        }

                        if (count == 0)
                        {
                            Console.WriteLine("Совпадений по цене не найдено");
                        }

                        break;
                }

            } while (userChoice != 0);
        }

        public static void MenuTask3(TestCollection collection)
        {
            int userChoice;
            Console.Clear();

            do
            {
                Console.WriteLine(
                    "1. Добавить элемент \n2. Измерить время нахождения \n3. Вывести коллекцию \n4.Удалить элемент \n0. Выход в главное меню");
                userChoice = InputNumber("", 0, 4);
                Console.Clear();

                switch (userChoice)
                {
                    case 1:
                        string key;
                        Console.WriteLine("Введите уникальный ключ: ");
                        do
                        {
                            key = Console.ReadLine();
                        } while (collection.collection_1Int.Contains(key));
                        
                        Console.WriteLine("Выберите тип продукта: \n1)Product \n2)Toy \n3)MilkProduct");
                        int choose = InputNumber("", 1, 3);

                        switch (choose)
                        {
                            case 1:
                                Product tmp = new Product();
                                collection.collection_1Int.Add(key);
                                collection.collection_1TKey.Add(tmp.BaseGood);
                                collection.collection_2StringTValue.Add(key, tmp);
                                collection.collection_2TKeyTValue.Add(tmp.BaseGood, tmp);
                                break;
                            case 2:
                                Toy tmp2 = new Toy();
                                collection.collection_1Int.Add(key);
                                collection.collection_1TKey.Add(tmp2.BaseGood);
                                collection.collection_2StringTValue.Add(key, tmp2);
                                collection.collection_2TKeyTValue.Add(tmp2.BaseGood, tmp2);
                                break;
                            case 3:
                                MilkProduct tmp3 = new MilkProduct();
                                collection.collection_1Int.Add(key);
                                collection.collection_1TKey.Add(tmp3.BaseGood);
                                collection.collection_2StringTValue.Add(key, tmp3);
                                collection.collection_2TKeyTValue.Add(tmp3.BaseGood, tmp3);
                                break;
                        }
                        
                        break;
                    
                    case 2:
                        Console.WriteLine(
                            "Скорость нахождения первого, центрального, последнего и несуществующего элемента в коллекциях 1 типа:");

                        //first
                        Console.WriteLine("Collection List<String>");
                        var first = collection.collection_1Int[0];
                        var mid = collection.collection_1Int[collection.collection_1Int.Count / 2];
                        var last = collection.collection_1Int[collection.collection_1Int.Count - 1];
                        var invalid = "djkfghskgneibnvdfvjeprv";
                        
                        Stopwatch stopwatch = new Stopwatch();
                        TimeSpan timeSpan;
                        
                        stopwatch.Start();
                        var contains = collection.collection_1Int.Contains(first);
                        contains = collection.collection_1Int.Contains(mid);
                        contains = collection.collection_1Int.Contains(last);
                        contains = collection.collection_1Int.Contains(invalid);
                        stopwatch.Stop();
                        
                        timeSpan = stopwatch.Elapsed;
                        Console.WriteLine("Время поиска:" + timeSpan);
                        
                        //second
                        Console.WriteLine("Collection List<Goods>");
                        var f = collection.collection_1TKey[0];
                        var m = collection.collection_1TKey[collection.collection_1TKey.Count / 2];
                        var l = collection.collection_1TKey[collection.collection_1TKey.Count - 1];
                        var inv = new Toy();
                        stopwatch = new Stopwatch();
                        
                        stopwatch.Start();
                        contains = collection.collection_1TKey.Contains(f);
                        contains = collection.collection_1TKey.Contains(m);
                        contains = collection.collection_1TKey.Contains(l);
                        contains = collection.collection_1TKey.Contains(inv);
                        stopwatch.Stop();

                        timeSpan = stopwatch.Elapsed;
                        Console.WriteLine("Время поиска:" + timeSpan);
                        
                        //third; key
                        Console.WriteLine("Collection SortedDictionary<string, Goods>");
                        Console.WriteLine("Введите ключ");
                        var key2 = Console.ReadLine();
                        
                        stopwatch = new Stopwatch();
                        stopwatch.Start();
                        contains = collection.collection_2StringTValue.ContainsKey(key2);
                        stopwatch.Stop();

                        timeSpan = stopwatch.Elapsed;
                        Console.WriteLine("Время поиска:" + timeSpan);
                        
                        //forth; key
                        Console.WriteLine("Collection SortedDictionary<Goods, Goods>");
                        Goods g = new Goods();
                        stopwatch = new Stopwatch();
                        
                        stopwatch.Start();
                        contains = collection.collection_2TKeyTValue.ContainsKey(g);
                        stopwatch.Stop();
                        
                        timeSpan = stopwatch.Elapsed;
                        Console.WriteLine("Время поиска:" + timeSpan);
                        
                        //firth; value
                        Console.WriteLine("Collection SortedDictionary<string, Goods>");
                        var toy = new Toy();
                        stopwatch = new Stopwatch();
                        
                        stopwatch.Start();
                        contains = collection.collection_2StringTValue.ContainsValue(toy);
                        stopwatch.Stop();
                        
                        timeSpan = stopwatch.Elapsed;
                        Console.WriteLine("Время поиска:" + timeSpan);
                        
                        //sixth
                        Console.WriteLine("Collection SortedDictionary<Goods, Goods>");
                        stopwatch = new Stopwatch();
                        
                        stopwatch.Start();
                        contains = collection.collection_2TKeyTValue.ContainsValue(toy);
                        stopwatch.Stop();
                        
                        timeSpan = stopwatch.Elapsed;
                        Console.WriteLine("Время поиска:" + timeSpan);
                        
                        break;
                    
                    case 3:
                        foreach (var keyPair in collection.collection_2StringTValue)
                        {
                            Console.WriteLine($"{keyPair.Key}) {keyPair.Value.ToString()}");
                        }
                        
                        break;
                    
                    case 4:
                        Console.WriteLine("Введите уникальный ключ: ");
                        do
                        {
                            key = Console.ReadLine();
                        } while (!collection.collection_1Int.Contains(key));

                        collection.collection_1Int.Remove(key);
                        collection.collection_2StringTValue.Remove(key);
                        break;
                        
                }

            } while (userChoice != 0);
        }
        
        public class TestCollection
        {
             public List<string> collection_1Int;
             public List<Goods> collection_1TKey;
            
             public SortedDictionary<Goods, Goods> collection_2TKeyTValue;
             public SortedDictionary<string, Goods> collection_2StringTValue;
            
             public int Length;
             
             public TestCollection(int length)
             {
                 // init collections 
                 collection_1Int = new List<string>();
                 collection_1TKey = new List<Goods>();
                 collection_2StringTValue = new SortedDictionary<string, Goods>();
                 collection_2TKeyTValue = new SortedDictionary<Goods, Goods>();
            
                 Length = length;
                 for (int i = 0; i < Length; i++)
                 {
                     // generate for keys collections
                     string tmpKey = "key number" + i;
                     
                     Random rnd = new Random();

                     int choice = rnd.Next(1, 4);
                     if (choice == 1)
                     {
                         Toy toy = new Toy();
                         collection_1Int.Add(tmpKey);
                         collection_1TKey.Add(new Goods(){Cost = i});
                         collection_2StringTValue.Add(tmpKey, toy);
                         collection_2TKeyTValue.Add(new Goods(){Cost = i}, toy);
                     }
                     else if (choice == 2)
                     {
                         Product toy = new Product();
                         collection_1Int.Add(tmpKey);
                         collection_1TKey.Add(new Goods(){Cost = i});
                         collection_2StringTValue.Add(tmpKey, toy);
                         collection_2TKeyTValue.Add(new Goods(){Cost = i}, toy);
                     }
                     else
                     {
                         MilkProduct toy = new MilkProduct();
                         collection_1Int.Add(tmpKey);
                         collection_1TKey.Add(new Goods(){Cost = i});
                         collection_2StringTValue.Add(tmpKey, toy);
                         collection_2TKeyTValue.Add(new Goods(){Cost = i}, toy);
                     }
                 }
             }
             
        }
    }
}
