# githubanswer1
This Repository is to demonstrate a solution for http://stackoverflow.com/questions/40258230/explain-weird-behavior-of-c-sharp-tcpclient-tcplistener-on-netcore

## The Question
To recreate the state of the question have a look at https://github.com/rwindegger/githubanswer1/tree/88af39fae46a64129122121ffbd0e2eabb27f00e. This contains a runable example.

## The Answer
The valid solution can be found at commit https://github.com/rwindegger/githubanswer1/tree/6681947993298b0e33e7f65f5a6c96d786110fa4. The question creator wasn't reading the whole input from the network stream. A small change on the server side and the server is able to handle strings of every length. It is still reading with a 32 byte buffer.

## Shout out
Since you're reading this make sure to stay up to date with the stuff I'm up to at https://www.windegger.wtf/