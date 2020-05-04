# ClientServer10App
> возможность отправки файлов или сообщений на сервер
* Скачайте пример проекта «Клиент-серверное приложение».
* Измените класс TCP/IP клиента, добавив возможность отправки файлов на сервер.
Для этого потребуется разместить на форме дополнительную кнопку («Send File») и
связать её с обработчиком SendFileToServer.
* Добавьте на сервер метод ReceiveFileFromClient, позволяющий получать и
сохранять файл от клиента.
Замечания:
  * файл сохраняется в папку вида ГГГГ-ММ-ДД (текущая дата), с названием
File<Номер полученного файла>.<Расширение файла>;
  * для потокобезопасного обновления номера полученного файла используйте
метод Interlock.Increment;
  * для отправки файлов можно использовать двоичную сериализацию
(подробнее о классе BinaryFormatter: https://docs.microsoft.com/enus/dotnet/api/system.runtime.serialization.formatters.binary.binaryformatter?view
=netframework-4.8) и метод File.WriteAllBytes.
* Добавьте вывод результатов работы сервера (логирование) в текстовое поле
оконного приложения клиента.
* Создайте внутри разработанного класса TCP/IP сервера переменные, определяющее
максимально возможное число соединений и текущее число соединений, реализуйте
проверку возможности подключения нового клиента. Для потокобезопасного
изменения текущего числа соединений используйте методы Interlock.Increment и
Interlock.Decrement.
* Проведите рефакторинг кода, добавьте XML-документацию и загрузите проект в
репозиторий на портале GitHub.
* Пришлите ссылку на репозиторий в качестве ответа на задание