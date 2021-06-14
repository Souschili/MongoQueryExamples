using MongoDB.Bson;
using MongoDB.Driver;
using MongoQueryExamples.Mongo;
using System;
using System.Collections.Generic;

namespace MongoQueryExamples
{
    class Program
    {
        /// <summary>
        /// Работа и вывод информации используя курсор
        /// </summary>
        /// <param name="cursor">Курсор полученый от сервера</param>
        static public void FindAllCursor(IAsyncCursor<object> cursor)
        {
            int batchCount = 1; // счетчик пакетных запросов
            // Условие означает ,пока итератор (курсор) не дошел до конца всех полученных документов цикл работает
            while (cursor.MoveNext()) 
            {
                // Перебор данных курсора, по дефолту сервер Монго отправляет 101 документ или 1 мб данных
                // Максимально размер данных может быть увеличен до 16 мб. 
                // Наглядно это видно , в первом случае по дефолту мы получаем 100 документов,
                //а потом происходит увеличение объема и мы разом получаем оставшиесся документы
                foreach (var elem in cursor.Current)
                {
                    Console.WriteLine(elem.ToBsonDocument());
                }
                Console.WriteLine($"Пакетный запрос номер {batchCount++}");
                Console.WriteLine("Курсор ждет следующую порцию данных от сервера!!");
                Console.ReadLine();
            }
            Console.WriteLine("Данных больше нет,завершаем итерацию документов");
            Console.ReadLine();
        }

        static public void FindAllCollection(ICollection<object> persons)
        {

        }


        static void Main(string[] args)
        {
            // Основной класс для работы с коллекцией
            ApplicationContext context = new ApplicationContext();

            //Простая выборка ,аналог нативной команды db.Persons.find({});
           
            // в данном варианте мы преобразуем пакетный ответ от сервера в коллекцию(список)
            //по которому мы можем пройтись в цикле
            List<object> getAllList = context.PersonCollection.Find(_ => true).ToList(); 

            // Тут мы работаем с курсором (итератором)
            IAsyncCursor<object> getAllCursor = context.PersonCollection.Find(_ => true).ToCursor();

            //используем Ling to Mongo
            var getAllLing = context.PersonCollection.AsQueryable<object>().ToList();
            foreach (var item in getAllLing)
            {
                Console.WriteLine(item.ToBsonDocument());
            }
            Console.ReadLine();
            // расскоментируй и запусти
            //FindAllCursor(getAllCursor);
            

           
        }
    }
}
