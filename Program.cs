using System;
using System.IO;
using System.IO.Compression;

namespace IOCompressionProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceFile = @"C:\Users\Pavlo\source\repos\IOCompressionProject\bin\Debug\Исходный файл.png"; // исходный файл
            string compressedFile = @"C:\Users\Pavlo\source\repos\IOCompressionProject\bin\Debug\Архив с жатым файлом.gz"; // сжатый файл
            string targetFile = @"C:\Users\Pavlo\source\repos\IOCompressionProject\bin\Debug\Восстановлённый с архива файл.png"; // восстановленный файл

            Compress1(sourceFile, compressedFile); // создание сжатого файла
            Decompress1(compressedFile, targetFile); // чтение из сжатого файла

            Console.ReadLine();
        }

        public static void Compress1(string sourceFile, string compressedFile)
        {
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate)) // поток для чтения исходного файла
            {
                using (FileStream targetStream = File.Create(compressedFile)) // поток для записи сжатого файла
                {
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress)) // поток архивации
                    {
                        sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой
                        Console.WriteLine
                            (
                                "Сжатие файла по адресу «{0}» завершено.\n\n" +
                                "Исходный размер: {1}.\n" +
                                "Сжатый размер: {2}.\n\n",

                                sourceFile,
                                sourceStream.Length.ToString(),
                                targetStream.Length.ToString()
                            );
                    }
                }
            }
        }

        public static void Decompress1(string compressedFile, string targetFile)
        {
            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate)) // поток для чтения из сжатого файла
            {
                using (FileStream targetStream = File.Create(targetFile)) // поток для записи восстановленного файла
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress)) // поток разархивации
                    {
                        decompressionStream.CopyTo(targetStream);
                        Console.WriteLine
                            (
                                "Файл по адресу «{0}» восстановлен!",

                                targetFile
                            );
                    }
                }
            }
        }
    }
}