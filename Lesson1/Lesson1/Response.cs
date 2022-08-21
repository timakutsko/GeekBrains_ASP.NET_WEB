using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1
{
    /// <summary>
    /// Структура для сериализации JSON-ответа
    /// </summary>
    struct Response
    {
        public int userId { get; set; }

        public int id { get; set; }

        public string title { get; set; }

        public string body { get; set; }

    }
}
