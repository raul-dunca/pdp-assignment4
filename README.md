# Assignment 4

Write a program that is capable of simultaneously downloading several files through HTTP. Use directly the BeginConnect()/EndConnect(), BeginSend()/EndSend() and BeginReceive()/EndReceive() Socket functions, and write a simple parser for the HTTP protocol (it should be able only to get the header lines and to understand the Content-lenght: header line).

Try three implementations:

1) Directly implement the parser on the callbacks (event-driven);
2) Wrap the connect/send/receive operations in tasks, with the callback setting the result of the task;
3) Like the previous, but also use the async/await mechanism.
